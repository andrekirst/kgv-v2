# 🔄 KGV Git Workflow Guide

## 🎯 Übersicht

Diese Anweisung definiert den Git-Workflow für das KGV-Projekt mit Fokus auf Branch-Protection, Issue-Tracking und konventionelle Commit-Messages.

---

## 🛡️ Branch Protection Strategy

### **Main Branch Protection**

Der `main`-Branch ist **VOLLSTÄNDIG GESCHÜTZT**:

- ❌ **Keine direkten Commits** auf main erlaubt
- ❌ **Keine Force-Push** Operationen
- ✅ **Nur Pull Requests** können main modifizieren
- ✅ **Mindestens 1 Review** erforderlich vor Merge
- ✅ **Domain Guard Checks** müssen bestehen
- ✅ **CI/CD Pipeline** muss erfolgreich sein

### **Branch Protection Konfiguration**

```json
{
  "required_status_checks": {
    "strict": true,
    "contexts": ["Domain Guard - Compliance Check", "CI/Build", "Tests"]
  },
  "enforce_admins": true,
  "required_pull_request_reviews": {
    "required_approving_review_count": 1,
    "dismiss_stale_reviews": true,
    "require_code_owner_reviews": true
  },
  "restrictions": null
}
```

---

## 🌿 Issue-basierter Branch Workflow

### **1. Issue-First Development**

#### **Schritt 1: Issue analysieren**

```markdown
📋 Issue #123: Waiting List Export Feature

**Labels**: enhancement, feature, waiting-list
**Assignee**: @developer  
**Sprint**: Sprint-12
**Estimation**: 8 Story Points
```

#### **Schritt 2: Branch erstellen**

**Naming Convention**: `[type]/[issue-number]-[short-description]`

```bash
# Feature Branch
git checkout -b feature/123-waiting-list-export

# Bug Fix Branch
git checkout -b fix/124-aktenzeichen-validation

# Hotfix Branch
git checkout -b hotfix/125-security-patch

# Documentation Branch
git checkout -b docs/126-api-documentation
```

#### **Schritt 3: Branch dem Issue zuordnen**

```bash
# In GitHub: Issue #123
# Development Section → "Create a branch"
# Branch name: feature/123-waiting-list-export
# Repository: kgv-v2
```

### **2. Branch Types & Naming**

| Branch Type | Pattern                          | Beispiel                          | Verwendung           |
| ----------- | -------------------------------- | --------------------------------- | -------------------- |
| `feature/`  | `feature/[issue]-[description]`  | `feature/123-waiting-list-export` | Neue Features        |
| `fix/`      | `fix/[issue]-[description]`      | `fix/124-validation-bug`          | Bug Fixes            |
| `hotfix/`   | `hotfix/[issue]-[description]`   | `hotfix/125-security-patch`       | Kritische Fixes      |
| `docs/`     | `docs/[issue]-[description]`     | `docs/126-api-docs`               | Dokumentation        |
| `refactor/` | `refactor/[issue]-[description]` | `refactor/127-cleanup-service`    | Code Refactoring     |
| `test/`     | `test/[issue]-[description]`     | `test/128-unit-tests`             | Test-Implementierung |

### **3. Issue-Branch-Verknüpfung**

```bash
# Branch erstellen und Issue verknüpfen
git checkout -b feature/123-waiting-list-export

# Ersten Commit mit Issue-Referenz
git commit -m "feat(export): initialize waiting list export feature

- Add basic export controller structure
- Configure dependency injection for export service
- Reference domain objects without modification

Closes #123"
```

---

## 📝 Conventional Commits Standard

### **Commit Message Format**

```
<type>(<scope>): <description>

[optional body]

[optional footer with issue references]
```

### **Commit Types**

| Type       | Verwendung            | Beispiel                                        |
| ---------- | --------------------- | ----------------------------------------------- |
| `feat`     | Neue Features         | `feat(export): add PDF export functionality`    |
| `fix`      | Bug Fixes             | `fix(validation): correct Aktenzeichen pattern` |
| `docs`     | Dokumentation         | `docs(api): update swagger annotations`         |
| `style`    | Formatierung          | `style(format): fix code formatting`            |
| `refactor` | Code-Umstrukturierung | `refactor(service): extract common validation`  |
| `test`     | Tests                 | `test(unit): add application service tests`     |
| `chore`    | Build/Config          | `chore(deps): update Angular to v18`            |
| `perf`     | Performance           | `perf(query): optimize waiting list ranking`    |
| `ci`       | CI/CD                 | `ci(github): add domain guard workflow`         |

### **Scope Guidelines**

| Scope        | Bereich           | Beispiel                                  |
| ------------ | ----------------- | ----------------------------------------- |
| `domain`     | Domain Layer      | `feat(domain): add new value object`      |
| `app`        | Application Layer | `feat(app): implement CQRS handler`       |
| `api`        | Web API           | `feat(api): add export endpoints`         |
| `ui`         | Frontend          | `feat(ui): create export dialog`          |
| `db`         | Database          | `feat(db): add export table migration`    |
| `auth`       | Authentication    | `fix(auth): handle token expiry`          |
| `export`     | Export Feature    | `feat(export): add Excel generation`      |
| `validation` | Validation Logic  | `fix(validation): improve error messages` |

### **Issue-Referenzen in Commits**

```bash
# Issue schließen
git commit -m "feat(export): complete waiting list export

Implements Excel and PDF export functionality
with filtering by Gemarkung and date range.

Closes #123"

# Issue referenzieren (nicht schließen)
git commit -m "feat(export): add export service interface

Basic structure for export functionality.
Additional work needed for PDF generation.

Relates to #123"

# Mehrere Issues
git commit -m "fix(validation): improve error handling

Fixes validation issues in multiple components.

Fixes #124, #125
Relates to #123"
```

---

## 🔄 Entwicklungsworkflow

### **Täglicher Workflow**

#### **1. Arbeitsbeginn**

```bash
# Aktuellen main Branch abrufen
git checkout main
git pull origin main

# Feature Branch erstellen
git checkout -b feature/123-waiting-list-export

# Issue in GitHub dem Branch zuordnen
# GitHub → Issue #123 → Development → Create branch
```

#### **2. Entwicklungsarbeit**

```bash
# Häufige, kleine Commits
git add .
git commit -m "feat(export): add export controller structure

Initial setup for waiting list export endpoints.

Relates to #123"

# Regelmäßig pushen (Backup)
git push origin feature/123-waiting-list-export

# Weitere kleine Commits
git commit -m "feat(export): implement Excel service

Add ExcelExportService with basic functionality.

Relates to #123"

git push origin feature/123-waiting-list-export
```

#### **3. Domain Guard Validation**

```bash
# Vor jedem Commit: Domain Guard Check
.domain-guard/pre-commit-hook.sh

# Compliance Report generieren
.domain-guard/monitor.sh

# Bei Violations: Fixen und erneut prüfen
```

#### **4. Pull Request Erstellung**

```bash
# Finalen Commit mit Issue-Closure
git commit -m "feat(export): complete waiting list export feature

✅ Implemented Excel export with filtering
✅ Added PDF export functionality
✅ Created responsive UI components
✅ Added comprehensive unit tests
✅ Domain compliance verified

Closes #123"

git push origin feature/123-waiting-list-export
```

### **Pull Request Template**

```markdown
## 🎯 Feature: Waiting List Export (#123)

### 📋 Summary

Implements comprehensive export functionality for waiting lists with Excel and PDF output options.

### 🛡️ Domain Compliance

- [ ] No domain model modifications
- [ ] Domain guard validation passed
- [ ] Business rules implemented as specified
- [ ] Event schemas unchanged

### 🔗 Related Issues

Closes #123
Relates to #45 (export infrastructure)

### 🧪 Testing

- [ ] Unit tests added/updated
- [ ] Integration tests cover main scenarios
- [ ] Manual testing completed
- [ ] Domain compliance verified

### 📊 Changes Made

- **Application Layer**: ExportWaitingListQuery/Handler
- **Infrastructure Layer**: Excel/PDF export services
- **Presentation Layer**: Export controller and endpoints
- **Frontend**: Export dialog and progress indicators

### 🔍 Verification Steps

1. Domain guard validation: ✅ PASSED
2. All tests pass: ✅ PASSED
3. Feature works as expected: ✅ PASSED
4. No performance degradation: ✅ PASSED

### 📷 Screenshots

[Include relevant UI screenshots]

### 📝 Migration Notes

No database migrations required.

---

_🤖 This PR follows the KGV Git Workflow and Domain Protection guidelines._
```

---

## 📦 Commit Strategien

### **Kleine, Häufige Commits (Preferred)**

```bash
# ✅ GOOD: Atomic Commits
git commit -m "feat(export): add export controller"
git commit -m "feat(export): implement Excel service"
git commit -m "feat(export): add PDF generation"
git commit -m "test(export): add unit tests"
git commit -m "docs(export): update API documentation"

# Vorteile:
# - Einfaches Debugging
# - Granulare History
# - Besseres Code Review
# - Backup-Schutz bei Crash
```

### **Große Commits (Vermeiden)**

```bash
# ❌ BAD: Monolithic Commit
git commit -m "feat(export): implement complete export functionality

- Add controller, service, tests, documentation
- Fix validation issues
- Update UI components
- Refactor existing code"

# Nachteile:
# - Schwieriges Debugging
# - Komplexe Code Reviews
# - Verlust bei Crash möglich
```

### **Commit Häufigkeit**

- 🕐 **Mindestens alle 2 Stunden** committen und pushen
- 🔄 **Bei Funktionsabschluss** immer committen
- 💾 **Vor Pausen/Feierabend** immer pushen
- 🧪 **Nach erfolgreichen Tests** committen
- 🛡️ **Nach Domain Guard Check** committen

---

## 🚫 Branch Protection Rules

### **GitHub Branch Protection Setup**

```bash
# Repository Settings → Branches → Add rule

Branch name pattern: main

☑️ Require a pull request before merging
  ☑️ Require approvals (1)
  ☑️ Dismiss stale reviews when new commits are pushed
  ☑️ Require review from code owners

☑️ Require status checks to pass before merging
  ☑️ Require branches to be up to date before merging

  Required status checks:
  - Domain Guard - Compliance Check
  - CI / Build (.NET)
  - CI / Test (Unit Tests)
  - CI / Test (Integration Tests)

☑️ Require conversation resolution before merging
☑️ Require signed commits
☑️ Require linear history
☑️ Include administrators (recommended)
☑️ Allow force pushes (disabled)
☑️ Allow deletions (disabled)
```

### **Pre-commit Hook Integration**

```bash
# .git/hooks/pre-commit (automatisch installiert)
#!/bin/bash
echo "🛡️ Running Domain Guard validation..."

# Domain Guard Check
.domain-guard/pre-commit-hook.sh

# Conventional Commit Check
.domain-guard/commit-msg-check.sh

# Exit if violations found
if [ $? -ne 0 ]; then
    echo "❌ Commit blocked due to violations"
    exit 1
fi

echo "✅ All checks passed - commit allowed"
```

---

## 🔍 Code Review Process

### **Review Checklist**

```markdown
## 📋 Code Review Checklist

### 🛡️ Domain Compliance

- [ ] No domain model modifications
- [ ] Business rules implemented correctly
- [ ] Domain guard validation passed
- [ ] Event schemas unchanged

### 💻 Code Quality

- [ ] Code follows project conventions
- [ ] Appropriate error handling
- [ ] Performance considerations addressed
- [ ] Security best practices followed

### 🧪 Testing

- [ ] Unit tests added/updated
- [ ] Integration tests cover scenarios
- [ ] Manual testing documented
- [ ] Edge cases considered

### 📝 Documentation

- [ ] Code comments where needed
- [ ] API documentation updated
- [ ] README changes if applicable
- [ ] CHANGELOG updated

### 🔗 Issue Tracking

- [ ] Commit messages follow conventions
- [ ] Issue properly referenced
- [ ] Acceptance criteria met
- [ ] Related issues linked
```

### **Review Guidelines**

- 🎯 **Focus auf Funktionalität** statt Style-Diskussionen
- 🛡️ **Domain-Schutz** hat oberste Priorität
- 💬 **Konstruktives Feedback** mit Verbesserungsvorschlägen
- ⚡ **Schnelle Reviews** (max. 24h Response-Zeit)
- ✅ **Approve nur bei vollständiger Compliance**

---

## 🚨 Emergency Procedures

### **Hotfix Workflow**

```bash
# Kritischer Bug in Production
git checkout main
git pull origin main

# Hotfix Branch erstellen
git checkout -b hotfix/urgent-security-fix

# Schnelle Fixes implementieren
git commit -m "fix(security): patch authentication vulnerability

Critical security fix for token validation.

Fixes #URGENT-001"

# Sofort Push und PR
git push origin hotfix/urgent-security-fix

# PR mit "URGENT" Label erstellen
# Expedited Review Process
```

### **Branch Recovery**

```bash
# Falls Branch verloren/korrupt
git reflog  # Finde verlorene Commits
git checkout -b recovery-branch [commit-hash]

# Oder von GitHub
git fetch origin
git checkout -b recovered-branch origin/lost-branch
```

### **Main Branch Corruption**

```bash
# Falls main Branch Probleme hat
git checkout -b emergency-main [last-known-good-commit]

# Koordination mit Team erforderlich
# GitHub Admin kontaktieren
```

---

## 📊 Workflow Monitoring

### **Branch Analytics**

```bash
# Aktuelle Branches anzeigen
git branch -a

# Branch-Aktivität tracken
git for-each-ref --format='%(refname:short) %(committerdate)' refs/remotes

# Issue-Branch-Mapping prüfen
gh issue list --state open --json number,title,assignees
```

### **Commit Quality Metrics**

- 📊 **Durchschnittliche Commit-Größe**: < 100 Zeilen
- 🕐 **Commit-Häufigkeit**: > 3 pro Tag
- 🔗 **Issue-Verlinkung**: 100% aller Commits
- 🛡️ **Domain Compliance**: 100% aller PRs
- ⚡ **Review-Zeit**: < 24 Stunden

### **Success Indicators**

- ✅ **Zero direkter main-Commits**
- ✅ **100% Issue-basierte Branches**
- ✅ **Konventionelle Commit Messages**
- ✅ **Regelmäßige Push-Zyklen**
- ✅ **Domain Guard Compliance**

---

## 🛠️ Tools & Automation

### **Git Hooks**

- **pre-commit**: Domain Guard + Commit Message Validation
- **commit-msg**: Conventional Commit Format Check
- **pre-push**: Branch Protection Validation

### **GitHub Actions**

- **Domain Guard Workflow**: Automatische Compliance-Checks
- **Branch Protection**: Verhindert unerlaubte main-Commits
- **Issue-Branch-Sync**: Automatische Verknüpfung

### **CLI Tools**

```bash
# GitHub CLI für Issue-Management
gh issue create --title "Export Feature" --body "Description"
gh issue develop 123 --checkout

# Git Aliases für Effizienz
git config alias.feature '!f() { git checkout -b feature/$1; }; f'
git config alias.fix '!f() { git checkout -b fix/$1; }; f'
```

---

## 📚 Quick Reference

### **Häufige Kommandos**

```bash
# Neues Feature starten
git checkout main && git pull origin main
git checkout -b feature/123-new-feature

# Commit mit Issue-Referenz
git commit -m "feat(scope): description

Relates to #123"

# Domain Guard Check
.domain-guard/pre-commit-hook.sh

# Pull Request erstellen
gh pr create --title "Feature: New Feature (#123)" --body-file PR_TEMPLATE.md

# Branch nach Merge aufräumen
git checkout main && git pull origin main
git branch -d feature/123-new-feature
git push origin --delete feature/123-new-feature
```

### **Troubleshooting**

- **Domain Guard Fehler**: Siehe CLAUDE.md für Lösungsansätze
- **Commit Message Fehler**: Format prüfen gegen Conventional Commits
- **Branch Protection**: PR erstellen statt direkter Push
- **Merge Conflicts**: Basis-Branch aktualisieren vor Merge

---

**🎯 Merke**: Ein sauberer Git-Workflow ist die Basis für effektive Zusammenarbeit und hohe Code-Qualität!

**🚀 Happy Git-ing!** 🌿
