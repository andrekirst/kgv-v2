# 📋 KGV GitHub Project Manual Setup Guide

## 🎯 Übersicht

Diese detaillierte Anleitung führt Sie durch die manuelle Einrichtung des GitHub Projects für das KGV-System, falls die automatisierten Skripte nicht verwendet werden können.

---

## 🚀 Schnellstart-Optionen

### **Option 1: Automatisierte Scripts (Empfohlen)**
```bash
# 1. GitHub CLI Authentifizierung
gh auth login

# 2. Projekt und Milestones erstellen
./scripts/setup-github-project.sh

# 3. Issues erstellen (Sample)
./scripts/create-milestone-issues.sh

# Oder alle 90 Issues erstellen
./scripts/create-milestone-issues.sh --full
```

### **Option 2: Manuelle Einrichtung (Diese Anleitung)**
Folgen Sie den detaillierten Schritten unten für vollständige manuelle Kontrolle.

---

## 📋 Schritt 1: GitHub Project erstellen

### **1.1 Navigation zum Repository**
1. Öffnen Sie https://github.com/andrekirst/kgv-v2
2. Klicken Sie auf den **"Projects"** Tab oben
3. Klicken Sie auf **"Link a project"** → **"Create new project"**

### **1.2 Projekt-Konfiguration**
```yaml
Project Settings:
  Name: "KGV-v2 Development"
  Description: "Comprehensive project management for KGV system migration from legacy VB to modern .NET 9 + Angular"
  Visibility: Private (Repository access)
  Template: "Feature development" (dann anpassen)
```

### **1.3 Initial View Setup**
1. Starten Sie mit der **"Table"** Ansicht
2. Fügen Sie eine **"Board"** Ansicht später hinzu
3. Konfigurieren Sie die Default-Spalten:
   - 📝 New
   - 🏷️ Ready  
   - 🏃 In Progress
   - 👀 In Review
   - ✅ Done

---

## 🏷️ Schritt 2: Labels erstellen

### **2.1 Navigieren zu Repository Labels**
1. Gehen Sie zu: https://github.com/andrekirst/kgv-v2/labels
2. Klicken Sie **"New label"** für jeden Label unten

### **2.2 Size Labels**
```yaml
size/XS:
  Color: #00ff00 (Grün)
  Description: "0.5 day - Trivial tasks"

size/S:
  Color: #ffff00 (Gelb)  
  Description: "1 day - Simple features"

size/M:
  Color: #ff8800 (Orange)
  Description: "1.5 days - Standard features"

size/L:
  Color: #ff0000 (Rot)
  Description: "2 days - Complex features"

size/XL:
  Color: #000000 (Schwarz)
  Description: ">2 days - SPLIT REQUIRED!"
```

### **2.3 Type Labels**
```yaml
feature:
  Color: #0075ca (Blau)
  Description: "New functionality"

bug:
  Color: #d73a4a (Rot)
  Description: "Something isn't working"

task:
  Color: #7057ff (Lila)
  Description: "Infrastructure/maintenance task"

documentation:
  Color: #0052cc (Dunkelblau)
  Description: "Improvements or additions to docs"

refactor:
  Color: #fbca04 (Gelb)
  Description: "Code quality improvement"
```

### **2.4 Priority Labels**
```yaml
priority/critical:
  Color: #b60205 (Dunkelrot)
  Description: "🔥 Critical - Blocking issues"

priority/high:
  Color: #d93f0b (Rot-Orange)
  Description: "🚨 High - Important features"

priority/medium:
  Color: #fbca04 (Gelb)
  Description: "📋 Medium - Standard priority"

priority/low:
  Color: #0e8a16 (Grün)
  Description: "📝 Low - Nice to have"
```

### **2.5 Component Labels**
```yaml
domain:
  Color: #5319e7 (Lila)
  Description: "🏛️ Domain Layer (PROTECTED)"

application:
  Color: #1d76db (Blau)
  Description: "💼 Application Layer"

api:
  Color: #0052cc (Dunkelblau)
  Description: "🌐 Web API Controllers"

frontend:
  Color: #d4c5f9 (Hellila)
  Description: "🖥️ Angular Components"

database:
  Color: #c2e0c6 (Hellgrün)
  Description: "🗃️ Database/Migrations"

auth:
  Color: #f9d0c4 (Hellorange)
  Description: "🔐 Authentication/Authorization"
```

### **2.6 Domain Risk Labels**
```yaml
domain-safe:
  Color: #28a745 (Grün)
  Description: "🟢 Safe - No domain changes"

domain-review:
  Color: #ffc107 (Gelb)
  Description: "🟡 Review - Might affect domain"

domain-risk:
  Color: #dc3545 (Rot)
  Description: "🔴 High Risk - Domain changes"

domain-blocked:
  Color: #6c757d (Grau)
  Description: "⚫ Blocked - Needs approval"
```

### **2.7 Workflow Labels**
```yaml
needs-sizing:
  Color: #f9c2ff (Rosa)
  Description: "Needs size estimation"

needs-triage:
  Color: #d876cc (Pink)
  Description: "Needs initial review"

ready:
  Color: #c5f015 (Hellgrün)
  Description: "Ready for development"

in-progress:
  Color: #0075ca (Blau)
  Description: "Currently being worked on"

blocked:
  Color: #b60205 (Rot)
  Description: "Blocked by external dependency"
```

---

## 🎯 Schritt 3: Milestones erstellen

### **3.1 Navigation zu Milestones**
1. Gehen Sie zu: https://github.com/andrekirst/kgv-v2/milestones
2. Klicken Sie **"New milestone"**

### **3.2 Milestone-Konfiguration**

#### **Milestone 1: 🏗️ M1: Foundation**
```yaml
Title: "🏗️ M1: Foundation"
Description: "Technical foundation: Domain model, authentication, basic API endpoints. 20 issues, 40 development days."
Due Date: [Berechnet: Startdatum + 70 Tage]
```

#### **Milestone 2: 🌟 M2: Core Features**
```yaml
Title: "🌟 M2: Core Features"
Description: "Main functionality: Citizen portal, admin portal, waiting list management. 30 issues, 60 development days."
Due Date: [M1 Ende + 84 Tage]
```

#### **Milestone 3: 🚀 M3: Advanced Features**
```yaml
Title: "🚀 M3: Advanced Features"
Description: "Advanced workflows, reporting, mobile optimization, integrations. 25 issues, 50 development days."
Due Date: [M2 Ende + 70 Tage]
```

#### **Milestone 4: 🎯 M4: Production Ready**
```yaml
Title: "🎯 M4: Production Ready"
Description: "Security hardening, performance optimization, deployment, go-live preparation. 15 issues, 30 development days."
Due Date: [M3 Ende + 56 Tage]
```

---

## 📊 Schritt 4: Project Custom Fields

### **4.1 Field Setup**
1. Gehen Sie zu Ihrem GitHub Project
2. Klicken Sie auf das **Settings** (Zahnrad) Icon
3. Wählen Sie **"Fields"** im linken Menü
4. Klicken Sie **"New field"** für jedes Field unten

### **4.2 Custom Fields Configuration**

#### **Field 1: Size**
```yaml
Field Name: "Size"
Type: "Single select"
Options:
  - XS (🟢 0.5 day)
  - S (🟡 1 day)
  - M (🟠 1.5 days)
  - L (🔴 2 days)  
  - XL (⚫ >2 days - SPLIT!)
Default: "Not sized"
Required: No (but strongly recommended)
```

#### **Field 2: Component**
```yaml
Field Name: "Component"
Type: "Single select"
Options:
  - 🏛️ Domain
  - 💼 Application
  - 🌐 API
  - 🖥️ Frontend
  - 🗃️ Database
  - 🔐 Auth
  - 📚 Docs
  - 🛠️ Infrastructure
Default: "Not classified"
```

#### **Field 3: Sprint**
```yaml
Field Name: "Sprint"
Type: "Single select"
Options:
  - Backlog
  - Sprint 1
  - Sprint 2
  - Sprint 3
  - Sprint 4
  - Sprint 5
  - Current Sprint
  - Next Sprint
Default: "Backlog"
```

#### **Field 4: Domain Risk**
```yaml
Field Name: "Domain Risk"
Type: "Single select"
Options:
  - 🟢 Safe
  - 🟡 Review
  - 🔴 High Risk
  - ⚫ Blocked
Default: "Safe"
Required: Yes (für Domain Protection)
```

#### **Field 5: Business Value**
```yaml
Field Name: "Business Value"  
Type: "Single select"
Options:
  - 🔥 Critical
  - 🚨 High
  - 📊 Medium
  - 📝 Low
Default: "Medium"
```

#### **Field 6: Complexity**
```yaml
Field Name: "Complexity"
Type: "Single select"
Options:
  - Simple
  - Moderate  
  - Complex
  - Research
Default: "Simple"
```

---

## 📋 Schritt 5: Project Views erstellen

### **5.1 View 1: Backlog Board**
1. Klicken Sie **"New view"** 
2. Wählen Sie **"Board"**
3. Name: `Backlog`

**Konfiguration:**
```yaml
Group by: Status
Columns:
  - 📝 New
  - 🏷️ Ready
  - 🏃 In Progress  
  - 👀 In Review
  - ✅ Done

Filters:
  - Status: not "Done"
  - Size: is not empty

Sort:
  - Priority: Descending
  - Business Value: Descending
  - Created: Ascending

Visible Fields:
  - Title, Assignees, Size, Component
  - Sprint, Domain Risk, Business Value
```

### **5.2 View 2: Active Sprint**
1. **New view** → **Board**
2. Name: `Current Sprint`

**Konfiguration:**
```yaml
Group by: Assignee

Filters:
  - Sprint: "Current Sprint"  
  - Status: not "Done"

Sort:
  - Status: Custom order
  - Size: Ascending

WIP Limits:
  - In Progress: 3 per assignee
  - In Review: 2 per assignee

Visible Fields:
  - Title, Size, Component, Dev Type
  - Domain Risk, Dependencies
```

### **5.3 View 3: Milestones Overview**
1. **New view** → **Table**
2. Name: `Milestones`

**Konfiguration:**
```yaml
Group by: Milestone

Filters:
  - Has milestone assigned

Sort:
  - Milestone Due Date: Ascending
  - Business Value: Descending

Visible Fields:
  - Title, Assignees, Status, Size
  - Component, Business Value, Sprint
  - Milestone, Due Date

Show calculations:
  - Count per Milestone
  - Sum of Size estimates
```

### **5.4 View 4: Domain Protection Monitor**
1. **New view** → **Table**
2. Name: `Domain Protection`

**Konfiguration:**
```yaml
Filters:
  - Domain Risk: "Review", "High Risk", or "Blocked"
  - Status: not "Done"

Sort:
  - Domain Risk: Custom (Blocked > High Risk > Review)
  - Created: Descending

Visible Fields:
  - Title, Assignees, Status
  - Domain Risk, Component
  - Labels, Comments

Highlighting:
  - Red: Domain Risk = "Blocked"
  - Yellow: Domain Risk = "High Risk"
```

### **5.5 View 5: Team Workload**
1. **New view** → **Table**
2. Name: `Team Workload`

**Konfiguration:**
```yaml
Group by: Assignee

Filters:
  - Status: "In Progress" or "Ready"
  - Assignee: has value

Sort:
  - Assignee: Ascending
  - Size: Descending

Visible Fields:
  - Title, Status, Size, Component
  - Sprint, Domain Risk

Show calculations:
  - Count per Assignee (WIP)
  - Sum of Size per Assignee (Workload)
```

---

## 🤖 Schritt 6: GitHub Actions Setup

### **6.1 Workflow File erstellen**
Die GitHub Actions Datei wurde bereits unter `.github/workflows/project-automation.yml` erstellt.

### **6.2 Project Number aktualisieren**
1. Öffnen Sie Ihr GitHub Project
2. Schauen Sie in der URL nach der Projektnummer
3. URL Format: `https://github.com/users/andrekirst/projects/[NUMBER]`
4. Aktualisieren Sie `PROJECT_NUMBER` in der Workflow-Datei

### **6.3 Workflow testen**
1. Erstellen Sie ein Test-Issue
2. Fügen Sie Labels hinzu
3. Überprüfen Sie, ob Automation funktioniert
4. Schauen Sie in **Actions** Tab für Logs

---

## 📋 Schritt 7: Issues erstellen

### **7.1 Issue Template nutzen**
1. Gehen Sie zu: **Issues** → **New issue**
2. Wählen Sie das entsprechende Template:
   - 🚀 Feature Request (Backend + Frontend)
   - 🐛 Bug Report (Single Layer)
   - 📋 Task (Infrastructure/Docs)

### **7.2 Milestone 1 Foundation Issues**

#### **Sample Issue Creation**
Hier ist ein Beispiel für die erste kritische Issue:

**Issue Title:** `feat: Domain Value Objects - Aktenzeichen, PersonData, Address`

**Labels:** `feature`, `size/L`, `domain`, `priority/critical`, `domain-review`

**Milestone:** `🏗️ M1: Foundation`

**Body:**
```markdown
## 🎯 Ziel
Domain Model Foundation mit den drei Kern Value Objects implementieren.

## 📋 Backend Tasks (2 Tage)
- [ ] Aktenzeichen Value Object implementieren
  - Pattern-Validation: `^(32\.2|33\.2)\s+(\d+)\s+(\d{4})$`
  - Factory Methods: Create() und Parse()
  - Business Rules: ListType-Mapping
- [ ] PersonData Value Object erstellen
  - Validation: Min 18 Jahre, Email-Format
  - Computed Properties: FullName
  - Immutability gewährleisten
- [ ] Address Value Object implementieren
  - Deutsche PLZ-Validierung (5 Ziffern)
  - Normalisierte Datenstruktur
  - FullAddress computed property
- [ ] Unit Tests für alle Value Objects (>90% Coverage)
- [ ] Domain Guard Validation durchlaufen

## ✅ Acceptance Criteria
- ✅ Alle Value Objects sind immutable (sealed records)
- ✅ Pattern-Matching für Aktenzeichen funktioniert
- ✅ Validierung wirft korrekte Exceptions
- ✅ Unit Tests decken alle Business Rules ab
- ✅ Keine Änderungen an CLAUDE.md-geschützten Bereichen

## ⚠️ Domain Protection
⚠️ Diese Issue betrifft geschützte Domain-Bereiche. Vor der Implementierung:
1. CLAUDE.md Domain Protection Contract lesen
2. Domain-Architect Review einholen
3. Keine Änderung der existierenden Business Rules

## 📊 Aufwand
**Geschätzte Zeit**: 2 Tage (16 Stunden)
**Komplexität**: Hoch (Domain-kritisch)
**Abhängigkeiten**: Keine
```

### **7.3 Project Fields ausfüllen**
Nach der Issue-Erstellung:
1. **Size**: L (2 days)
2. **Component**: 🏛️ Domain
3. **Domain Risk**: 🔴 High Risk
4. **Business Value**: 🔥 Critical
5. **Complexity**: Complex
6. **Sprint**: Sprint 1

---

## ✅ Schritt 8: Testing und Validation

### **8.1 Project Setup validieren**
- [ ] Alle Labels sind erstellt
- [ ] Alle 4 Milestones existieren
- [ ] Project hat alle Custom Fields
- [ ] Alle 5 Views sind konfiguriert
- [ ] GitHub Actions läuft ohne Fehler

### **8.2 Issue Workflow testen**
1. **Issue erstellen** ohne Size Label → Automation sollte `needs-sizing` hinzufügen
2. **Domain Label hinzufügen** → Automation sollte Domain Warning erstellen
3. **size/XL hinzufügen** → Automation sollte Split Warning erstellen
4. **PR erstellen** für Issue → Automation sollte Status auf "In Review" setzen

### **8.3 Team Onboarding**
- [ ] Team-Mitglieder zu Repository einladen
- [ ] Project Workflow erklären
- [ ] Views und deren Verwendung zeigen
- [ ] Issue-Erstellungsprozess trainieren
- [ ] Automation-Rules demonstrieren

---

## 🎯 Schritt 9: Daily Workflow etablieren

### **9.1 Developer Workflow**
```bash
Morgendliche Routine:
1. "Current Sprint" View öffnen
2. In Progress Issues checken  
3. Blockers identifizieren
4. Work-in-Progress Status aktualisieren

Während der Entwicklung:
1. Issues von "Ready" → "In Progress" verschieben
2. PR erstellen mit Issue-Referenz
3. PR Review durchführen
4. Nach Merge automatisch "Done"

Abendliche Updates:
1. Progress in Issues kommentieren
2. Blockers kommunizieren
3. Nächste Aufgaben planen
```

### **9.2 Sprint Planning Workflow**
```bash
Wöchentliches Sprint Planning:
1. "Backlog" View für Issue-Auswahl nutzen
2. Team Capacity in "Team Workload" prüfen
3. Domain Risks in "Domain Protection" View checken
4. Issues zu "Current Sprint" zuweisen
5. Sprint-Ziele definieren und dokumentieren

Sprint Review:
1. "Milestones" View für Progress-Check
2. Velocity in completed Issues analysieren
3. Impediments und Lessons Learned sammeln
4. Nächste Sprint-Kapazität planen
```

---

## 📊 Erfolgsmessung

### **Quantitative Metriken**
- **Velocity**: Completed Story Points per Sprint
- **Cycle Time**: Ready → Done Duration
- **Lead Time**: Created → Done Duration
- **WIP Limits**: Adherence to 3 In Progress per Developer
- **Domain Compliance**: Zero domain guard violations

### **Qualitative Indikatoren**
- **Team Satisfaction**: Regular Retrospective Feedback
- **Process Efficiency**: Reduced Sprint Planning Time
- **Quality Metrics**: Lower Bug Rate, Higher Test Coverage
- **Stakeholder Confidence**: Regular Demo Acceptance

---

## 🚀 Nächste Schritte

### **Sofort (Woche 1)**
- [ ] GitHub Project komplett eingerichtet
- [ ] Team in Workflow trainiert
- [ ] Erste 5 Foundation Issues erstellt
- [ ] Sprint 1 geplant

### **Kurzfristig (Wochen 2-4)**
- [ ] Alle Milestone 1 Issues erstellt (20 total)
- [ ] Automation Rules getestet und angepasst
- [ ] Team Velocity baseline etabliert
- [ ] Domain Protection Workflow validiert

### **Mittelfristig (Monate 2-3)**
- [ ] Milestone 2 + 3 Issues detailliert geplant
- [ ] Advanced Project Views und Reports
- [ ] Integration mit externen Tools (Zeit-Tracking)
- [ ] Automated Metrics Dashboard

---

**🎯 Diese umfassende Anleitung stellt sicher, dass das KGV-Projekt professionell organisiert wird und alle Anforderungen für effiziente 1-2 Tage Issue-Entwicklung erfüllt sind!**