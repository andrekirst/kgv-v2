#!/bin/bash
# KGV GitHub Project Setup Script
# Erstellt automatisch das komplette GitHub Project mit Milestones und Issues

set -e

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m'

# Configuration
REPO="andrekirst/kgv-v2"
PROJECT_TITLE="KGV-v2 Development"
PROJECT_BODY="Comprehensive project management for KGV system migration from legacy VB to modern .NET 9 + Angular"

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

check_prerequisites() {
    log_info "Checking prerequisites..."
    
    # Check if gh CLI is installed
    if ! command -v gh &> /dev/null; then
        log_error "GitHub CLI (gh) is not installed. Please install it first:"
        echo "https://cli.github.com/"
        exit 1
    fi
    
    # Check if user is authenticated
    if ! gh auth status &> /dev/null; then
        log_error "Not authenticated with GitHub. Please run 'gh auth login' first."
        exit 1
    fi
    
    # Check if we can access the repository
    if ! gh repo view $REPO &> /dev/null; then
        log_error "Cannot access repository $REPO. Please check permissions."
        exit 1
    fi
    
    log_success "Prerequisites check passed"
}

create_labels() {
    log_info "Creating project labels..."
    
    # Size labels
    gh label create "size/XS" --color "00ff00" --description "0.5 day - Trivial tasks" --repo $REPO --force
    gh label create "size/S" --color "ffff00" --description "1 day - Simple features" --repo $REPO --force
    gh label create "size/M" --color "ff8800" --description "1.5 days - Standard features" --repo $REPO --force
    gh label create "size/L" --color "ff0000" --description "2 days - Complex features" --repo $REPO --force
    gh label create "size/XL" --color "000000" --description ">2 days - SPLIT REQUIRED!" --repo $REPO --force
    
    # Type labels
    gh label create "feature" --color "0075ca" --description "New functionality" --repo $REPO --force
    gh label create "bug" --color "d73a4a" --description "Something isn't working" --repo $REPO --force
    gh label create "task" --color "7057ff" --description "Infrastructure/maintenance task" --repo $REPO --force
    gh label create "documentation" --color "0052cc" --description "Improvements or additions to docs" --repo $REPO --force
    gh label create "refactor" --color "fbca04" --description "Code quality improvement" --repo $REPO --force
    
    # Priority labels
    gh label create "priority/critical" --color "b60205" --description "🔥 Critical - Blocking issues" --repo $REPO --force
    gh label create "priority/high" --color "d93f0b" --description "🚨 High - Important features" --repo $REPO --force
    gh label create "priority/medium" --color "fbca04" --description "📋 Medium - Standard priority" --repo $REPO --force
    gh label create "priority/low" --color "0e8a16" --description "📝 Low - Nice to have" --repo $REPO --force
    
    # Component labels
    gh label create "domain" --color "5319e7" --description "🏛️ Domain Layer (PROTECTED)" --repo $REPO --force
    gh label create "application" --color "1d76db" --description "💼 Application Layer" --repo $REPO --force
    gh label create "api" --color "0052cc" --description "🌐 Web API Controllers" --repo $REPO --force
    gh label create "frontend" --color "d4c5f9" --description "🖥️ Angular Components" --repo $REPO --force
    gh label create "database" --color "c2e0c6" --description "🗃️ Database/Migrations" --repo $REPO --force
    gh label create "auth" --color "f9d0c4" --description "🔐 Authentication/Authorization" --repo $REPO --force
    
    # Domain risk labels
    gh label create "domain-safe" --color "28a745" --description "🟢 Safe - No domain changes" --repo $REPO --force
    gh label create "domain-review" --color "ffc107" --description "🟡 Review - Might affect domain" --repo $REPO --force
    gh label create "domain-risk" --color "dc3545" --description "🔴 High Risk - Domain changes" --repo $REPO --force
    gh label create "domain-blocked" --color "6c757d" --description "⚫ Blocked - Needs approval" --repo $REPO --force
    
    # Workflow labels
    gh label create "needs-sizing" --color "f9c2ff" --description "Needs size estimation" --repo $REPO --force
    gh label create "needs-triage" --color "d876cc" --description "Needs initial review" --repo $REPO --force
    gh label create "ready" --color "c5f015" --description "Ready for development" --repo $REPO --force
    gh label create "in-progress" --color "0075ca" --description "Currently being worked on" --repo $REPO --force
    gh label create "blocked" --color "b60205" --description "Blocked by external dependency" --repo $REPO --force
    
    log_success "Labels created successfully"
}

create_milestones() {
    log_info "Creating project milestones..."
    
    # Calculate dates (assuming project starts in 2 weeks)
    START_DATE=$(date -d "+14 days" +%Y-%m-%d)
    
    # Milestone 1: Foundation (10 weeks)
    M1_END=$(date -d "$START_DATE +70 days" +%Y-%m-%d)
    gh api repos/$REPO/milestones \
        --method POST \
        --field title="🏗️ M1: Foundation" \
        --field description="Technical foundation: Domain model, authentication, basic API endpoints. 20 issues, 40 development days." \
        --field due_on="${M1_END}T23:59:59Z" \
        --field state="open"
    
    # Milestone 2: Core Features (12 weeks after M1)
    M2_END=$(date -d "$M1_END +84 days" +%Y-%m-%d)
    gh api repos/$REPO/milestones \
        --method POST \
        --field title="🌟 M2: Core Features" \
        --field description="Main functionality: Citizen portal, admin portal, waiting list management. 30 issues, 60 development days." \
        --field due_on="${M2_END}T23:59:59Z" \
        --field state="open"
    
    # Milestone 3: Advanced Features (10 weeks after M2)
    M3_END=$(date -d "$M2_END +70 days" +%Y-%m-%d)
    gh api repos/$REPO/milestones \
        --method POST \
        --field title="🚀 M3: Advanced Features" \
        --field description="Advanced workflows, reporting, mobile optimization, integrations. 25 issues, 50 development days." \
        --field due_on="${M3_END}T23:59:59Z" \
        --field state="open"
    
    # Milestone 4: Production Ready (8 weeks after M3)
    M4_END=$(date -d "$M3_END +56 days" +%Y-%m-%d)
    gh api repos/$REPO/milestones \
        --method POST \
        --field title="🎯 M4: Production Ready" \
        --field description="Security hardening, performance optimization, deployment, go-live preparation. 15 issues, 30 development days." \
        --field due_on="${M4_END}T23:59:59Z" \
        --field state="open"
    
    log_success "Milestones created successfully"
    log_info "Milestone dates:"
    log_info "  M1 Foundation: $START_DATE → $M1_END"
    log_info "  M2 Core Features: $M1_END → $M2_END" 
    log_info "  M3 Advanced Features: $M2_END → $M3_END"
    log_info "  M4 Production Ready: $M3_END → $M4_END"
}

create_project() {
    log_info "Creating GitHub Project..."
    
    # Create project using GitHub CLI
    PROJECT_URL=$(gh project create \
        --title "$PROJECT_TITLE" \
        --body "$PROJECT_BODY" \
        --public \
        --format json | jq -r '.url')
    
    if [ -z "$PROJECT_URL" ]; then
        log_error "Failed to create project"
        exit 1
    fi
    
    log_success "Project created: $PROJECT_URL"
    
    # Extract project number from URL
    PROJECT_NUMBER=$(echo $PROJECT_URL | grep -o '[0-9]*$')
    echo "PROJECT_NUMBER=$PROJECT_NUMBER" > .project_config
    
    log_info "Project configuration saved to .project_config"
}

setup_project_fields() {
    log_info "Setting up custom project fields..."
    
    if [ ! -f .project_config ]; then
        log_error ".project_config not found. Please run create_project first."
        exit 1
    fi
    
    source .project_config
    
    # Note: GitHub CLI v2.20+ supports project field creation
    # For older versions, fields must be created manually in the GitHub web interface
    
    log_info "Creating custom fields requires GitHub CLI v2.20+ or manual setup in web interface"
    log_info "Required fields to create manually:"
    log_info "  1. Size (Single select): XS, S, M, L, XL"
    log_info "  2. Component (Single select): Domain, Application, API, Frontend, Database, Auth"
    log_info "  3. Domain Risk (Single select): Safe, Review, High Risk, Blocked"
    log_info "  4. Sprint (Single select): Backlog, Current Sprint, Next Sprint, etc."
    log_info "  5. Business Value (Single select): Critical, High, Medium, Low"
    
    log_warning "Please configure these fields manually in the project settings"
}

# Main execution
main() {
    echo "=========================================="
    echo "🏗️  KGV GitHub Project Setup"
    echo "=========================================="
    echo ""
    
    check_prerequisites
    
    echo ""
    log_info "This script will create:"
    log_info "  • Project labels for issue management"
    log_info "  • 4 milestones with calculated dates"  
    log_info "  • GitHub Project board"
    log_info "  • Custom field setup instructions"
    echo ""
    
    read -p "Do you want to continue? (y/N) " -n 1 -r
    echo
    if [[ ! $REPLY =~ ^[Yy]$ ]]; then
        log_info "Setup cancelled by user"
        exit 0
    fi
    
    echo ""
    create_labels
    echo ""
    create_milestones  
    echo ""
    create_project
    echo ""
    setup_project_fields
    echo ""
    
    log_success "=========================================="
    log_success "🎉 GitHub Project setup completed!"
    log_success "=========================================="
    echo ""
    log_info "Next steps:"
    log_info "  1. Visit the project URL and configure custom fields"
    log_info "  2. Run ./create-milestone-issues.sh to create all 90 issues"
    log_info "  3. Set up project views and automation rules"
    echo ""
    log_info "Project URL: $PROJECT_URL"
}

# Run main function
main "$@"