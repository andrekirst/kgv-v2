#!/bin/bash
# KGV Domain Compliance Monitor
# Continuous monitoring and reporting of domain model integrity

set -e

# Configuration
SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
PROJECT_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"
REPORT_DIR="$PROJECT_ROOT/.domain-guard/reports"
CONFIG_FILE="$SCRIPT_DIR/config.json"

# Colors
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m'

# Create reports directory
mkdir -p "$REPORT_DIR"

# Functions
log_info() {
    echo -e "${BLUE}ℹ️  $1${NC}"
}

log_success() {
    echo -e "${GREEN}✅ $1${NC}"
}

log_warning() {
    echo -e "${YELLOW}⚠️  $1${NC}"
}

log_error() {
    echo -e "${RED}❌ $1${NC}"
}

# Generate timestamp
TIMESTAMP=$(date "+%Y-%m-%d_%H-%M-%S")
REPORT_FILE="$REPORT_DIR/compliance-report-$TIMESTAMP.md"

# Start monitoring
log_info "Starting KGV Domain Compliance Monitor..."
echo "📊 Report will be saved to: $REPORT_FILE"

# Initialize report
cat > "$REPORT_FILE" << EOF
# Domain Compliance Report

**Generated**: $(date)  
**Project**: KGV-v2  
**Monitor Version**: 1.0.0  

---

## Executive Summary

EOF

# Check 1: CLAUDE.md integrity
log_info "Checking CLAUDE.md domain contract..."
if [ ! -f "$PROJECT_ROOT/CLAUDE.md" ]; then
    log_error "CLAUDE.md missing!"
    echo "- ❌ **CLAUDE.md**: Missing domain protection contract" >> "$REPORT_FILE"
else
    # Check if CLAUDE.md has been modified recently
    CLAUDE_MODIFIED=$(stat -c %Y "$PROJECT_ROOT/CLAUDE.md")
    WEEK_AGO=$(date -d "7 days ago" +%s)
    
    if [ $CLAUDE_MODIFIED -gt $WEEK_AGO ]; then
        log_warning "CLAUDE.md modified recently - review needed"
        echo "- ⚠️  **CLAUDE.md**: Modified within last week - review required" >> "$REPORT_FILE"
    else
        log_success "CLAUDE.md intact"
        echo "- ✅ **CLAUDE.md**: Stable and unchanged" >> "$REPORT_FILE"
    fi
fi

# Check 2: Domain documentation consistency
log_info "Validating domain documentation..."
DOMAIN_DOC="$PROJECT_ROOT/docs/domain-model-documentation.md"
if [ ! -f "$DOMAIN_DOC" ]; then
    log_error "Domain documentation missing!"
    echo "- ❌ **Domain Documentation**: Missing" >> "$REPORT_FILE"
else
    # Check for required sections
    REQUIRED_SECTIONS=("Core Domain Types" "Business Rules" "Bounded Contexts" "ADR-")
    MISSING_SECTIONS=()
    
    for section in "${REQUIRED_SECTIONS[@]}"; do
        if ! grep -q "$section" "$DOMAIN_DOC"; then
            MISSING_SECTIONS+=("$section")
        fi
    done
    
    if [ ${#MISSING_SECTIONS[@]} -gt 0 ]; then
        log_warning "Domain documentation incomplete"
        echo "- ⚠️  **Domain Documentation**: Missing sections: ${MISSING_SECTIONS[*]}" >> "$REPORT_FILE"
    else
        log_success "Domain documentation complete"
        echo "- ✅ **Domain Documentation**: All required sections present" >> "$REPORT_FILE"
    fi
fi

# Check 3: Domain code integrity
log_info "Scanning for domain code modifications..."
DOMAIN_VIOLATIONS=0

# Find all C# files in potential domain directories
DOMAIN_FILES=$(find "$PROJECT_ROOT" -name "*.cs" -path "*/Domain/*" 2>/dev/null || true)
if [ -n "$DOMAIN_FILES" ]; then
    while IFS= read -r file; do
        # Check for concerning patterns
        if grep -q "// TODO:" "$file" 2>/dev/null; then
            log_warning "TODO comments found in domain file: $(basename $file)"
            ((DOMAIN_VIOLATIONS++))
        fi
        
        # Check for mutable properties in value objects
        if grep -q "public.*{ get; set; }" "$file" 2>/dev/null && grep -q "record.*\(Aktenzeichen\|PersonData\|Address\)" "$file" 2>/dev/null; then
            log_error "Mutable properties in value object: $(basename $file)"
            ((DOMAIN_VIOLATIONS++))
        fi
    done <<< "$DOMAIN_FILES"
fi

if [ $DOMAIN_VIOLATIONS -eq 0 ]; then
    log_success "No domain code violations detected"
    echo "- ✅ **Domain Code**: No violations detected" >> "$REPORT_FILE"
else
    log_error "$DOMAIN_VIOLATIONS domain violations found"
    echo "- ❌ **Domain Code**: $DOMAIN_VIOLATIONS violations detected" >> "$REPORT_FILE"
fi

# Check 4: Git hook installation
log_info "Verifying git hooks..."
GIT_HOOK="$PROJECT_ROOT/.git/hooks/pre-commit"
if [ ! -f "$GIT_HOOK" ]; then
    log_warning "Pre-commit hook not installed"
    echo "- ⚠️  **Git Hooks**: Pre-commit hook missing - run .domain-guard/install-hooks.sh" >> "$REPORT_FILE"
elif [ ! -x "$GIT_HOOK" ]; then
    log_warning "Pre-commit hook not executable"
    echo "- ⚠️  **Git Hooks**: Pre-commit hook not executable" >> "$REPORT_FILE"
else
    log_success "Pre-commit hook active"
    echo "- ✅ **Git Hooks**: Pre-commit validation active" >> "$REPORT_FILE"
fi

# Check 5: Recent commit analysis
log_info "Analyzing recent commits for domain changes..."
RECENT_COMMITS=$(git log --oneline --since="7 days ago" 2>/dev/null || echo "")
DOMAIN_COMMITS=0

if [ -n "$RECENT_COMMITS" ]; then
    while IFS= read -r commit; do
        # Check if commit involves domain files
        COMMIT_HASH=$(echo "$commit" | cut -d' ' -f1)
        DOMAIN_FILES_CHANGED=$(git diff-tree --no-commit-id --name-only -r "$COMMIT_HASH" 2>/dev/null | grep -E "(Domain|ValueObject|Entity|Aggregate)" || true)
        
        if [ -n "$DOMAIN_FILES_CHANGED" ]; then
            log_warning "Domain changes in commit: $COMMIT_HASH"
            ((DOMAIN_COMMITS++))
        fi
    done <<< "$RECENT_COMMITS"
fi

if [ $DOMAIN_COMMITS -eq 0 ]; then
    log_success "No domain modifications in recent commits"
    echo "- ✅ **Recent Commits**: No domain changes detected" >> "$REPORT_FILE"
else
    log_warning "$DOMAIN_COMMITS commits with domain changes"
    echo "- ⚠️  **Recent Commits**: $DOMAIN_COMMITS commits modified domain files" >> "$REPORT_FILE"
fi

# Generate compliance score
TOTAL_CHECKS=5
PASSED_CHECKS=0

# Count passed checks from report
if grep -q "✅.*CLAUDE.md" "$REPORT_FILE"; then ((PASSED_CHECKS++)); fi
if grep -q "✅.*Domain Documentation" "$REPORT_FILE"; then ((PASSED_CHECKS++)); fi
if grep -q "✅.*Domain Code" "$REPORT_FILE"; then ((PASSED_CHECKS++)); fi
if grep -q "✅.*Git Hooks" "$REPORT_FILE"; then ((PASSED_CHECKS++)); fi
if grep -q "✅.*Recent Commits" "$REPORT_FILE"; then ((PASSED_CHECKS++)); fi

COMPLIANCE_PERCENTAGE=$((PASSED_CHECKS * 100 / TOTAL_CHECKS))

# Add summary to report
cat >> "$REPORT_FILE" << EOF

**Compliance Score**: $COMPLIANCE_PERCENTAGE% ($PASSED_CHECKS/$TOTAL_CHECKS checks passed)

EOF

if [ $COMPLIANCE_PERCENTAGE -eq 100 ]; then
    echo "- 🎉 **Overall Status**: EXCELLENT - Full domain compliance" >> "$REPORT_FILE"
    log_success "Domain compliance: $COMPLIANCE_PERCENTAGE% - Excellent!"
elif [ $COMPLIANCE_PERCENTAGE -ge 80 ]; then
    echo "- ✅ **Overall Status**: GOOD - Minor issues to address" >> "$REPORT_FILE"
    log_success "Domain compliance: $COMPLIANCE_PERCENTAGE% - Good"
else
    echo "- ⚠️  **Overall Status**: ATTENTION REQUIRED - Multiple issues detected" >> "$REPORT_FILE"
    log_warning "Domain compliance: $COMPLIANCE_PERCENTAGE% - Needs attention"
fi

# Add detailed findings
cat >> "$REPORT_FILE" << EOF

---

## Detailed Findings

### Domain Protection Status
$(if [ -f "$PROJECT_ROOT/CLAUDE.md" ]; then echo "✅ Domain contract in place"; else echo "❌ Domain contract missing"; fi)
$(if [ -f "$PROJECT_ROOT/.domain-guard/pre-commit-hook.sh" ]; then echo "✅ Pre-commit validation available"; else echo "❌ Pre-commit validation missing"; fi)
$(if [ -f "$PROJECT_ROOT/.domain-guard/config.json" ]; then echo "✅ Domain guard configuration present"; else echo "❌ Domain guard configuration missing"; fi)

### Architecture Compliance
- **Bounded Contexts**: $(grep -c "Context" "$DOMAIN_DOC" 2>/dev/null || echo "Unknown")
- **Aggregate Roots**: $(grep -c "Aggregate Root" "$DOMAIN_DOC" 2>/dev/null || echo "Unknown") 
- **Value Objects**: $(grep -c "Value Object" "$DOMAIN_DOC" 2>/dev/null || echo "Unknown")
- **Domain Events**: $(grep -c "Domain Event" "$DOMAIN_DOC" 2>/dev/null || echo "Unknown")

### Business Rules Status
- **Application Validity**: $(grep -c "InitialValidityMonths.*12" "$PROJECT_ROOT" -r 2>/dev/null || echo "Unknown") occurrences
- **Extension Rules**: $(grep -c "MaxExtensions.*3" "$PROJECT_ROOT" -r 2>/dev/null || echo "Unknown") occurrences
- **Status Transitions**: $(grep -c "ValidTransitions" "$PROJECT_ROOT" -r 2>/dev/null || echo "Unknown") implementations

---

## Recommendations

EOF

# Add recommendations based on findings
if ! grep -q "✅.*CLAUDE.md" "$REPORT_FILE"; then
    echo "1. **Install Domain Protection**: Run the domain protection setup to create CLAUDE.md" >> "$REPORT_FILE"
fi

if ! grep -q "✅.*Git Hooks" "$REPORT_FILE"; then
    echo "2. **Install Git Hooks**: Run \`.domain-guard/install-hooks.sh\` to enable pre-commit validation" >> "$REPORT_FILE"
fi

if [ $DOMAIN_VIOLATIONS -gt 0 ]; then
    echo "3. **Fix Domain Violations**: Review and fix the $DOMAIN_VIOLATIONS domain code violations detected" >> "$REPORT_FILE"
fi

if [ $DOMAIN_COMMITS -gt 0 ]; then
    echo "4. **Review Domain Changes**: Analyze the $DOMAIN_COMMITS recent commits that modified domain files" >> "$REPORT_FILE"
fi

if [ $COMPLIANCE_PERCENTAGE -eq 100 ]; then
    echo "🎉 **Excellent Work!** Domain model integrity is fully maintained. Keep up the great work!" >> "$REPORT_FILE"
fi

# Footer
cat >> "$REPORT_FILE" << EOF

---

*Report generated by KGV Domain Guard v1.0.0 on $(date)*  
*For support, see: docs/development-workflow.md*
EOF

# Final output
echo "=================================================="
if [ $COMPLIANCE_PERCENTAGE -ge 90 ]; then
    log_success "Domain compliance monitoring complete! Score: $COMPLIANCE_PERCENTAGE%"
elif [ $COMPLIANCE_PERCENTAGE -ge 70 ]; then
    log_warning "Domain compliance monitoring complete. Score: $COMPLIANCE_PERCENTAGE% - Some attention needed"
else
    log_error "Domain compliance monitoring complete. Score: $COMPLIANCE_PERCENTAGE% - Immediate action required"
fi

echo "📄 Full report: $REPORT_FILE"
echo "🔄 Run this monitor regularly to maintain domain integrity"
echo ""