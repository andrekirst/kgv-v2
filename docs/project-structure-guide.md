# 🏗️ KGV-v2 Projektstruktur-Guide

## 🎯 Übersicht

Diese Dokumentation definiert die standardisierte Projektstruktur für das KGV-v2 System mit klarer Trennung zwischen Frontend und Backend-Entwicklung.

---

## 📁 Root-Verzeichnis Struktur

```
kgv-v2/
├── src/                          # 📦 Source Code
│   ├── frontend/                 # 🖥️ Angular Application
│   └── backend/                  # 🌐 .NET 9 Web API (inkl. Test-Projekte)
├── tests/                        # 🧪 Frontend Tests
│   ├── unit/                     # Unit Tests (Jest)
│   ├── integration/              # Integration Tests
│   └── e2e/                      # End-to-End Tests (Playwright)
├── docs/                         # 📚 Documentation
├── scripts/                      # 🔧 Build & Automation Scripts
├── .github/                      # ⚙️ GitHub Workflows
├── config/                       # ⚙️ Configuration Files
├── tools/                        # 🛠️ Development Tools
├── .gitignore                    # Git Ignore Rules
├── .editorconfig                 # Editor Configuration
├── README.md                     # Project Overview
└── docker-compose.yml            # Local Development Environment
```

---

## 🌐 Backend Struktur (`src/backend/`)

### **Clean Architecture Implementierung**

```
src/backend/
├── KGV.sln                       # Visual Studio Solution (inkl. Test-Projekte)
├── .dockerignore                 # Docker Ignore Rules
├── Dockerfile                    # Container Configuration
│
├── KGV.WebApi/                   # 🌐 Presentation Layer (API Entry Point)
│   ├── Controllers/              
│   │   ├── ApplicationsController.cs     # Application Management
│   │   ├── AuthController.cs             # Authentication
│   │   ├── UsersController.cs            # User Management  
│   │   ├── WaitingListController.cs      # Waiting List
│   │   └── HealthController.cs           # Health Checks
│   ├── Middleware/
│   │   ├── ErrorHandlingMiddleware.cs    # Global Error Handler
│   │   ├── LoggingMiddleware.cs          # Request/Response Logging
│   │   └── SecurityHeadersMiddleware.cs  # Security Headers
│   ├── Configuration/
│   │   ├── DependencyInjection.cs       # DI Container Setup
│   │   ├── SwaggerConfiguration.cs      # API Documentation
│   │   └── CorsConfiguration.cs         # CORS Policy
│   ├── Extensions/
│   │   ├── ServiceCollectionExtensions.cs
│   │   └── ApplicationBuilderExtensions.cs
│   ├── appsettings.json          # Configuration
│   ├── appsettings.Development.json
│   ├── appsettings.Production.json
│   ├── Program.cs                # Application Entry Point
│   └── KGV.WebApi.csproj
│
├── KGV.Application/              # 💼 Application Layer (Use Cases)
│   ├── Commands/                 # CQRS Commands
│   │   ├── Applications/
│   │   │   ├── CreateApplicationCommand.cs
│   │   │   ├── UpdateApplicationCommand.cs
│   │   │   └── ChangeApplicationStatusCommand.cs
│   │   ├── Auth/
│   │   │   ├── LoginCommand.cs
│   │   │   └── RefreshTokenCommand.cs
│   │   └── Users/
│   │       ├── CreateUserCommand.cs
│   │       └── UpdateUserRoleCommand.cs
│   ├── Queries/                  # CQRS Queries
│   │   ├── Applications/
│   │   │   ├── GetApplicationQuery.cs
│   │   │   ├── GetApplicationsQuery.cs
│   │   │   └── GetApplicationHistoryQuery.cs
│   │   ├── WaitingList/
│   │   │   ├── GetWaitingListQuery.cs
│   │   │   └── GetWaitingListPositionQuery.cs
│   │   └── Users/
│   │       └── GetUsersQuery.cs
│   ├── Handlers/                 # Command/Query Handlers
│   │   ├── Applications/
│   │   ├── Auth/
│   │   ├── Users/
│   │   └── WaitingList/
│   ├── Services/                 # Application Services
│   │   ├── IApplicationService.cs
│   │   ├── IAuthService.cs
│   │   ├── IEmailService.cs
│   │   └── IPdfService.cs
│   ├── DTOs/                     # Data Transfer Objects
│   │   ├── ApplicationDto.cs
│   │   ├── UserDto.cs
│   │   ├── AuthTokenDto.cs
│   │   └── WaitingListEntryDto.cs
│   ├── Mappings/                 # AutoMapper Profiles
│   │   ├── ApplicationProfile.cs
│   │   ├── UserProfile.cs
│   │   └── WaitingListProfile.cs
│   ├── Validation/               # FluentValidation Rules
│   │   ├── ApplicationValidators/
│   │   ├── UserValidators/
│   │   └── AuthValidators/
│   ├── Common/                   # Common Application Logic
│   │   ├── Behaviors/            # MediatR Behaviors
│   │   ├── Exceptions/           # Application Exceptions
│   │   └── Interfaces/           # Application Interfaces
│   └── KGV.Application.csproj
│
├── KGV.Domain/                   # 🏛️ Domain Layer (PROTECTED BY CLAUDE.md!)
│   ├── Entities/                 # Domain Entities
│   │   ├── Application.cs        # Core Business Entity
│   │   ├── Person.cs             # Person Entity
│   │   ├── WaitingList.cs        # Waiting List Aggregate
│   │   ├── WaitingListEntry.cs   # Entry in Waiting List
│   │   └── User.cs               # System User
│   ├── ValueObjects/             # Value Objects (IMMUTABLE!)
│   │   ├── Aktenzeichen.cs       # File Reference Number
│   │   ├── PersonData.cs         # Person Information
│   │   ├── Address.cs            # Address Information  
│   │   └── Email.cs              # Email Value Object
│   ├── Services/                 # Domain Services
│   │   ├── WaitingListRankingService.cs  # FIFO Ranking Logic
│   │   ├── ApplicationNumberService.cs  # Number Generation
│   │   └── DuplicateCheckService.cs     # Duplicate Detection
│   ├── Events/                   # Domain Events
│   │   ├── ApplicationCreatedEvent.cs
│   │   ├── ApplicationStatusChangedEvent.cs
│   │   ├── WaitingListPositionChangedEvent.cs
│   │   └── UserRegisteredEvent.cs
│   ├── Interfaces/               # Repository Interfaces
│   │   ├── IApplicationRepository.cs
│   │   ├── IWaitingListRepository.cs
│   │   ├── IUserRepository.cs
│   │   └── IUnitOfWork.cs
│   ├── Specifications/           # Business Rule Specifications
│   │   ├── ApplicationSpecifications/
│   │   ├── WaitingListSpecifications/
│   │   └── UserSpecifications/
│   ├── Enums/                    # Domain Enumerations
│   │   ├── ApplicationStatus.cs
│   │   ├── WaitingListType.cs
│   │   └── UserRole.cs
│   ├── Constants/                # Domain Constants
│   │   └── BusinessRules.cs      # PROTECTED BUSINESS RULES
│   └── KGV.Domain.csproj
│
├── KGV.Infrastructure/           # 🔧 Infrastructure Layer
│   ├── Persistence/              # Database Implementation
│   │   ├── Context/
│   │   │   ├── KgvDbContext.cs   # Main Database Context
│   │   │   └── KgvDbContextFactory.cs  # Design-time Factory
│   │   ├── Configurations/       # Entity Framework Configurations
│   │   │   ├── ApplicationConfiguration.cs
│   │   │   ├── PersonConfiguration.cs
│   │   │   ├── WaitingListConfiguration.cs
│   │   │   └── UserConfiguration.cs
│   │   ├── Migrations/           # EF Core Migrations
│   │   │   └── [Generated Migration Files]
│   │   ├── Repositories/         # Repository Implementations
│   │   │   ├── ApplicationRepository.cs
│   │   │   ├── WaitingListRepository.cs
│   │   │   ├── UserRepository.cs
│   │   │   └── UnitOfWork.cs
│   │   ├── Seeds/                # Database Seed Data
│   │   │   ├── GemarkungSeed.cs
│   │   │   └── DefaultUserSeed.cs
│   │   └── Extensions/
│   │       └── ModelBuilderExtensions.cs
│   ├── Services/                 # External Service Implementations
│   │   ├── EmailService.cs       # Email Implementation
│   │   ├── PdfService.cs         # PDF Generation
│   │   ├── FileStorageService.cs # File Management
│   │   └── NotificationService.cs
│   ├── Identity/                 # Authentication & Authorization
│   │   ├── JwtTokenService.cs    # JWT Token Management
│   │   ├── PasswordHasher.cs     # Password Security
│   │   ├── AuthenticationService.cs
│   │   └── AuthorizationHandlers/
│   ├── External/                 # External API Integrations
│   │   ├── GeocodeService.cs     # Address Geocoding
│   │   └── PostalCodeService.cs  # PLZ Validation
│   └── KGV.Infrastructure.csproj
│
├── KGV.Shared/                   # 📦 Shared Components
│   ├── Constants/                # Application-wide Constants
│   │   ├── ApiRoutes.cs          # API Route Constants
│   │   ├── PolicyNames.cs        # Authorization Policies
│   │   └── ConfigurationKeys.cs  # Configuration Keys
│   ├── Enums/                    # Shared Enumerations
│   │   └── OperationResult.cs    # Result Types
│   ├── Exceptions/               # Custom Exceptions
│   │   ├── DomainException.cs
│   │   ├── ValidationException.cs
│   │   └── BusinessRuleException.cs
│   ├── Extensions/               # Extension Methods
│   │   ├── StringExtensions.cs
│   │   ├── DateTimeExtensions.cs
│   │   └── EnumExtensions.cs
│   ├── Models/                   # Shared Models
│   │   ├── ApiResponse.cs        # Standard API Response
│   │   ├── PagedResult.cs        # Pagination Model
│   │   └── OperationResult.cs    # Operation Result Wrapper
│   └── KGV.Shared.csproj
│
└── 🧪 Test Projects/             # Test-Bibliotheken (in derselben Solution)
    ├── KGV.Domain.Tests/         # Domain Layer Tests (CRITICAL!)
    │   ├── Entities/             # Entity Tests
    │   ├── ValueObjects/         # Value Object Tests
    │   ├── Services/             # Domain Service Tests  
    │   ├── Specifications/       # Business Rule Tests
    │   └── KGV.Domain.Tests.csproj
    ├── KGV.Application.Tests/    # Application Layer Tests
    │   ├── Commands/             # Command Handler Tests
    │   ├── Queries/              # Query Handler Tests
    │   ├── Handlers/             # Handler Integration Tests
    │   ├── Services/             # Service Tests
    │   └── KGV.Application.Tests.csproj
    ├── KGV.Infrastructure.Tests/ # Infrastructure Tests
    │   ├── Persistence/          # Repository Tests
    │   ├── Services/             # External Service Tests
    │   ├── Identity/             # Auth Tests
    │   └── KGV.Infrastructure.Tests.csproj
    └── KGV.WebApi.Tests/         # API Integration Tests
        ├── Controllers/          # Controller Tests
        ├── Middleware/           # Middleware Tests
        ├── Integration/          # Full Integration Tests
        ├── TestFixtures/         # Test Data & Helpers
        └── KGV.WebApi.Tests.csproj
```

---

## 🖥️ Frontend Struktur (`src/frontend/`)

### **Angular Feature-Module Architektur**

```
src/frontend/
├── angular.json                  # Angular CLI Configuration
├── package.json                  # NPM Dependencies
├── package-lock.json            # Locked Dependencies
├── tsconfig.json                # TypeScript Root Config
├── tsconfig.app.json            # App TypeScript Config
├── tsconfig.spec.json           # Test TypeScript Config
├── .browserslistrc              # Browser Compatibility
├── karma.conf.js                # Test Configuration
├── .eslintrc.json               # ESLint Configuration
├── tailwind.config.js           # Tailwind CSS Config (optional)
│
├── src/
│   ├── main.ts                   # Application Bootstrap
│   ├── index.html                # HTML Entry Point
│   ├── styles.scss               # Global Styles
│   │
│   ├── app/
│   │   ├── app.component.ts      # Root Component
│   │   ├── app.component.html
│   │   ├── app.component.scss
│   │   ├── app.module.ts         # Root Module
│   │   ├── app-routing.module.ts # Main Routing
│   │   │
│   │   ├── core/                 # 🔧 Core Module (Singletons)
│   │   │   ├── guards/           # Route Guards
│   │   │   │   ├── auth.guard.ts
│   │   │   │   ├── admin.guard.ts
│   │   │   │   └── role.guard.ts
│   │   │   ├── interceptors/     # HTTP Interceptors
│   │   │   │   ├── auth.interceptor.ts
│   │   │   │   ├── error.interceptor.ts
│   │   │   │   ├── loading.interceptor.ts
│   │   │   │   └── base-url.interceptor.ts
│   │   │   ├── services/         # Core Services
│   │   │   │   ├── auth.service.ts
│   │   │   │   ├── api.service.ts
│   │   │   │   ├── notification.service.ts
│   │   │   │   ├── loading.service.ts
│   │   │   │   └── storage.service.ts
│   │   │   ├── models/           # Core Domain Models
│   │   │   │   ├── user.model.ts
│   │   │   │   ├── auth-token.model.ts
│   │   │   │   ├── api-response.model.ts
│   │   │   │   └── pagination.model.ts
│   │   │   ├── constants/        # Application Constants
│   │   │   │   ├── api-endpoints.ts
│   │   │   │   ├── app-constants.ts
│   │   │   │   └── local-storage-keys.ts
│   │   │   └── core.module.ts    # Core Module Definition
│   │   │
│   │   ├── shared/               # 🔄 Shared Module (Reusable Components)
│   │   │   ├── components/       # Reusable UI Components
│   │   │   │   ├── loading-spinner/
│   │   │   │   │   ├── loading-spinner.component.ts
│   │   │   │   │   ├── loading-spinner.component.html
│   │   │   │   │   └── loading-spinner.component.scss
│   │   │   │   ├── confirmation-dialog/
│   │   │   │   ├── data-table/
│   │   │   │   ├── pagination/
│   │   │   │   └── error-message/
│   │   │   ├── directives/       # Custom Directives
│   │   │   │   ├── highlight.directive.ts
│   │   │   │   └── tooltip.directive.ts
│   │   │   ├── pipes/            # Custom Pipes
│   │   │   │   ├── format-aktenzeichen.pipe.ts
│   │   │   │   ├── truncate.pipe.ts
│   │   │   │   └── safe-html.pipe.ts
│   │   │   ├── validators/       # Custom Form Validators
│   │   │   │   ├── aktenzeichen.validator.ts
│   │   │   │   ├── plz.validator.ts
│   │   │   │   └── age.validator.ts
│   │   │   ├── utils/            # Utility Functions
│   │   │   │   ├── form.utils.ts
│   │   │   │   ├── date.utils.ts
│   │   │   │   └── validation.utils.ts
│   │   │   ├── models/           # Shared Models
│   │   │   │   ├── base.model.ts
│   │   │   │   └── form-error.model.ts
│   │   │   └── shared.module.ts  # Shared Module Definition
│   │   │
│   │   ├── features/             # 🎯 Feature Modules
│   │   │   │
│   │   │   ├── auth/             # Authentication Feature
│   │   │   │   ├── components/
│   │   │   │   │   ├── login/
│   │   │   │   │   │   ├── login.component.ts
│   │   │   │   │   │   ├── login.component.html
│   │   │   │   │   │   ├── login.component.scss
│   │   │   │   │   │   └── login.component.spec.ts
│   │   │   │   │   ├── logout/
│   │   │   │   │   ├── forgot-password/
│   │   │   │   │   └── change-password/
│   │   │   │   ├── services/
│   │   │   │   │   └── auth-api.service.ts
│   │   │   │   ├── models/
│   │   │   │   │   ├── login-request.model.ts
│   │   │   │   │   └── login-response.model.ts
│   │   │   │   ├── auth-routing.module.ts
│   │   │   │   └── auth.module.ts
│   │   │   │
│   │   │   ├── applications/     # Application Management Feature
│   │   │   │   ├── components/
│   │   │   │   │   ├── application-list/
│   │   │   │   │   ├── application-detail/
│   │   │   │   │   ├── application-form/
│   │   │   │   │   ├── application-status/
│   │   │   │   │   └── application-search/
│   │   │   │   ├── services/
│   │   │   │   │   └── applications-api.service.ts
│   │   │   │   ├── models/
│   │   │   │   │   ├── application.model.ts
│   │   │   │   │   ├── person-data.model.ts
│   │   │   │   │   ├── address.model.ts
│   │   │   │   │   └── aktenzeichen.model.ts
│   │   │   │   ├── store/         # NgRx State Management (optional)
│   │   │   │   │   ├── actions/
│   │   │   │   │   ├── effects/
│   │   │   │   │   ├── reducers/
│   │   │   │   │   └── selectors/
│   │   │   │   ├── applications-routing.module.ts
│   │   │   │   └── applications.module.ts
│   │   │   │
│   │   │   ├── waiting-list/     # Waiting List Management
│   │   │   │   ├── components/
│   │   │   │   │   ├── waiting-list-overview/
│   │   │   │   │   ├── waiting-list-entry/
│   │   │   │   │   ├── position-tracker/
│   │   │   │   │   └── ranking-display/
│   │   │   │   ├── services/
│   │   │   │   │   └── waiting-list-api.service.ts
│   │   │   │   ├── models/
│   │   │   │   │   ├── waiting-list.model.ts
│   │   │   │   │   └── waiting-list-entry.model.ts
│   │   │   │   ├── waiting-list-routing.module.ts
│   │   │   │   └── waiting-list.module.ts
│   │   │   │
│   │   │   ├── admin/            # Admin Feature
│   │   │   │   ├── components/
│   │   │   │   │   ├── admin-dashboard/
│   │   │   │   │   ├── user-management/
│   │   │   │   │   ├── system-settings/
│   │   │   │   │   └── reports/
│   │   │   │   ├── services/
│   │   │   │   │   ├── admin-api.service.ts
│   │   │   │   │   └── reports.service.ts
│   │   │   │   ├── models/
│   │   │   │   │   └── admin.model.ts
│   │   │   │   ├── admin-routing.module.ts
│   │   │   │   └── admin.module.ts
│   │   │   │
│   │   │   └── public/           # Public Pages (No Auth Required)
│   │   │       ├── components/
│   │   │       │   ├── home/
│   │   │       │   ├── about/
│   │   │       │   └── contact/
│   │   │       ├── public-routing.module.ts
│   │   │       └── public.module.ts
│   │   │
│   │   └── layout/               # 🖼️ Layout Components
│   │       ├── header/
│   │       │   ├── header.component.ts
│   │       │   ├── header.component.html
│   │       │   └── header.component.scss
│   │       ├── sidebar/
│   │       │   ├── sidebar.component.ts
│   │       │   ├── sidebar.component.html
│   │       │   └── sidebar.component.scss
│   │       ├── footer/
│   │       │   ├── footer.component.ts
│   │       │   ├── footer.component.html
│   │       │   └── footer.component.scss
│   │       ├── main-layout/
│   │       │   ├── main-layout.component.ts
│   │       │   ├── main-layout.component.html
│   │       │   └── main-layout.component.scss
│   │       └── breadcrumb/
│   │           ├── breadcrumb.component.ts
│   │           ├── breadcrumb.component.html
│   │           └── breadcrumb.component.scss
│   │
│   ├── assets/                   # 📁 Static Assets
│   │   ├── images/               # Images
│   │   │   ├── logo/
│   │   │   ├── icons/
│   │   │   └── backgrounds/
│   │   ├── fonts/                # Custom Fonts
│   │   ├── styles/               # SCSS Partials
│   │   │   ├── _variables.scss   # SCSS Variables
│   │   │   ├── _mixins.scss      # SCSS Mixins
│   │   │   ├── _base.scss        # Base Styles
│   │   │   ├── _components.scss  # Component Styles
│   │   │   └── _utilities.scss   # Utility Classes
│   │   └── i18n/                 # Internationalization
│   │       ├── en.json           # English Translations
│   │       └── de.json           # German Translations
│   │
│   ├── environments/             # Environment Configuration
│   │   ├── environment.ts        # Development Environment
│   │   ├── environment.prod.ts   # Production Environment
│   │   └── environment.staging.ts # Staging Environment
│   │
│   └── polyfills.ts              # Browser Polyfills
│
├── .vscode/                      # VSCode Configuration
│   ├── settings.json
│   ├── launch.json
│   └── extensions.json
│
└── dist/                         # Build Output (Generated)
    └── [Build artifacts]
```

---

## 🧪 Test Struktur

### **Backend Tests (in src/backend/ Solution)**
Backend-Tests sind als Test-Projekte in der gleichen .NET Solution organisiert:

```
src/backend/
├── KGV.sln                       # Enthält sowohl Haupt- als auch Test-Projekte
├── [Hauptprojekte...]
└── [Test-Projekte...]            # Alle Backend-Tests in derselben Solution
    ├── KGV.Domain.Tests/         # ⚠️ KRITISCH - Domain Tests
    ├── KGV.Application.Tests/    # Application Layer Tests  
    ├── KGV.Infrastructure.Tests/ # Infrastructure Tests
    └── KGV.WebApi.Tests/         # API Integration Tests
```

### **Frontend Tests (separates tests/ Verzeichnis)**
Frontend-Tests bleiben in einem eigenen Verzeichnis für Angular-spezifische Tools:

```
tests/                            # 🖥️ Nur Frontend Tests
├── unit/                         # Unit Tests (Jest)
│   ├── components/               # Component Tests
│   │   ├── auth/
│   │   ├── applications/
│   │   ├── waiting-list/
│   │   └── shared/
│   ├── services/                 # Service Tests
│   │   ├── auth.service.spec.ts
│   │   ├── api.service.spec.ts
│   │   └── applications-api.service.spec.ts
│   ├── pipes/                    # Pipe Tests
│   │   ├── format-aktenzeichen.pipe.spec.ts
│   │   └── truncate.pipe.spec.ts
│   └── validators/               # Validator Tests
│       ├── aktenzeichen.validator.spec.ts
│       └── plz.validator.spec.ts
├── integration/                  # Integration Tests
│   ├── auth/                     # Auth Flow Tests
│   ├── applications/             # Application Management Tests
│   └── waiting-list/             # Waiting List Tests
├── e2e/                          # End-to-End Tests (Playwright)
│   ├── page-objects/             # Page Object Model
│   │   ├── login.page.ts
│   │   ├── application-list.page.ts
│   │   └── waiting-list.page.ts
│   ├── test-data/                # Test Data
│   │   ├── users.json
│   │   ├── applications.json
│   │   └── test-scenarios.json
│   └── specs/                    # Test Specifications
│       ├── auth.spec.ts
│       ├── application-management.spec.ts
│       └── waiting-list.spec.ts
└── shared/                       # Shared Test Utilities
    ├── fixtures/                 # Test Data
    │   ├── mock-applications.ts
    │   ├── mock-users.ts
    │   └── mock-waiting-list.ts
    ├── helpers/                  # Test Helper Functions
    │   ├── auth.helpers.ts
    │   ├── component.helpers.ts
    │   └── api.helpers.ts
    └── mocks/                    # Mock Objects
        ├── services/
        │   ├── mock-auth.service.ts
        │   └── mock-api.service.ts
        └── data/
            ├── mock-responses.ts
            └── mock-requests.ts
```

---

## 📝 Naming Conventions

### **General Rules**
- **PascalCase**: Classes, Interfaces, Enums, Properties
- **camelCase**: Methods, Variables, Fields
- **kebab-case**: File names, URLs, CSS classes
- **UPPER_CASE**: Constants

### **Backend (.NET)**
```csharp
// ✅ Correct Naming
public class ApplicationController : ControllerBase
{
    private readonly IApplicationService _applicationService;
    
    public async Task<ActionResult<ApplicationDto>> GetApplication(int id)
    {
        const int MAX_RETRY_ATTEMPTS = 3;
        // ...
    }
}

// File: ApplicationController.cs
```

### **Frontend (Angular/TypeScript)**
```typescript
// ✅ Correct Naming
export class ApplicationListComponent implements OnInit {
  private readonly applicationService = inject(ApplicationService);
  
  public applicationList: Application[] = [];
  
  public async loadApplications(): Promise<void> {
    const MAX_PAGE_SIZE = 50;
    // ...
  }
}

// File: application-list.component.ts
```

### **File Naming Patterns**

#### **Backend Files**
- Controllers: `{Entity}Controller.cs`
- Services: `{Entity}Service.cs` oder `I{Entity}Service.cs`
- Models/DTOs: `{Entity}Dto.cs`
- Repositories: `{Entity}Repository.cs`
- Tests: `{Entity}Tests.cs`

#### **Frontend Files**
- Components: `{feature}-{type}.component.{ext}`
- Services: `{feature}-api.service.ts`
- Models: `{entity}.model.ts`
- Tests: `{file}.component.spec.ts`

---

## 🛡️ Domain Protection Integration

### **Geschützte Bereiche (CLAUDE.md Compliance)**

#### **Hochgeschützt (Änderungen verboten)**
```
src/backend/KGV.Domain/
├── ValueObjects/             # ❌ KEINE ÄNDERUNGEN
├── Constants/BusinessRules.cs # ❌ KEINE ÄNDERUNGEN
└── Specifications/           # ❌ ÄNDERUNGEN NUR MIT APPROVAL
```

#### **Geschützt (Review erforderlich)**
```
src/backend/KGV.Domain/
├── Entities/                 # ⚠️ REVIEW ERFORDERLICH
├── Services/                 # ⚠️ REVIEW ERFORDERLICH  
└── Events/                   # ⚠️ REVIEW ERFORDERLICH
```

#### **Entwicklungsfrei (Kreativität erlaubt)**
```
src/backend/
├── KGV.WebApi/              # ✅ FREI ENTWICKELBAR
├── KGV.Application/         # ✅ FREI ENTWICKELBAR
├── KGV.Infrastructure/      # ✅ FREI ENTWICKELBAR
└── KGV.Shared/             # ✅ FREI ENTWICKELBAR

src/frontend/               # ✅ KOMPLETT FREI ENTWICKELBAR
```

### **Domain Protection Workflow**
1. **Vor jeder Domain-Änderung**: CLAUDE.md lesen
2. **Domain Guard ausführen**: `.domain-guard/pre-commit-hook.sh`  
3. **Review einholen**: Bei Domain-kritischen Änderungen
4. **Dokumentation aktualisieren**: Nach genehmigten Änderungen

---

## 🔧 Development Workflow

### **Neue Features hinzufügen**

#### **Backend Feature**
1. **Domain Layer**: Neue Entities/Value Objects (wenn nötig)
2. **Application Layer**: Commands/Queries/Handlers
3. **Infrastructure Layer**: Repository-Implementierungen
4. **Web API Layer**: Controller-Endpoints
5. **Tests**: Unit- und Integration-Tests

#### **Frontend Feature**
1. **Feature Module**: Neuen Ordner unter `features/`
2. **Components**: UI-Komponenten entwickeln
3. **Services**: API-Integration Services
4. **Routing**: Feature-Routing konfigurieren
5. **Tests**: Component- und Service-Tests

### **Code Organisation Best Practices**

1. **Ein Feature = Ein Ordner**: Alles zusammengehörige an einem Ort
2. **Lazy Loading**: Features als separate Module laden
3. **Barrel Exports**: `index.ts` für saubere Imports
4. **Shared Code**: Nur wirklich wiederverwendbare Komponenten
5. **Domain Alignment**: Frontend-Struktur spiegelt Backend-Domain

---

## 🚀 Nächste Schritte

### **Sofort implementieren:**
1. **Verzeichnisstruktur anlegen** (Issue #4 vorbereiten)
2. **EditorConfig erstellen** (konsistente Formatierung)
3. **Basic Configuration Files** (Angular/ASP.NET)

### **Nach Issue #4 (Angular Foundation):**
1. **Feature Module Templates** erstellen
2. **Code Generators** für wiederkehrende Patterns
3. **Development Guidelines** verfeinern

### **Langfristig:**
1. **Automated Architecture Tests** (ArchUnit.NET)
2. **Code Quality Gates** (SonarQube Integration)
3. **Documentation Generation** (automatisierte API-Docs)

---

**🎯 Diese Struktur gewährleistet saubere Architektur, Domain-Schutz und effiziente Team-Zusammenarbeit!**