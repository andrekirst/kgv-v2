#!/bin/bash
# KGV Commit Message Validation
# Validates conventional commit format and issue references

set -e

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m'

# Get commit message from file or parameter
COMMIT_MSG_FILE=${1:-".git/COMMIT_EDITMSG"}

if [ ! -f "$COMMIT_MSG_FILE" ]; then
    echo -e "${RED}❌ Commit message file not found: $COMMIT_MSG_FILE${NC}"
    exit 1
fi

COMMIT_MSG=$(head -n 1 "$COMMIT_MSG_FILE")

echo -e "${BLUE}🔍 Validating commit message format...${NC}"
echo "Message: $COMMIT_MSG"

# Conventional Commit Pattern
CONVENTIONAL_PATTERN="^(feat|fix|docs|style|refactor|test|chore|perf|ci|build)(\(.+\))?: .{1,50}"

# Issue Reference Patterns
ISSUE_CLOSE_PATTERN="(Closes|Fixes|Resolves) #[0-9]+"
ISSUE_RELATE_PATTERN="(Relates to|References|Refs) #[0-9]+"

# Validation flags
VALID_FORMAT=false
HAS_ISSUE_REFERENCE=false

# Check conventional commit format
if [[ $COMMIT_MSG =~ $CONVENTIONAL_PATTERN ]]; then
    VALID_FORMAT=true
    echo -e "${GREEN}✅ Conventional commit format: VALID${NC}"
else
    echo -e "${RED}❌ Conventional commit format: INVALID${NC}"
    echo -e "${YELLOW}Expected format: type(scope): description${NC}"
    echo -e "${YELLOW}Example: feat(export): add PDF export functionality${NC}"
fi

# Check for issue references
FULL_COMMIT_MSG=$(cat "$COMMIT_MSG_FILE")

if [[ $FULL_COMMIT_MSG =~ $ISSUE_CLOSE_PATTERN ]] || [[ $FULL_COMMIT_MSG =~ $ISSUE_RELATE_PATTERN ]]; then
    HAS_ISSUE_REFERENCE=true
    echo -e "${GREEN}✅ Issue reference: FOUND${NC}"
    
    # Extract issue numbers
    ISSUES=$(echo "$FULL_COMMIT_MSG" | grep -oE "#[0-9]+" | sort -u | tr '\n' ' ')
    echo -e "${BLUE}📋 Referenced issues: $ISSUES${NC}"
else
    echo -e "${YELLOW}⚠️  Issue reference: MISSING (recommended)${NC}"
    echo -e "${YELLOW}Add: 'Relates to #123' or 'Closes #123'${NC}"
fi

# Check commit types and provide guidance
TYPE=$(echo "$COMMIT_MSG" | grep -oE "^[a-z]+" | head -1)
case $TYPE in
    "feat")
        echo -e "${BLUE}💡 Type: Feature - Adding new functionality${NC}"
        ;;
    "fix")
        echo -e "${BLUE}🔧 Type: Bug Fix - Fixing existing issues${NC}"
        ;;
    "docs")
        echo -e "${BLUE}📚 Type: Documentation - Documentation changes${NC}"
        ;;
    "refactor")
        echo -e "${BLUE}♻️  Type: Refactor - Code improvement without feature changes${NC}"
        ;;
    "test")
        echo -e "${BLUE}🧪 Type: Test - Adding or updating tests${NC}"
        ;;
    *)
        if [ "$VALID_FORMAT" = false ]; then
            echo -e "${YELLOW}💡 Valid types: feat, fix, docs, style, refactor, test, chore, perf, ci, build${NC}"
        fi
        ;;
esac

# Check scope recommendations for KGV project
SCOPE=$(echo "$COMMIT_MSG" | grep -oE "\([^)]+\)" | tr -d "()" | head -1)
if [ -n "$SCOPE" ]; then
    case $SCOPE in
        "domain"|"app"|"api"|"ui"|"db"|"auth"|"export"|"validation")
            echo -e "${BLUE}🎯 Scope: '$SCOPE' - Recognized KGV scope${NC}"
            ;;
        *)
            echo -e "${YELLOW}💡 Consider using KGV scopes: domain, app, api, ui, db, auth, export, validation${NC}"
            ;;
    esac
fi

# Check commit message length
MSG_LENGTH=${#COMMIT_MSG}
if [ $MSG_LENGTH -gt 50 ]; then
    echo -e "${YELLOW}⚠️  Commit message length: $MSG_LENGTH characters (consider shortening)${NC}"
else
    echo -e "${GREEN}✅ Commit message length: $MSG_LENGTH characters${NC}"
fi

# Check for domain-related commits
if [[ $FULL_COMMIT_MSG =~ (domain|Domain|DOMAIN) ]] && [[ ! $FULL_COMMIT_MSG =~ \[domain-approved\] ]]; then
    echo -e "${YELLOW}⚠️  Domain-related commit detected without approval marker${NC}"
    echo -e "${YELLOW}💡 If domain changes are approved, add '[domain-approved]' to commit message${NC}"
fi

# Branch name validation (if available)
CURRENT_BRANCH=$(git branch --show-current 2>/dev/null || echo "unknown")
if [ "$CURRENT_BRANCH" != "main" ] && [ "$CURRENT_BRANCH" != "unknown" ]; then
    # Extract issue number from branch name
    BRANCH_ISSUE=$(echo "$CURRENT_BRANCH" | grep -oE "[0-9]+" | head -1)
    if [ -n "$BRANCH_ISSUE" ] && [ "$HAS_ISSUE_REFERENCE" = true ]; then
        # Check if branch issue matches commit issue
        if [[ $FULL_COMMIT_MSG =~ #$BRANCH_ISSUE ]]; then
            echo -e "${GREEN}✅ Branch-issue consistency: Branch and commit reference same issue #$BRANCH_ISSUE${NC}"
        else
            echo -e "${YELLOW}⚠️  Branch-issue consistency: Branch suggests #$BRANCH_ISSUE but commit references different issue${NC}"
        fi
    fi
fi

# Final validation
echo "=================================================="

if [ "$VALID_FORMAT" = true ]; then
    echo -e "${GREEN}🎉 Commit message validation: PASSED${NC}"
    
    # Provide tips for improvement
    if [ "$HAS_ISSUE_REFERENCE" = false ]; then
        echo -e "${YELLOW}💡 Tip: Add issue reference for better traceability${NC}"
    fi
    
    exit 0
else
    echo -e "${RED}❌ Commit message validation: FAILED${NC}"
    echo ""
    echo -e "${YELLOW}🛠️  How to fix:${NC}"
    echo -e "${YELLOW}   1. Use conventional commit format: type(scope): description${NC}"
    echo -e "${YELLOW}   2. Valid types: feat, fix, docs, style, refactor, test, chore, perf, ci, build${NC}"
    echo -e "${YELLOW}   3. Add issue reference: 'Relates to #123' or 'Closes #123'${NC}"
    echo -e "${YELLOW}   4. Keep first line under 50 characters${NC}"
    echo ""
    echo -e "${BLUE}📚 See docs/git-workflow-guide.md for examples${NC}"
    exit 1
fi