# 📏 KGV Issue-Sizing Guidelines

## 🎯 Grundprinzipien

Diese Guidelines gewährleisten, dass alle Issues innerhalb von **maximal 1-2 Tagen** von einem Entwickler vollständig implementiert werden können, einschließlich Backend und Frontend für Features.

---

## 📐 Sizing-System Übersicht

### **Size Labels und Zeiteinteilung**

| Label | Zeit | Beschreibung | Beispiele |
|-------|------|-------------|-----------|
| `size/XS` | 🟢 0.5 Tag | Triviale Tasks, kleine Fixes | Config-Änderungen, Dokumentation |
| `size/S` | 🟡 1 Tag | Einfache Features/Fixes | Basic CRUD, einfache UI-Komponenten |
| `size/M` | 🟠 1.5 Tage | Standard Features | Feature mit Backend+Frontend |
| `size/L` | 🔴 2 Tage | Komplexe Features | Multi-layer Features, Integration |
| `size/XL` | ⚫ >2 Tage | **SPLIT REQUIRED!** | Zu groß - muss aufgeteilt werden |

---

## 🏗️ Feature Issue Sizing (Backend + Frontend)

### **Size/S (1 Tag) - Einfache Features**

#### **Charakteristika:**
- Einfache CRUD-Operationen
- Minimale Business Logic
- Standard UI-Komponenten
- Keine komplexen Integrationen

#### **Backend Aufwand (0.5 Tage):**
- [ ] 1 Controller mit 2-3 Endpoints
- [ ] 1 Command/Query mit Handler
- [ ] Basic Validation
- [ ] Simple Unit Tests

#### **Frontend Aufwand (0.5 Tage):**
- [ ] 1 Angular Component
- [ ] Basic Form oder List View
- [ ] Service für API-Integration
- [ ] Component Tests

#### **Beispiele:**
- `feat: Display Application Status - Simple status badge component`
- `feat: User Profile Edit - Basic profile form`
- `feat: Export CSV - Simple data export`

---

### **Size/M (1.5 Tage) - Standard Features**

#### **Charakteristika:**
- Moderate Business Logic
- Custom UI-Komponenten erforderlich
- State Management nötig
- Integration zwischen mehreren Bereichen

#### **Backend Aufwand (1 Tag):**
- [ ] 1-2 Controller mit 4-6 Endpoints
- [ ] 2-3 Commands/Queries mit Validation
- [ ] Domain Event Publishing
- [ ] Comprehensive Unit Tests
- [ ] Basic Integration Tests

#### **Frontend Aufwand (0.5 Tage):**
- [ ] 2-3 Angular Components
- [ ] Reactive Forms mit Validation
- [ ] State Management (NgRx Actions/Effects)
- [ ] Component und Integration Tests

#### **Beispiele:**
- `feat: Waiting List Ranking - FIFO calculation with UI display`
- `feat: Application Search - Advanced search with filters`
- `feat: Notification System - Email notifications with UI preferences`

---

### **Size/L (2 Tage) - Komplexe Features**

#### **Charakteristika:**
- Komplexe Business Logic
- Multiple UI-States und Workflows
- Integration mit externen Services
- Advanced State Management erforderlich

#### **Backend Aufwand (1.5 Tage):**
- [ ] Multiple Controllers oder Complex Controller
- [ ] 4+ Commands/Queries mit Business Rules
- [ ] External Service Integration
- [ ] Domain Event Handling
- [ ] Extensive Unit und Integration Tests
- [ ] Performance Optimization

#### **Frontend Aufwand (0.5 Tage):**
- [ ] 3+ Angular Components
- [ ] Complex Forms mit Multi-Step Wizard
- [ ] Advanced State Management
- [ ] Real-time Updates (WebSocket)
- [ ] Comprehensive Testing

#### **Beispiele:**
- `feat: Multi-Step Application Form - Complete application wizard`
- `feat: Document Generation - PDF templates with preview`
- `feat: Real-time Dashboard - Live updates with WebSocket`

---

## 🐛 Bug Issue Sizing (Single Layer)

### **Size/XS (0.5 Tag) - Triviale Bugs**

#### **Charakteristika:**
- Offensichtliche Ursache
- Einfacher Fix
- Keine Tests erforderlich
- Minimaler Testing-Aufwand

#### **Aufgaben:**
- [ ] Bug reproduzieren (15 min)
- [ ] Fix implementieren (2 Stunden)  
- [ ] Manuelles Testen (1 Stunde)
- [ ] Regression Test (30 min)

#### **Beispiele:**
- `fix: Typo in validation message - Frontend`
- `fix: Missing null check in API response - Backend`
- `fix: Button styling on mobile - Frontend`

---

### **Size/S (1 Tag) - Standard Bugs**

#### **Charakteristika:**
- Klare Reproduktion möglich
- Lokalisierter Fix erforderlich
- Unit Test Update nötig
- Moderate Testabdeckung

#### **Aufgaben:**
- [ ] Bug reproduzieren und analysieren (2 Stunden)
- [ ] Root Cause Analysis (1 Stunde)
- [ ] Fix implementieren (3 Stunden)
- [ ] Unit Tests aktualisieren (1 Stunde)
- [ ] Integration Testing (1 Stunde)

#### **Beispiele:**
- `fix: Aktenzeichen validation fails for edge cases - Backend`
- `fix: Form validation error display - Frontend`  
- `fix: Pagination not working correctly - Backend`

---

### **Size/M (1.5 Tage) - Komplexe Bugs**

#### **Charakteristika:**
- Schwer reproduzierbare Bugs
- Multiple Komponenten betroffen
- Extensive Testing erforderlich
- Mögliche Architektur-Änderungen

#### **Aufgaben:**
- [ ] Bug-Reproduktion und Investigation (4 Stunden)
- [ ] Root Cause Analysis (2 Stunden)
- [ ] Fix Design und Implementation (6 Stunden)
- [ ] Comprehensive Testing (3 Stunden)
- [ ] Documentation Update (1 Stunde)

#### **Beispiele:**
- `fix: Race condition in waiting list ranking - Backend`
- `fix: Memory leak in real-time updates - Frontend`
- `fix: Inconsistent data state across components - Frontend`

---

## 📋 Task Issue Sizing (Infrastructure/Docs)

### **Size/XS (0.5 Tag) - Quick Tasks**

#### **Beispiele:**
- `task: Update README with new setup instructions`
- `task: Add environment variable to Docker config`
- `task: Fix broken link in documentation`

---

### **Size/S (1 Tag) - Standard Tasks**

#### **Beispiele:**
- `task: Setup ESLint configuration - Frontend tooling`
- `task: Add health check endpoint - API monitoring`
- `task: Create user guide document - Documentation`

---

### **Size/M (1.5 Tage) - Complex Tasks**

#### **Beispiele:**
- `task: Setup CI/CD pipeline - GitHub Actions with testing`
- `task: Configure monitoring dashboard - Application insights`
- `task: Implement caching layer - Redis integration`

---

### **Size/L (2 Tage) - Major Tasks**

#### **Beispiele:**
- `task: Docker containerization - Full application containerization`
- `task: Performance optimization - Database and API optimization`
- `task: Security audit implementation - Complete security review`

---

## 🔍 Sizing-Kriterien Details

### **Backend-Aufwand Schätzen**

#### **Controller Complexity:**
- **Simple** (0.5h): Basic CRUD mit standard DTOs
- **Medium** (2h): Business Logic, custom validation
- **Complex** (4h): Multiple entities, complex workflows

#### **Business Logic:**
- **Simple** (1h): Standard CRUD operations
- **Medium** (3h): Business rules, domain events
- **Complex** (6h): Complex algorithms, external integrations

#### **Testing Effort:**
- **Unit Tests**: 25% der Implementation-Zeit
- **Integration Tests**: 15% der Implementation-Zeit
- **API Documentation**: 10% der Implementation-Zeit

### **Frontend-Aufwand Schätzen**

#### **Component Complexity:**
- **Simple** (1h): Display-only, basic forms
- **Medium** (3h): Interactive forms, state management
- **Complex** (6h): Complex workflows, real-time updates

#### **State Management:**
- **None** (0h): Simple component state
- **Local** (1h): Component or service state
- **Global** (3h): NgRx actions, effects, reducers

#### **Styling Effort:**
- **Basic** (0.5h): Material Components as-is
- **Custom** (1h): Custom styling, responsive design
- **Complex** (2h): Advanced animations, complex layouts

---

## ⚡ Sizing-Workflow

### **1. Issue-Erstellung**

#### **Template Pre-Fill:**
- Akzeptiere nur Issues mit `size/` Label
- Automatische Schätzung basierend auf Titel-Keywords
- Validation gegen 2-Tage-Limit

#### **Sizing-Checklist:**
- [ ] Backend-Aufwand geschätzt
- [ ] Frontend-Aufwand geschätzt (bei Features)  
- [ ] Testing-Aufwand berücksichtigt
- [ ] Integration-Komplexität bewertet
- [ ] Domain-Risiko eingeschätzt

### **2. Review-Prozess**

#### **Sizing-Review:**
```markdown
## Size Estimation Review

**Backend Estimate**: X hours
- Controllers: Y hours
- Business Logic: Z hours  
- Testing: A hours

**Frontend Estimate**: B hours (für Features)
- Components: C hours
- State Management: D hours
- Testing: E hours

**Total**: F hours (should be ≤ 16 hours for 2 days)

**Risks/Unknowns**: [List any uncertainties]
**Dependencies**: [List blocking issues]
```

### **3. Sizing-Anpassung**

#### **Wenn Issue > 2 Tage:**
1. **Identifiziere Trennlinien:**
   - Backend vs. Frontend
   - Core Feature vs. Nice-to-have
   - MVP vs. Full Feature

2. **Split-Strategien:**
   - **Horizontal**: Backend zuerst, dann Frontend
   - **Vertical**: Minimal Feature, dann Extensions
   - **Technical**: Core Logic, dann UI Polish

3. **Beispiel-Split:**
```markdown
Original Issue: "Complete User Management System (5 days)"

Split into:
1. "User CRUD API - Backend foundation" (1.5 days)
2. "User List UI - Basic display component" (1 day)  
3. "User Edit Form - Advanced form with validation" (1.5 days)
4. "User Role Management - Admin functionality" (1 day)
```

---

## 📊 Sizing-Accuracy Tracking

### **Velocity Metrics**

#### **Issue-Level Tracking:**
- Estimated vs. Actual time
- Accuracy per developer
- Accuracy per issue type
- Accuracy trends over time

#### **Team-Level Metrics:**
- Average estimation error
- Issues that required re-sizing
- Split rate (XL → smaller issues)
- Completion rate within estimates

### **Continuous Improvement**

#### **Weekly Sizing Review:**
1. **Over-Estimated Issues**: Why was it faster?
2. **Under-Estimated Issues**: What was missed?
3. **Split Issues**: Could we have sized better initially?
4. **Blocked Issues**: External dependencies?

#### **Sizing Calibration:**
- Monthly team calibration sessions
- Review historical data
- Update sizing guidelines based on learnings
- Share best practices across team

---

## 🛡️ Domain-Integration

### **Domain-Risk-Sizing**

#### **Size/S + Domain Risk:**
```yaml
Base Estimation: 1 day
Domain Risk Factor: +0.5 days
Approval Process: +0.25 days
Total: 1.75 days (still within size/L)
```

#### **Domain-Approval-Process:**
1. **Size normally** but flag as domain-risk
2. **Add approval buffer** (0.25-0.5 days)
3. **Block until approval** received
4. **Proceed with implementation**

### **Protected Areas Impact:**

#### **Value Objects (High Risk):**
- Automatic +1 day for approval process
- Mandatory architecture review
- Enhanced testing requirements

#### **Business Rules (Critical):**
- Automatic escalation to domain architect
- Extended review period
- Comprehensive impact analysis

---

## 🎯 Best Practices

### **Für Product Owner:**
- ✅ Schreibe klare, testbare Akzeptanzkriterien
- ✅ Priorisiere Features nach Business Value
- ✅ Akzeptiere nur richtig gesized Issues
- ✅ Plane Buffer für unerwartete Komplexität

### **Für Entwickler:**
- ✅ Schätze konservativ (lieber etwas mehr Zeit)
- ✅ Berücksichtige immer Testing-Zeit
- ✅ Kommuniziere Unsicherheiten früh
- ✅ Teile zu große Issues proaktiv auf

### **Für Team:**
- ✅ Kalibriert Schätzungen regelmäßig
- ✅ Dokumentiert Lessons Learned
- ✅ Teilt Sizing-Expertise
- ✅ Maintains consistent sizing standards

---

**📏 Diese Guidelines gewährleisten planbare, realistische Entwicklungszyklen mit maximal 2 Tagen pro Issue, während die Qualität und Domain-Schutz-Anforderungen eingehalten werden.**