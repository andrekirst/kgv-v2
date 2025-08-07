# 🎯 KGV-Projekt Meilenstein-Struktur

## 📊 Übersicht der Meilensteine

Basierend auf der Migrationsstrategie und den Business Requirements wurden die Meilensteine so definiert, dass jeder Phase klare, messbare Ziele hat und die Issues in maximal 1-2 Tage Entwicklungszeit aufgeteilt sind.

---

## 🏗️ Meilenstein 1: Foundation (M1)

### **📋 Projekt-Metadaten**
- **Zeitraum**: Wochen 1-10 (Q1 2025)
- **Entwicklertage**: 40 Tage
- **Issues Gesamt**: 20 
- **Team-Fokus**: 2 Backend + 2 Frontend Entwickler

### **🎯 Hauptziele**
- ✅ Solide technische Grundlagen schaffen
- ✅ Domain Model sicher implementieren (CLAUDE.md konform)
- ✅ Authentication & Authorization System
- ✅ Basic API Endpoints für MVP

### **📈 Erfolgskriterien**
- [ ] Domain Model vollständig implementiert und getestet
- [ ] JWT Authentication funktionsfähig
- [ ] 10 Core API Endpoints verfügbar
- [ ] Angular Projekt-Grundlagen erstellt
- [ ] CI/CD Pipeline läuft
- [ ] >95% Domain Guard Compliance

---

## 🌟 Meilenstein 1 - Detaillierte Issue-Planung

### **🏛️ Domain & Architecture Block (5 Issues, 7.5 Tage)**

#### **Issue #1: Domain Model Foundation**
**Titel**: `feat: Domain Value Objects - Aktenzeichen, PersonData, Address`
**Assignee**: Backend Lead Developer
**Aufwand**: 2 Tage
**Dependencies**: Keine

**Backend Tasks (2 Tage)**:
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

**Acceptance Criteria**:
- ✅ Alle Value Objects sind immutable (sealed records)
- ✅ Pattern-Matching für Aktenzeichen funktioniert
- ✅ Validierung wirft korrekte Exceptions
- ✅ Unit Tests decken alle Business Rules ab
- ✅ Keine Änderungen an CLAUDE.md-geschützten Bereichen

---

#### **Issue #2: Entity Framework Foundation** 
**Titel**: `feat: EF Core Setup - DbContext, Entities, Migrations`
**Assignee**: Backend Developer
**Aufwand**: 1 Tag
**Dependencies**: #1

**Backend Tasks (1 Tag)**:
- [ ] KgvDbContext erstellen mit DbSets
- [ ] Entity-Konfigurationen für Value Objects
- [ ] Application Entity mit Status-Enums
- [ ] WaitingList und WaitingListEntry Entities
- [ ] Initial Migration erstellen
- [ ] Seed-Data für Gemarkungen

**Acceptance Criteria**:
- ✅ Migrations laufen ohne Fehler
- ✅ Value Objects werden korrekt serialisiert/deserialisiert
- ✅ Foreign Key Constraints sind definiert
- ✅ Seed-Data wird korrekt geladen

---

#### **Issue #3: Repository Pattern Implementation**
**Titel**: `feat: Repository Pattern - Generic Repository + Unit of Work`
**Assignee**: Backend Developer  
**Aufwand**: 1 Tag
**Dependencies**: #2

**Backend Tasks (1 Tag)**:
- [ ] Generic Repository Interface und Implementation
- [ ] Specialized Repositories: ApplicationRepository, WaitingListRepository
- [ ] Unit of Work Pattern implementieren
- [ ] Repository Extensions für complex queries
- [ ] Integration Tests für Repository

**Acceptance Criteria**:
- ✅ Generic Repository funktioniert für alle Entities
- ✅ Complex Business Queries sind implementiert
- ✅ Unit of Work koordiniert Transaktionen korrekt
- ✅ Integration Tests bestehen

---

#### **Issue #4: CQRS Infrastructure Setup**
**Titel**: `feat: CQRS Setup - MediatR Commands/Queries Infrastructure`
**Assignee**: Backend Lead Developer
**Aufwand**: 1.5 Tage  
**Dependencies**: #3

**Backend Tasks (1.5 Tage)**:
- [ ] MediatR Package konfigurieren
- [ ] Base Command/Query/Handler Classes
- [ ] Validation Pipeline mit FluentValidation
- [ ] Logging und Error-Handling Pipeline
- [ ] Example Command/Query für Application CRUD
- [ ] Unit Tests für Pipeline-Behavior

**Acceptance Criteria**:
- ✅ Commands und Queries funktionieren über MediatR
- ✅ Validation Pipeline fängt alle Validierungsfehler
- ✅ Logging erfasst alle Command/Query-Aktivitäten
- ✅ Error-Handling ist konsistent

---

#### **Issue #5: Domain Events Infrastructure**
**Titel**: `feat: Domain Events - Event Publishing/Handling Infrastructure`
**Assignee**: Backend Developer
**Aufwand**: 1.5 Tage
**Dependencies**: #4

**Backend Tasks (1.5 Tage)**:
- [ ] IDomainEvent Interface definieren
- [ ] Domain Event Publisher implementieren
- [ ] Event Handler Registry
- [ ] ApplicationCreated, ApplicationStatusChanged Events
- [ ] Event Sourcing Vorbereitungen (Versioning)
- [ ] Integration Tests für Event Flow

**Acceptance Criteria**:
- ✅ Domain Events werden korrekt publiziert
- ✅ Event Handler werden automatisch registriert
- ✅ Event Versioning ist vorbereitet
- ✅ Events sind testbar und nachverfolgbar

---

### **🔐 Authentication & Security Block (4 Issues, 5.5 Tage)**

#### **Issue #6: JWT Authentication System**
**Titel**: `feat: JWT Authentication - Token generation, validation, refresh`
**Assignee**: Backend Lead Developer
**Aufwand**: 2 Tage
**Dependencies**: #2

**Backend Tasks (1 Tag)**:
- [ ] JWT Token-Generierung implementieren
- [ ] Token-Validation Middleware
- [ ] Refresh Token Mechanismus
- [ ] Claims-based Authorization Setup
- [ ] Login/Logout Endpoints
- [ ] Token Blacklisting für Logout

**Frontend Tasks (1 Tag)**:
- [ ] AuthService für Token-Management
- [ ] HTTP Interceptor für Authorization Headers
- [ ] Token-Refresh Logic
- [ ] Login/Logout Components (basic)
- [ ] Route Guards implementieren

**Acceptance Criteria**:
- ✅ JWT Tokens werden korrekt generiert und validiert
- ✅ Refresh Token funktioniert automatisch
- ✅ Authorization Headers werden automatisch gesetzt
- ✅ Expired Tokens werden automatisch erneuert
- ✅ Logout invalidiert Tokens serverseitig

---

#### **Issue #7: User Management Foundation**
**Titel**: `feat: User Management - User Entity, basic CRUD`
**Assignee**: Backend Developer
**Aufwand**: 1 Tag  
**Dependencies**: #6

**Backend Tasks (1 Tag)**:
- [ ] User Entity mit Rollen definieren
- [ ] User Repository und Services
- [ ] Password-Hashing mit bcrypt
- [ ] User CRUD Commands/Queries
- [ ] Basic User Management API Endpoints
- [ ] Unit Tests für User Services

**Acceptance Criteria**:
- ✅ User können erstellt, gelesen, aktualisiert werden
- ✅ Passwörter sind sicher gehasht
- ✅ User-Rollen funktionieren korrekt
- ✅ API Endpoints sind dokumentiert

---

#### **Issue #8: Role-based Authorization**
**Titel**: `feat: Role-based Authorization - Roles, Permissions, Policies`
**Assignee**: Backend Developer
**Aufwand**: 1.5 Tage
**Dependencies**: #7

**Backend Tasks (1 Tag)**:
- [ ] Role und Permission Entities
- [ ] Policy-based Authorization Setup
- [ ] Custom Authorization Attributes
- [ ] Admin, Sachbearbeiter, Bürger Rollen
- [ ] Permission-Matrix implementieren

**Frontend Tasks (0.5 Tage)**:
- [ ] Role-based Route Guards
- [ ] Component-Level Permission Checks
- [ ] UI Element Visibility basierend auf Rollen

**Acceptance Criteria**:
- ✅ Verschiedene Rollen haben unterschiedliche Berechtigungen
- ✅ UI passt sich basierend auf Benutzerrolle an
- ✅ API Endpoints sind rollenbasiert geschützt
- ✅ Authorization Policies funktionieren korrekt

---

#### **Issue #9: Security Middleware**
**Titel**: `feat: Security Middleware - Rate limiting, CORS, Headers`
**Assignee**: Backend Developer
**Aufwand**: 1 Tag
**Dependencies**: #8

**Backend Tasks (1 Tag)**:
- [ ] Rate Limiting implementieren
- [ ] CORS Policy konfigurieren  
- [ ] Security Headers Middleware
- [ ] Request/Response Logging
- [ ] API Key Validation (für externe APIs)
- [ ] Security Integration Tests

**Acceptance Criteria**:
- ✅ Rate Limiting verhindert API Abuse
- ✅ CORS ist korrekt für Frontend konfiguriert
- ✅ Security Headers sind gesetzt
- ✅ Logging erfasst sicherheitsrelevante Events

---

### **🌐 Core API Endpoints Block (6 Issues, 8.5 Tage)**

#### **Issue #10: Application CRUD API**
**Titel**: `feat: Application Management API - Complete CRUD with validation`
**Assignee**: Full-Stack Developer 1
**Aufwand**: 2 Tage
**Dependencies**: #5, #9

**Backend Tasks (1.5 Tage)**:
- [ ] Application Controller mit allen CRUD Endpoints
- [ ] CreateApplicationCommand mit Validation
- [ ] UpdateApplicationCommand mit Business Rules
- [ ] GetApplicationQuery mit Paging/Filtering
- [ ] DeleteApplicationCommand (Soft Delete)
- [ ] Integration Tests für alle Endpoints
- [ ] OpenAPI/Swagger Dokumentation

**Frontend Tasks (0.5 Tage)**:
- [ ] ApplicationService für API Calls
- [ ] Basic DTOs/Models definieren
- [ ] Error-Handling für API Responses
- [ ] Loading States Management

**Acceptance Criteria**:
- ✅ CRUD Operationen funktionieren korrekt
- ✅ Business Rules werden eingehalten
- ✅ API ist vollständig dokumentiert
- ✅ Frontend kann erfolgreich API aufrufen
- ✅ Error-Handling funktioniert durchgängig

---

#### **Issue #11: Application Status API**
**Titel**: `feat: Application Status Management - Status changes with validation`
**Assignee**: Backend Developer
**Aufwand**: 1.5 Tage
**Dependencies**: #10

**Backend Tasks (1.5 Tage)**:
- [ ] ChangeApplicationStatusCommand implementieren
- [ ] Status-Transition-Validierung (State Machine)
- [ ] ApplicationStatusChanged Domain Event
- [ ] Status History Tracking
- [ ] GetApplicationStatusHistoryQuery
- [ ] Status Change API Endpoints

**Acceptance Criteria**:
- ✅ Nur gültige Status-Übergänge sind erlaubt
- ✅ Status-Geschichte wird vollständig protokolliert
- ✅ Domain Events werden bei Status-Änderungen gefeuert
- ✅ API verhindert ungültige Übergänge

---

#### **Issue #12: Person Management API**  
**Titel**: `feat: Person Management API - Person CRUD with validation`
**Assignee**: Backend Developer
**Aufwand**: 1 Tag
**Dependencies**: #1

**Backend Tasks (1 Tag)**:
- [ ] Person Controller und Endpoints
- [ ] PersonData Validation Rules
- [ ] Duplicate Person Detection
- [ ] Person Search/Filter Functionality
- [ ] Person-Address Relationship Management
- [ ] Unit Tests für Person-spezifische Logic

**Acceptance Criteria**:
- ✅ Person-Daten werden korrekt validiert
- ✅ Duplikate werden erkannt und verhindert
- ✅ Search/Filter funktioniert performant
- ✅ PersonData Value Object Integration

---

#### **Issue #13: Waiting List API**
**Titel**: `feat: Waiting List Management - List management, ranking`
**Assignee**: Full-Stack Developer 2
**Aufwand**: 2 Tage
**Dependencies**: #1, #10

**Backend Tasks (1.5 Tage)**:
- [ ] WaitingList Controller mit CRUD
- [ ] AddToWaitingListCommand
- [ ] FIFO Ranking Algorithm implementieren
- [ ] RemoveFromWaitingListCommand mit Re-ranking
- [ ] GetWaitingListQuery mit Paging
- [ ] Ranking Recalculation Service
- [ ] Integration Tests für Ranking Logic

**Frontend Tasks (0.5 Tage)**:
- [ ] WaitingListService für API Integration
- [ ] Basic List Display Component
- [ ] Ranking Position Display
- [ ] Real-time Updates Setup (WebSocket Vorbereitung)

**Acceptance Criteria**:
- ✅ FIFO Ranking funktioniert korrekt
- ✅ Ranking wird bei Änderungen automatisch aktualisiert
- ✅ Frontend zeigt aktuelle Ranking Position
- ✅ Performance ist auch bei großen Listen gut

---

#### **Issue #14: User Management API**
**Titel**: `feat: Admin User Management API - Admin functions`
**Assignee**: Backend Developer
**Aufwand**: 1 Tag
**Dependencies**: #8

**Backend Tasks (1 Tag)**:
- [ ] Admin-specific User Management Endpoints
- [ ] User Role Assignment API
- [ ] User Activity Tracking
- [ ] Password Reset Functionality
- [ ] User Account Activation/Deactivation
- [ ] Admin Dashboard Statistics API

**Acceptance Criteria**:
- ✅ Nur Admins können User-Management zugreifen
- ✅ Role-Assignment funktioniert korrekt
- ✅ Password-Reset ist sicher implementiert
- ✅ User-Aktivitäten werden geloggt

---

#### **Issue #15: Health Check API**
**Titel**: `feat: Health Check API - Monitoring, diagnostics`
**Assignee**: Backend Developer
**Aufwand**: 0.5 Tage
**Dependencies**: #2

**Backend Tasks (0.5 Tage)**:
- [ ] Basic Health Check Endpoint (/health)
- [ ] Database Connectivity Check
- [ ] External Services Health Check
- [ ] Version Information Endpoint
- [ ] System Status Dashboard API
- [ ] Monitoring Integration (準備)

**Acceptance Criteria**:
- ✅ Health Check gibt korrekten System Status zurück
- ✅ Database und externe Services werden geprüft
- ✅ Monitoring kann System-Health abrufen
- ✅ API ist für Load Balancer konfiguriert

---

### **🖥️ Frontend Foundation Block (5 Issues, 6.5 Tage)**

#### **Issue #16: Angular Project Setup**
**Titel**: `feat: Angular Foundation - CLI, Material, Routing`
**Assignee**: Frontend Lead Developer
**Aufwand**: 1 Tag
**Dependencies**: Keine

**Frontend Tasks (1 Tag)**:
- [ ] Angular CLI Projekt erstellen (v18+)
- [ ] Angular Material Installation und Konfiguration
- [ ] Basic Routing Setup mit Lazy Loading
- [ ] SCSS Configuration und Theme Setup
- [ ] Environment-Konfiguration (dev/prod)
- [ ] Build-Optimization (Bundle Analyzer)
- [ ] PWA Service Worker Vorbereitung

**Acceptance Criteria**:
- ✅ Angular Projekt kompiliert ohne Warnings
- ✅ Angular Material ist integriert und funktioniert
- ✅ Routing funktioniert mit Lazy Loading
- ✅ Build ist für Produktion optimiert
- ✅ Theme ist konfigurierbar

---

#### **Issue #17: Authentication Module**
**Titel**: `feat: Authentication Module - Login, JWT handling, Guards`
**Assignee**: Frontend Developer
**Aufwand**: 2 Tage
**Dependencies**: #16, #6

**Frontend Tasks (2 Tage)**:
- [ ] Login Component mit Reactive Forms
- [ ] AuthService mit JWT Token Management
- [ ] HTTP Interceptor für Token-Injection
- [ ] AuthGuard für Route Protection
- [ ] Token-Refresh-Logic implementieren
- [ ] Logout-Functionality
- [ ] Login State Management (NgRx Setup)
- [ ] Component Tests für Auth Module

**Acceptance Criteria**:
- ✅ Login funktioniert mit Backend JWT API
- ✅ Token werden automatisch in Requests eingefügt
- ✅ Route Guards schützen geschützte Bereiche
- ✅ Token werden automatisch erneuert
- ✅ Logout entfernt Token korrekt

---

#### **Issue #18: Main Layout Components**
**Titel**: `feat: Main Layout - Header, Navigation, Footer`
**Assignee**: Frontend Developer
**Aufwand**: 1 Tag
**Dependencies**: #17

**Frontend Tasks (1 Tag)**:
- [ ] App Layout Component mit Angular Material
- [ ] Header mit User-Info und Logout
- [ ] Navigation Sidebar mit Role-based Menu
- [ ] Footer mit System-Infos
- [ ] Responsive Breakpoints für Mobile
- [ ] Theme Toggle (Light/Dark Mode)
- [ ] Loading Indicator Component

**Acceptance Criteria**:
- ✅ Layout ist responsive auf allen Geräten
- ✅ Navigation passt sich an Benutzerrolle an
- ✅ Theme Toggle funktioniert
- ✅ User-Informationen werden korrekt angezeigt

---

#### **Issue #19: Form Infrastructure**
**Titel**: `feat: Form Infrastructure - Reactive forms, validation`
**Assignee**: Frontend Lead Developer
**Aufwand**: 1.5 Tage
**Dependencies**: #18

**Frontend Tasks (1.5 Tage)**:
- [ ] Base Form Component mit Validation
- [ ] Custom Form Controls für KGV-spezifische Felder
- [ ] Aktenzeichen Input Component
- [ ] PersonData Form Component
- [ ] Address Form Component mit PLZ-Lookup
- [ ] Form Validation Service
- [ ] Error-Message Display System
- [ ] Form Components Testing

**Acceptance Criteria**:
- ✅ Reactive Forms funktionieren mit Validation
- ✅ Custom Controls sind wiederverwendbar
- ✅ Error Messages sind benutzerfreundlich
- ✅ PLZ-Lookup funktioniert in Echtzeit
- ✅ Forms sind barrierefrei (BITV 2.0)

---

#### **Issue #20: HTTP Interceptor Setup**
**Titel**: `feat: HTTP Infrastructure - Error handling, loading states`
**Assignee**: Frontend Developer
**Aufwand**: 1 Tag  
**Dependencies**: #17

**Frontend Tasks (1 Tag)**:
- [ ] Global Error Handling Interceptor
- [ ] Loading State Interceptor
- [ ] API Base URL Configuration
- [ ] Retry Logic für fehlgeschlagene Requests
- [ ] Global Error Notification Service
- [ ] Request/Response Logging (Development)
- [ ] HTTP Client Testing Setup

**Acceptance Criteria**:
- ✅ HTTP Errors werden benutzerfreundlich angezeigt
- ✅ Loading States werden automatisch gemanagt
- ✅ Fehlgeschlagene Requests werden automatisch wiederholt
- ✅ API Communication ist robust und testbar

---

## 📊 Meilenstein 1 - Metriken und Erfolgsmessung

### **Quantitative Erfolgskriterien**
- ✅ **20 von 20 Issues** erfolgreich abgeschlossen
- ✅ **>95% Test Coverage** für Domain Model
- ✅ **100% Domain Guard Compliance** (keine Violations)
- ✅ **API Response Time** < 500ms für alle Endpoints
- ✅ **Frontend Build Time** < 60 Sekunden
- ✅ **Zero Critical Security Issues** in Audit

### **Qualitative Erfolgskriterien**
- ✅ **Domain Model** entspricht exakt der Dokumentation
- ✅ **Authentication** ist produktionsbereit und sicher
- ✅ **API Architecture** ist erweiterbar und maintainable
- ✅ **Frontend Foundation** ermöglicht effiziente Feature-Entwicklung
- ✅ **Team Velocity** ist stabil und vorhersagbar

### **Go/No-Go Kriterien für Meilenstein 2**
- 🟢 **Go**: Alle Issues abgeschlossen, Tests bestehen, Stakeholder zufrieden
- 🔴 **No-Go**: >2 Issues offen, Domain Guard Violations, kritische Bugs

---

*Diese detaillierte Planung für Meilenstein 1 dient als Vorlage für die weiteren Meilensteine und gewährleistet, dass jedes Issue klar definiert, schätzbar und in maximal 2 Tagen umsetzbar ist.*