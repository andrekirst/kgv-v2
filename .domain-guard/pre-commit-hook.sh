#!/bin/bash
# KGV Domain Guard - Pre-commit Hook
# Prevents domain model violations before commit

set -e

echo "🛡️  KGV Domain Guard - Checking domain model compliance..."

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Configuration
DOMAIN_GUARD_DIR=".domain-guard"
DOMAIN_DOCS_DIR="docs"
BLOCKED_PATTERNS=(
    "# IMMUTABLE" 
    "# PROTECTED"
    "# BUSINESS REQUIREMENT"
    "InitialValidityMonths"
    "ExtensionMonths" 
    "MaxExtensions"
)

# Files to check for domain violations
DOMAIN_FILES=$(git diff --cached --name-only | grep -E '\.(cs|ts)$' || true)

if [ -z "$DOMAIN_FILES" ]; then
    echo -e "${GREEN}✅ No domain files to check${NC}"
    exit 0
fi

echo "🔍 Checking files: $DOMAIN_FILES"

# Function to check for protected constants
check_protected_constants() {
    local file=$1
    local violations=0
    
    # Check if file modifies protected business rules
    if git diff --cached "$file" | grep -E '\+.*InitialValidityMonths.*=.*[^12]' > /dev/null; then
        echo -e "${RED}❌ VIOLATION: InitialValidityMonths modified in $file${NC}"
        echo -e "${YELLOW}   Business rule constant cannot be changed (must be 12)${NC}"
        violations=$((violations + 1))
    fi
    
    if git diff --cached "$file" | grep -E '\+.*MaxExtensions.*=.*[^3]' > /dev/null; then
        echo -e "${RED}❌ VIOLATION: MaxExtensions modified in $file${NC}"
        echo -e "${YELLOW}   Business rule constant cannot be changed (must be 3)${NC}"
        violations=$((violations + 1))
    fi
    
    return $violations
}

# Function to check domain event schema modifications
check_event_schemas() {
    local file=$1
    local violations=0
    
    # Check for event schema breaking changes
    if git diff --cached "$file" | grep -E '\+.*EventVersion.*=.*"[^1]' > /dev/null; then
        echo -e "${RED}❌ VIOLATION: Event schema version change detected in $file${NC}"
        echo -e "${YELLOW}   Event schemas must maintain backward compatibility${NC}"
        violations=$((violations + 1))
    fi
    
    # Check for domain event property removal
    if git diff --cached "$file" | grep -E '^-.*public.*{.*get.*}' > /dev/null; then
        echo -e "${RED}❌ VIOLATION: Property removal detected in $file${NC}"
        echo -e "${YELLOW}   Domain event properties cannot be removed (breaking change)${NC}"
        violations=$((violations + 1))
    fi
    
    return $violations
}

# Function to check value object immutability
check_value_object_immutability() {
    local file=$1
    local violations=0
    
    # Check for mutable properties in value objects
    if git diff --cached "$file" | grep -E '\+.*public.*\{.*set.*\}' > /dev/null && \
       git diff --cached "$file" | grep -E 'record.*Aktenzeichen|record.*PersonData|record.*Address' > /dev/null; then
        echo -e "${RED}❌ VIOLATION: Mutable property added to value object in $file${NC}"
        echo -e "${YELLOW}   Value objects must be immutable (use init or readonly)${NC}"
        violations=$((violations + 1))
    fi
    
    return $violations
}

# Function to check Aktenzeichen pattern modifications
check_aktenzeichen_pattern() {
    local file=$1
    local violations=0
    
    # Check if Aktenzeichen regex pattern is modified
    if git diff --cached "$file" | grep -E '\+.*Pattern.*=.*new.*Regex.*"[^(]' > /dev/null; then
        echo -e "${RED}❌ VIOLATION: Aktenzeichen pattern modified in $file${NC}"
        echo -e "${YELLOW}   Pattern must remain: ^(32\\.2|33\\.2)\\s+(\\d+)\\s+(\\d{4})$${NC}"
        violations=$((violations + 1))
    fi
    
    return $violations
}

# Main validation loop
total_violations=0

for file in $DOMAIN_FILES; do
    echo "🔍 Checking $file..."
    
    file_violations=0
    
    # Run all checks
    check_protected_constants "$file" || file_violations=$((file_violations + $?))
    check_event_schemas "$file" || file_violations=$((file_violations + $?))
    check_value_object_immutability "$file" || file_violations=$((file_violations + $?))
    check_aktenzeichen_pattern "$file" || file_violations=$((file_violations + $?))
    
    total_violations=$((total_violations + file_violations))
    
    if [ $file_violations -eq 0 ]; then
        echo -e "${GREEN}✅ $file passed domain validation${NC}"
    fi
done

# Check if CLAUDE.md exists and is up to date
if [ ! -f "CLAUDE.md" ]; then
    echo -e "${RED}❌ VIOLATION: CLAUDE.md domain contract missing${NC}"
    total_violations=$((total_violations + 1))
fi

# Final result
echo "=================================================="
if [ $total_violations -eq 0 ]; then
    echo -e "${GREEN}🎉 All domain checks passed! Commit allowed.${NC}"
    echo -e "${GREEN}✅ Domain model integrity maintained${NC}"
    exit 0
else
    echo -e "${RED}🚨 Domain violations detected: $total_violations${NC}"
    echo -e "${RED}❌ Commit blocked to protect domain model${NC}"
    echo ""
    echo -e "${YELLOW}🛠️  How to fix:${NC}"
    echo -e "${YELLOW}   1. Review CLAUDE.md domain protection guidelines${NC}"
    echo -e "${YELLOW}   2. Implement changes in Application/Infrastructure layers${NC}"
    echo -e "${YELLOW}   3. Request approval for necessary domain changes${NC}"
    echo -e "${YELLOW}   4. Reference docs/domain-model-documentation.md${NC}"
    echo ""
    exit 1
fi