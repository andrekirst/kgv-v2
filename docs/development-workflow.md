# KGV-System Development Workflow Guidelines

## 🎯 Overview

This document defines the development workflow for the KGV (Kleingartenverwaltung) System, ensuring domain model stability while enabling rapid feature development.

---

## 🛡️ Domain-First Development Process

### Phase 1: Domain Analysis (MANDATORY)

#### **1.1 Pre-Development Checklist**
- [ ] Read CLAUDE.md domain protection guidelines
- [ ] Review relevant sections of domain-model-documentation.md
- [ ] Identify all domain concepts involved in the feature
- [ ] Determine if domain modifications are needed
- [ ] If domain changes required → **STOP and request approval**

#### **1.2 Domain Impact Assessment**
```markdown
## Feature: [Feature Name]

### Domain Concepts Involved:
- [ ] Aktenzeichen (File numbers)
- [ ] PersonData (Applicant information)  
- [ ] Address (Contact information)
- [ ] Application (Main aggregate)
- [ ] WaitingList (Ranking system)
- [ ] Status transitions
- [ ] Business rules

### Domain Modifications Required:
- [ ] None (✅ Proceed with implementation)
- [ ] Minor (value object extensions) → Request review
- [ ] Major (business rule changes) → Request approval
- [ ] Breaking (event schema changes) → **HALT - Architecture review**

### Implementation Strategy:
- Application Layer: [How will you implement without domain changes]
- Infrastructure Layer: [Database, repositories, external services]
- Presentation Layer: [API, DTOs, UI components]
```

---

## 🏗️ Layer-Based Development

### **2.1 Domain Layer (READ-ONLY)**
```
Domain Layer (⚠️ IMMUTABLE)
├── Entities/
│   ├── Application.cs (PROTECTED)
│   ├── WaitingList.cs (PROTECTED)  
│   └── ...
├── ValueObjects/
│   ├── Aktenzeichen.cs (IMMUTABLE)
│   ├── PersonData.cs (IMMUTABLE)
│   └── Address.cs (IMMUTABLE)
├── DomainEvents/
│   ├── ApplicationCreated.cs (VERSIONED)
│   └── ...
└── BusinessRules/
    └── ApplicationRules.cs (CONSTANTS)
```

**Development Rule**: Copy exact implementations from documentation, never modify.

### **2.2 Application Layer (CREATIVE ZONE)**
```
Application Layer (✅ FULL ACCESS)
├── Commands/
│   ├── CreateApplicationCommand.cs
│   └── UpdateApplicationStatusCommand.cs
├── Queries/
│   ├── GetApplicationQuery.cs
│   └── GetWaitingListQuery.cs
├── Handlers/
│   ├── CreateApplicationHandler.cs
│   └── ApplicationStatusHandler.cs
├── Services/
│   ├── ApplicationService.cs
│   └── WaitingListService.cs
└── DTOs/
    ├── ApplicationDto.cs
    └── WaitingListDto.cs
```

**Development Rules**:
- ✅ Create new commands, queries, handlers
- ✅ Add validation logic (using domain rules)  
- ✅ Implement business workflows
- ✅ Create DTOs with computed properties
- ✅ Add caching, logging, error handling

### **2.3 Infrastructure Layer (CREATIVE ZONE)**
```
Infrastructure Layer (✅ FULL ACCESS)
├── Persistence/
│   ├── Configurations/
│   │   ├── ApplicationConfiguration.cs
│   │   └── WaitingListConfiguration.cs
│   ├── Repositories/
│   │   ├── ApplicationRepository.cs
│   │   └── WaitingListRepository.cs
│   └── Migrations/
├── External/
│   ├── EmailService.cs
│   └── DocumentService.cs
└── Caching/
    └── RedisCacheService.cs
```

**Development Rules**:
- ✅ Configure Entity Framework mappings
- ✅ Implement repository patterns
- ✅ Create database migrations
- ✅ Integrate external services
- ✅ Add performance optimizations

### **2.4 Presentation Layer (CREATIVE ZONE)**
```
Presentation Layer (✅ FULL ACCESS)
├── Controllers/
│   ├── ApplicationController.cs
│   └── WaitingListController.cs
├── Models/
│   ├── ApplicationRequest.cs
│   └── ApplicationResponse.cs
├── Validators/
│   ├── ApplicationValidator.cs
│   └── PersonDataValidator.cs
└── Middleware/
    ├── ErrorHandlingMiddleware.cs
    └── AuthenticationMiddleware.cs
```

**Development Rules**:
- ✅ Design RESTful APIs
- ✅ Create request/response models
- ✅ Implement validation
- ✅ Add authentication/authorization
- ✅ Handle errors and logging

---

## 🔄 Feature Development Workflow

### **Step 1: Planning and Design**
```bash
# Create feature branch
git checkout -b feature/waiting-list-export

# Document the feature
touch docs/features/waiting-list-export.md
```

**Feature Documentation Template**:
```markdown
# Feature: Waiting List Export

## Business Requirements
- Export waiting list data to Excel/PDF
- Filter by gemarkung and date range
- Include applicant contact information

## Domain Impact Analysis
- ✅ No domain modifications required
- Uses existing WaitingList aggregate (read-only)
- Uses existing PersonData value object (read-only)

## Implementation Plan
1. Application Layer: Create ExportWaitingListQuery
2. Infrastructure Layer: Implement Excel/PDF generation  
3. Presentation Layer: Add export endpoints
4. Frontend: Add export buttons and progress indicators

## Testing Strategy
- Unit tests for query handlers
- Integration tests for export generation
- E2E tests for complete export workflow
```

### **Step 2: Implementation (Layer by Layer)**

#### **2.1 Start with Application Layer**
```csharp
// Application/Queries/ExportWaitingListQuery.cs
public record ExportWaitingListQuery(
    WaitingListType Type,
    string? GemarkungFilter = null,
    DateTime? FromDate = null,
    DateTime? ToDate = null,
    ExportFormat Format = ExportFormat.Excel
) : IRequest<ExportResult>;

// Application/Handlers/ExportWaitingListHandler.cs  
public class ExportWaitingListHandler : IRequestHandler<ExportWaitingListQuery, ExportResult>
{
    // Uses domain objects as-is, no modifications
    // Creates DTOs with additional computed properties
}
```

#### **2.2 Infrastructure Implementation**
```csharp
// Infrastructure/Export/ExcelExportService.cs
public class ExcelExportService : IExportService
{
    public async Task<byte[]> ExportWaitingListAsync(IEnumerable<WaitingListEntry> entries)
    {
        // Implementation using ClosedXML or similar
        // Maps domain objects to export format
    }
}
```

#### **2.3 Presentation Layer**
```csharp
// Presentation/Controllers/ExportController.cs
[ApiController]
[Route("api/export")]
public class ExportController : ControllerBase
{
    [HttpPost("waiting-list")]
    public async Task<IActionResult> ExportWaitingList([FromBody] ExportWaitingListRequest request)
    {
        // Maps request to query
        // Returns file download
    }
}
```

### **Step 3: Testing and Validation**

#### **3.1 Run Domain Protection Checks**
```bash
# Run domain guard validation
.domain-guard/pre-commit-hook.sh

# Check domain compliance
git diff --name-only | grep -E '\.(cs)$' | xargs grep -l "Domain" | while read file; do
    echo "Checking $file for domain violations..."
    # Automated checks here
done
```

#### **3.2 Comprehensive Testing**
```csharp
// Tests/Application/ExportWaitingListHandlerTests.cs
[Test]
public async Task ExportWaitingList_WithValidData_ReturnsExcelFile()
{
    // Arrange: Use domain objects from documentation
    var aktenzeichen = Aktenzeichen.Create(WaitingListType.Nr32, 123, 2024).Value;
    var personData = new PersonData("Max", "Mustermann", /* ... */);
    
    // Act & Assert: Test application logic without domain changes
}
```

### **Step 4: Code Review and Merge**

#### **4.1 Pre-Review Checklist**
- [ ] Domain guard checks pass
- [ ] No domain layer modifications
- [ ] All business rules from documentation implemented correctly  
- [ ] Event schemas unchanged (if applicable)
- [ ] Unit tests cover new functionality
- [ ] Integration tests validate end-to-end flow

#### **4.2 Pull Request Template**
```markdown
## Feature: [Feature Name]

### 🎯 Business Value
[What business problem does this solve?]

### 🛡️ Domain Compliance
- [ ] No domain model modifications
- [ ] Uses exact domain implementations from documentation
- [ ] All business rules implemented as specified
- [ ] Event schemas unchanged (if applicable)

### 🏗️ Implementation Summary
- **Application Layer**: [What was added/changed]
- **Infrastructure Layer**: [What was added/changed]  
- **Presentation Layer**: [What was added/changed]

### 🧪 Testing
- [ ] Unit tests added/updated
- [ ] Integration tests cover main scenarios
- [ ] Manual testing completed
- [ ] Performance impact assessed

### 📋 Verification Steps
1. Domain guard validation passed
2. All tests pass
3. Feature works as expected
4. No performance degradation
```

---

## 🚨 Emergency Procedures

### **When Domain Changes Are Required**

#### **Step 1: Immediate Actions**
- 🛑 **STOP** all implementation work
- 📝 Document the specific change requirement
- 🔍 Analyze business impact and justification
- 📋 Create domain change request

#### **Step 2: Domain Change Request Process**
```markdown
# 🚨 DOMAIN CHANGE REQUEST

**Date**: [Current Date]
**Requester**: [Your Name]
**Feature**: [Feature Name]

## Current Domain State
**Reference**: docs/domain-model-documentation.md#[relevant-section]
**Current Implementation**: 
```csharp
// Copy exact current implementation
```

## Proposed Change
**Modification**: [Specific change needed]
**Justification**: [Business reason - why is this necessary?]
**Alternatives Considered**: [What non-domain solutions were explored?]

## Impact Analysis
- **Affected Aggregates**: [List all affected domain objects]
- **Event Schema Impact**: [Will this break existing events?]
- **Migration Requirements**: [Data migration needed?]
- **Backward Compatibility**: [How will existing data be handled?]

## Implementation Plan
1. **Documentation Update**: Update domain-model-documentation.md first
2. **Schema Migration**: [Database changes required]
3. **Code Changes**: [Specific files to modify]
4. **Testing Strategy**: [How to validate the change]
5. **Rollback Plan**: [How to revert if needed]

## Approval Required From
- [ ] Domain Architect
- [ ] Business Stakeholder  
- [ ] Technical Lead
```

#### **Step 3: While Waiting for Approval**
- ✅ Continue with non-domain work (UI, infrastructure, documentation)
- ✅ Implement feature with temporary workarounds
- ✅ Prepare alternative solutions
- ❌ Do NOT modify domain objects

#### **Step 4: After Approval**
1. **Update documentation FIRST**
   - Modify docs/domain-model-documentation.md
   - Update ADR (Architecture Decision Record)
   - Update CLAUDE.md if protection rules change

2. **Implement changes**
   - Follow updated documentation exactly
   - Maintain backward compatibility
   - Add migration scripts

3. **Validate implementation**
   - Run all tests
   - Verify event sourcing compatibility
   - Check performance impact

---

## 🏃‍♂️ Daily Development Practices

### **Morning Routine**
1. **Pull latest changes**
   ```bash
   git pull origin main
   ```

2. **Review domain documentation**
   - Quick scan of CLAUDE.md for any updates
   - Check domain-model-documentation.md for relevant sections

3. **Plan the day**
   - Identify domain concepts you'll work with
   - Confirm no domain modifications needed
   - Plan application/infrastructure work

### **During Development**
1. **Keep domain docs open**
   - Always reference exact implementations
   - Copy-paste code templates
   - Validate business rules

2. **Use domain guard checks**
   ```bash
   # Before committing
   .domain-guard/pre-commit-hook.sh
   ```

3. **Focus on creative layers**
   - Application services for business logic
   - Infrastructure for performance optimizations
   - Presentation for user experience

### **Before Commits**
1. **Domain compliance check**
   - No modifications to protected files
   - Business rules implemented correctly
   - Event schemas unchanged

2. **Testing validation**
   ```bash
   # Run tests with domain validation
   dotnet test --configuration Release
   ```

3. **Documentation updates**
   - Feature documentation current
   - API documentation updated
   - README changes if needed

---

## 🎯 Success Metrics

### **Domain Stability Indicators**
- ✅ **Zero unplanned domain changes** in the last sprint
- ✅ **All features delivered** without domain modifications
- ✅ **Domain guard checks** pass consistently
- ✅ **Event sourcing compatibility** maintained
- ✅ **Backward compatibility** preserved

### **Development Velocity**
- ✅ **Features delivered on time** without domain rework
- ✅ **Pull requests focus** on application/infrastructure layers
- ✅ **Code reviews** complete quickly (no domain discussions)
- ✅ **Testing coverage** high for business logic
- ✅ **Technical debt** low (clean separation of concerns)

### **Team Performance**
- ✅ **Consistent domain understanding** across team members
- ✅ **Fast onboarding** for new developers
- ✅ **Clear development guidelines** followed
- ✅ **Proactive domain protection** by all team members
- ✅ **Effective collaboration** between domain and implementation

---

## 🛠️ Tools and Resources

### **Essential Commands**
```bash
# Domain validation
.domain-guard/pre-commit-hook.sh

# Install protection hooks
.domain-guard/install-hooks.sh

# Feature branch creation
git checkout -b feature/[feature-name]

# Domain-safe commit
git commit -m "feat: add waiting list export functionality

- Implements ExportWaitingListQuery in Application layer
- Adds Excel/PDF generation in Infrastructure layer  
- Creates export endpoints in Presentation layer
- Uses existing domain objects without modifications

🛡️ Domain-safe: No domain layer changes"
```

### **Documentation Quick Links**
- 🛡️ [Domain Protection](../CLAUDE.md) - Protection guidelines
- 📚 [Domain Model](domain-model-documentation.md) - Complete specifications
- 🏗️ [Architecture](architecture-documentation.md) - System design
- 📋 [Business Rules](business-requirements.md) - Requirements
- 🎨 [UX Guidelines](ux-design-guide.md) - User experience

### **VS Code Extensions**
```json
// .vscode/extensions.json
{
  "recommendations": [
    "ms-dotnettools.csharp",
    "bradlc.vscode-tailwindcss",
    "esbenp.prettier-vscode",
    "ms-vscode.vscode-json"
  ]
}
```

### **Git Hooks Configuration**
```bash
# Setup domain protection hooks
cd .git/hooks
ln -sf ../../.domain-guard/pre-commit-hook.sh pre-commit
chmod +x pre-commit
```

---

This workflow ensures that the KGV domain model remains stable and unchanging while enabling rapid, creative development in the application, infrastructure, and presentation layers. By following these guidelines, the team can deliver features efficiently without compromising the domain model's integrity.

🎉 **Remember**: The domain is our contract with the business - treat it as law, and build amazing features around it!