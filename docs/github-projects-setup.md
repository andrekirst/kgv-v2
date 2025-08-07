# 📊 GitHub Projects Setup für KGV-v2

## 🎯 Übersicht

Diese Anleitung erklärt die Einrichtung von GitHub Projects v2 für das KGV-System mit automatisierter Issue-Verwaltung, Milestone-Tracking und Team-Koordination.

---

## 🏗️ Haupt-Projekt Setup

### **Projekt erstellen**
1. **Repository navigieren**: `https://github.com/andrekirst/kgv-v2`
2. **Projects Tab** → **"Link a project"** → **"Create new project"**
3. **Project Name**: `KGV-v2 Development`  
4. **Template**: `Feature development` (angepasst)
5. **Visibility**: `Private` (Repository-Zugriff)

### **Basis-Konfiguration**
```yaml
Project Configuration:
  Name: "KGV-v2 Development"
  Description: "Comprehensive project management for KGV system development"
  README: |
    # KGV-v2 Development Project
    
    This project tracks all development work for the KGV system migration.
    
    ## Quick Links
    - 📚 [Documentation](https://github.com/andrekirst/kgv-v2/docs)
    - 🛡️ [Domain Protection](https://github.com/andrekirst/kgv-v2/CLAUDE.md)
    - 🌿 [Git Workflow](https://github.com/andrekirst/kgv-v2/docs/git-workflow-guide.md)
    
    ## Issue Guidelines
    - Maximum 2 days per issue
    - Features must include Backend + Frontend
    - Follow domain protection rules
```

---

## 📋 Custom Fields Configuration

### **Standard Fields**

#### **1. Size Estimation**
```yaml
Field Name: "Size"
Type: Single select
Options:
  - "size/XS" (0.5 day) - 🟢
  - "size/S" (1 day) - 🟡  
  - "size/M" (1.5 days) - 🟠
  - "size/L" (2 days) - 🔴
  - "size/XL" (>2 days - SPLIT!) - ⚫
Default: "Not sized"
Required: Yes (for sprint planning)
```

#### **2. Component Area**
```yaml
Field Name: "Component"
Type: Single select  
Options:
  - "🏛️ Domain" (Protected - needs approval)
  - "💼 Application" (Business logic)
  - "🌐 API" (Controllers, endpoints)
  - "🖥️ Frontend" (Angular components)
  - "🗃️ Database" (EF, migrations)
  - "🔐 Auth" (Security, permissions)
  - "📚 Docs" (Documentation)
  - "🛠️ Infrastructure" (DevOps, tools)
Default: "Not classified"
```

#### **3. Development Type**
```yaml
Field Name: "Dev Type"
Type: Single select
Options:
  - "🚀 Feature" (Backend + Frontend)
  - "🐛 Bug Fix" (Single layer)
  - "📋 Task" (Infrastructure, docs)
  - "🔧 Refactor" (Code quality)
  - "🧪 Testing" (Test infrastructure)
Default: "Feature"
```

#### **4. Sprint Assignment**
```yaml
Field Name: "Sprint"
Type: Single select
Options:
  - "Backlog" (Not planned)
  - "Sprint 1" (Weeks 1-2)
  - "Sprint 2" (Weeks 3-4)  
  - "Sprint 3" (Weeks 5-6)
  - "Sprint 4" (Weeks 7-8)
  - "Sprint 5" (Weeks 9-10)
  - "Current Sprint" (Active)
  - "Next Sprint" (Planned)
Auto-populate: Based on milestone dates
```

#### **5. Domain Risk Level**
```yaml
Field Name: "Domain Risk"
Type: Single select
Options:
  - "🟢 Safe" (No domain changes)
  - "🟡 Review" (Might affect domain)  
  - "🔴 High Risk" (Definite domain changes)
  - "⚫ Blocked" (Domain approval needed)
Default: "Safe"
Required: Yes (for domain protection)
```

### **Advanced Fields**

#### **6. Business Value**
```yaml
Field Name: "Business Value"
Type: Single select
Options:
  - "🔥 Critical" (Blocking/Security)
  - "🚨 High" (Core functionality)
  - "📊 Medium" (Important feature)
  - "📝 Low" (Nice to have)
Default: "Medium"
```

#### **7. Complexity**
```yaml
Field Name: "Complexity"
Type: Single select
Options:
  - "Simple" (Straightforward implementation)
  - "Moderate" (Some unknowns)
  - "Complex" (Multiple integrations)
  - "Research" (Needs investigation)
```

#### **8. Dependencies Count**
```yaml
Field Name: "Dependencies"
Type: Number
Description: "Number of issues this depends on"
Default: 0
```

---

## 📊 Views Configuration

### **View 1: 📋 Backlog Board**
```yaml
View Name: "Backlog"
Type: Board
Layout: Board
Group By: Status
Columns:
  - "📝 New" (Just created)
  - "🏷️ Ready" (Sized and planned)  
  - "🏃 In Progress" (Active development)
  - "👀 In Review" (PR created)
  - "✅ Done" (Merged and deployed)

Filters:
  - Status: not "Done"
  - Size: is not empty

Sort:
  - Priority: Descending
  - Business Value: Descending
  - Created Date: Ascending

Fields Visible:
  - Title, Assignees, Size, Component
  - Sprint, Domain Risk, Business Value
```

### **View 2: 🏃 Active Sprint**
```yaml
View Name: "Current Sprint"
Type: Board
Group By: Assignee

Filters:
  - Sprint: "Current Sprint"
  - Status: not "Done"

Sort:
  - Status: Custom order
  - Size: Ascending

Fields Visible:
  - Title, Size, Component, Dev Type
  - Domain Risk, Dependencies
  - Time Tracking (if available)

WIP Limits:
  - In Progress: 3 per assignee
  - In Review: 2 per assignee
```

### **View 3: 🎯 Milestones Overview**
```yaml
View Name: "Milestones"
Type: Table
Group By: Milestone

Filters:
  - Has milestone assigned

Sort:
  - Milestone Due Date: Ascending
  - Business Value: Descending

Fields Visible:
  - Title, Assignees, Status, Size
  - Component, Business Value, Sprint
  - Milestone, Due Date

Calculations:
  - Total Size per Milestone
  - Completion Percentage
  - Days Remaining
```

### **View 4: 👥 Team Workload**
```yaml
View Name: "Team Workload"
Type: Table
Group By: Assignee

Filters:
  - Status: "In Progress" or "Ready"
  - Assignee: has value

Sort:
  - Assignee: Ascending
  - Size: Descending

Fields Visible:
  - Title, Status, Size, Component
  - Sprint, Domain Risk
  - Estimated Hours (calculated)

Calculations:
  - Total Days per Assignee
  - WIP Count per Assignee
  - Capacity Utilization
```

### **View 5: 🛡️ Domain Protection Monitor**
```yaml
View Name: "Domain Protection"
Type: Table

Filters:
  - Domain Risk: "Review", "High Risk", or "Blocked"
  - Status: not "Done"

Sort:
  - Domain Risk: Custom (Blocked > High Risk > Review)
  - Created Date: Descending

Fields Visible:
  - Title, Assignees, Status
  - Domain Risk, Component  
  - Labels, Comments

Alerts:
  - Red highlighting for "Blocked" items
  - Yellow highlighting for "High Risk" items
```

### **View 6: 📈 Metrics Dashboard**
```yaml
View Name: "Metrics"
Type: Table

Filters:
  - Updated in last 30 days

Group By: Sprint

Calculations:
  - Velocity (Issues completed per sprint)
  - Cycle Time (In Progress → Done)
  - Lead Time (Created → Done)  
  - Defect Rate (Bugs / Total Issues)
  - Burndown Progress

Charts:
  - Velocity Trend Chart
  - Burndown Chart per Sprint
  - Issue Distribution by Component
  - Domain Risk Distribution
```

---

## 🤖 Automation Setup

### **GitHub Actions Integration**

#### **Auto-Project-Assignment**
```yaml
# .github/workflows/project-automation.yml
name: Project Management Automation

on:
  issues:
    types: [opened, labeled, assigned, closed]
  pull_request:
    types: [opened, closed, merged]

jobs:
  auto-assign-to-project:
    runs-on: ubuntu-latest
    steps:
      - name: Add issue to project
        uses: actions/add-to-project@v0.4.0
        with:
          project-url: https://github.com/users/andrekirst/projects/1
          github-token: ${{ secrets.GITHUB_TOKEN }}
          
  auto-set-fields:
    runs-on: ubuntu-latest  
    steps:
      - name: Set component based on labels
        run: |
          if [[ "${{ github.event.issue.labels }}" == *"domain"* ]]; then
            # Set Component to "Domain" and Domain Risk to "High Risk"
            echo "Setting domain-related fields"
          fi
          
  size-validation:
    runs-on: ubuntu-latest
    steps:
      - name: Check for size label
        run: |
          if [[ ! "${{ github.event.issue.labels }}" == *"size/"* ]]; then
            # Comment asking for size estimation
            echo "Please add size estimation label"
          fi
```

#### **Sprint Planning Automation**
```yaml
# Auto-assign issues to current sprint
sprint-assignment:
  runs-on: ubuntu-latest
  if: github.event.label.name == 'ready'
  steps:
    - name: Assign to current sprint
      run: |
        # Logic to assign to current sprint based on capacity
        # Check team workload and assign accordingly
```

### **Custom Automation Rules (GitHub Projects)**

#### **Rule 1: Size Validation**
```yaml
Trigger: Item added to project
Conditions:
  - Size field is empty
Actions:  
  - Add label "needs-sizing"
  - Set status to "Triage"
  - Add comment: "Please add size estimation before sprint planning"
```

#### **Rule 2: Domain Protection Alert**
```yaml
Trigger: Item labeled
Conditions:
  - Label contains "domain"
Actions:
  - Set Domain Risk to "High Risk"
  - Add label "needs-approval"
  - Assign to Domain Architect
  - Add comment: "⚠️ Domain changes detected. Review CLAUDE.md before proceeding."
```

#### **Rule 3: Sprint Capacity Management**
```yaml
Trigger: Item status changed to "In Progress"
Conditions:
  - Assignee workload > 6 days (3 issues)
Actions:
  - Add comment: "⚠️ Developer at capacity. Consider reassigning."
  - Set status back to "Ready"
  - Add label "workload-warning"
```

#### **Rule 4: Auto-Close on PR Merge**
```yaml
Trigger: Linked PR merged
Actions:
  - Set status to "Done"
  - Add comment: "✅ Completed via PR #{{pr.number}}"
  - Calculate actual time spent
  - Update velocity metrics
```

---

## 📊 Reporting und Analytics

### **Sprint Reports**

#### **Burndown Chart Setup**
```javascript
// Custom script to generate burndown charts
const sprintBurndown = {
  dataSource: "GitHub Projects API",
  metrics: [
    "remaining_story_points",
    "completed_story_points", 
    "days_remaining",
    "ideal_burndown_line"
  ],
  updateFrequency: "daily",
  visualization: "line_chart"
};
```

#### **Velocity Tracking**
```yaml
Velocity Metrics:
  - Issues completed per sprint
  - Story points completed per sprint  
  - Average cycle time
  - Lead time trends
  - Defect rate per sprint

Export Options:
  - Excel spreadsheet
  - CSV for analysis
  - JSON for API integration
  - Visual charts (Chart.js)
```

### **Team Performance Dashboards**

#### **Individual Performance**
- Issues completed per developer
- Average time per issue size
- Domain compliance score
- Code review participation
- Knowledge sharing contributions

#### **Team Health Metrics**  
- Sprint goal achievement rate
- WIP limit adherence
- Cycle time consistency
- Collaboration indicators
- Continuous improvement actions

---

## 🔧 Setup Instructions

### **Step 1: Create Project**
1. Navigate to repository
2. Go to **Projects** tab  
3. Click **"New project"**
4. Select **"Table"** view initially
5. Name: `KGV-v2 Development`

### **Step 2: Configure Fields**
1. Add custom fields as listed above
2. Set up field validation rules
3. Configure default values
4. Test field behavior

### **Step 3: Create Views**
1. Create each view listed above
2. Configure filters and sorting
3. Set up calculations
4. Test view performance

### **Step 4: Setup Automation**
1. Configure GitHub Actions workflows
2. Set up project automation rules
3. Test automation triggers
4. Monitor automation effectiveness

### **Step 5: Import Initial Issues**
1. Create milestone issues from `milestones-structure.md`
2. Apply appropriate labels and fields
3. Assign to milestones
4. Validate project structure

### **Step 6: Team Onboarding**
1. Share project URL with team
2. Provide training on using views
3. Establish daily workflow
4. Set up notification preferences

---

## 🎯 Best Practices

### **Daily Workflow**
- **Morning**: Check "Current Sprint" view, update status
- **During Work**: Move issues across board as progress
- **Evening**: Update progress, add time estimates
- **Blockers**: Immediately flag and communicate

### **Sprint Planning**
- Use "Backlog" view for planning  
- Check team capacity in "Team Workload" view
- Validate domain risks before committing
- Set realistic sprint goals

### **Monitoring**
- Daily check of "Domain Protection" view
- Weekly review of "Metrics Dashboard"
- Monthly analysis of velocity trends
- Quarterly retrospective with data

---

**Diese GitHub Projects Konfiguration ermöglicht eine strukturierte, automatisierte und transparente Projektverwaltung, die sowohl die 1-2 Tage Issue-Sizing als auch die Domain-Schutz-Anforderungen unterstützt.**