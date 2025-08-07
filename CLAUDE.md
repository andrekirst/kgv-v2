# KGV-System Domain Contract & Development Guidelines

## 🚨 CRITICAL: Domain Model Stability Protocol

**This document serves as an IMMUTABLE CONTRACT for the KGV (Kleingartenverwaltung) System domain model. Any modifications to the domain layer require explicit architectural approval.**

---

## 🛡️ PROTECTED DOMAIN MODEL

### Core Domain Types (⚠️ IMMUTABLE - DO NOT MODIFY)

These domain objects are **CARVED IN STONE** and must NOT be changed without explicit approval:

#### **Value Objects (IMMUTABLE)**

- `Aktenzeichen` - File number format: "32.2 [Number] [Year]" (e.g., "32.2 128 2024")
- `PersonData` - Person information with validation (FirstName, LastName, DateOfBirth, Email, Phone)
- `Address` - German address format (Street, HouseNumber, PostalCode, City, Country)

#### **Aggregate Roots (PROTECTED)**

- `Application` - Main business entity with complete lifecycle management
- `WaitingList` - Secondary aggregate for FIFO ranking (Nr32 and Nr33)

#### **Entities (CONTROLLED)**

- `ApplicationStatusChange` - Audit trail entries
- `ApplicationExtension` - Validity extensions (max 3)
- `WaitingListEntry` - Individual waiting list positions

### Business Rules (🔒 LOCKED CONSTANTS)

```csharp
// These values are BUSINESS REQUIREMENTS - DO NOT CHANGE
public static class ApplicationBusinessRules
{
    public const int InitialValidityMonths = 12;  // IMMUTABLE
    public const int ExtensionMonths = 12;        // IMMUTABLE
    public const int MaxExtensions = 3;           // IMMUTABLE

    // Total maximum validity: 4 years (12 + 3*12 months)
}

// Status transitions are FIXED business rules
Pending → {Active, Rejected}
Active → {Offered, Rejected}
Offered → {Assigned, Active, Rejected}
Assigned → {Completed}
Rejected/Completed → TERMINAL (no further transitions)
```

### Domain Events (📋 VERSIONED - NO BREAKING CHANGES)

```csharp
// Event schemas are VERSIONED - maintain backward compatibility
ApplicationCreated v1.0.0       // STABLE SCHEMA
ApplicationStatusChanged v1.0.0 // STABLE SCHEMA
ApplicationExtended v1.0.0      // STABLE SCHEMA
AllotmentOffered v1.0.0         // STABLE SCHEMA
```

---

## ✅ ALLOWED DEVELOPMENT AREAS

You can freely work on these layers:

### **Infrastructure Layer**

- Entity Framework configurations
- Repository implementations
- Database migrations
- External service integrations
- Caching strategies

### **Application Layer**

- CQRS Commands and Queries
- Command/Query Handlers
- Application Services
- DTOs and Mapping
- Validation (using domain rules)

### **Presentation Layer**

- Web API Controllers
- Request/Response models
- Authentication/Authorization
- Swagger documentation
- Error handling

### **Frontend (Angular)**

- Components and Services
- State Management (NgRx)
- HTTP interceptors
- UI/UX implementations
- Forms and validation

---

## ❌ FORBIDDEN WITHOUT EXPLICIT APPROVAL

**STOP IMMEDIATELY** if you need to:

### **Domain Layer Modifications**

- ❌ Add/remove/modify properties in Domain Entities
- ❌ Change Value Object structures or validation
- ❌ Alter Business Rule constants or logic
- ❌ Modify Domain Event schemas
- ❌ Change Aggregate Root boundaries
- ❌ Introduce new Domain Exceptions

### **Breaking Changes**

- ❌ Rename domain concepts or properties
- ❌ Change property types or constraints
- ❌ Modify business validation rules
- ❌ Alter event serialization formats
- ❌ Remove existing functionality

---

## 🛠️ IMPLEMENTATION GUIDELINES

### **Domain Reference Strategy**

**ALWAYS** reference the authoritative domain documentation:

- 📚 **Complete Specification**: `/docs/domain-model-documentation.md`
- 🏗️ **Architecture Details**: `/docs/architecture-documentation.md`
- 📋 **Business Requirements**: `/docs/business-requirements.md`

### **Code Implementation Rules**

1. **Copy, Don't Modify**: Use EXACT code templates from documentation
2. **Reference, Don't Guess**: Always check domain documentation first
3. **Ask, Don't Assume**: Clarify unclear domain logic before implementing
4. **Validate, Don't Skip**: Ensure implementation matches specifications exactly

### **Domain Object Implementation Templates**

#### **Aktenzeichen (EXACT IMPLEMENTATION)**

```csharp
public sealed record Aktenzeichen
{
    private static readonly Regex Pattern =
        new(@"^(32\.2|33\.2)\s+(\d+)\s+(\d{4})$", RegexOptions.Compiled);

    public string Value { get; }
    public string Prefix { get; }          // "32.2" or "33.2"
    public int Number { get; }             // Sequential number
    public int Year { get; }               // Application year
    public WaitingListType ListType { get; } // Nr32 or Nr33

    // IMPLEMENTATION: Use exact factory methods from documentation
    public static Result<Aktenzeichen> Create(WaitingListType listType, int number, int year)
    public static Result<Aktenzeichen> Parse(string value)
}
```

#### **PersonData (IMMUTABLE VALUE OBJECT)**

```csharp
public sealed record PersonData
{
    public string FirstName { get; }      // Required, min 1 char
    public string LastName { get; }       // Required, min 1 char
    public DateTime DateOfBirth { get; }  // Required, min 18 years old
    public string Email { get; }          // Optional, RFC format
    public string Phone { get; }          // Optional

    // Constructor includes validation - DO NOT MODIFY
    public PersonData(string firstName, string lastName, DateTime dateOfBirth, string email, string phone)

    public string FullName => $"{FirstName} {LastName}"; // Computed property
}
```

#### **Application Status Transitions (BUSINESS RULES)**

```csharp
// FIXED transition rules - implement exactly as specified
private static readonly Dictionary<ApplicationStatus, ApplicationStatus[]> ValidTransitions = new()
{
    [ApplicationStatus.Pending] = new[] { ApplicationStatus.Active, ApplicationStatus.Rejected },
    [ApplicationStatus.Active] = new[] { ApplicationStatus.Offered, ApplicationStatus.Rejected },
    [ApplicationStatus.Offered] = new[] { ApplicationStatus.Assigned, ApplicationStatus.Active, ApplicationStatus.Rejected },
    [ApplicationStatus.Assigned] = new[] { ApplicationStatus.Completed },
    [ApplicationStatus.Rejected] = Array.Empty<ApplicationStatus>(),
    [ApplicationStatus.Completed] = Array.Empty<ApplicationStatus>()
};
```

---

## 🔄 DEVELOPMENT WORKFLOW

### **Phase 1: Planning (MANDATORY)**

1. 📖 **Review domain documentation** thoroughly
2. 🎯 **Identify domain concepts** involved in the task
3. 🛠️ **Plan implementation** without domain modifications
4. ⚠️ **If domain changes needed** → STOP and request approval

### **Phase 2: Implementation**

1. 🏗️ **Infrastructure first**: Entity Framework, repositories, migrations
2. 🎮 **Application layer**: Commands, queries, handlers (using domain rules)
3. 🌐 **Presentation layer**: Controllers, DTOs, API endpoints
4. 📋 **Domain layer**: COPY-PASTE from documentation (no modifications)

### **Phase 3: Validation**

1. ✅ **Domain compliance**: All objects match documentation exactly
2. ✅ **Business rules**: Implementation follows specifications
3. ✅ **Event schemas**: Comply with versioned formats
4. ✅ **Tests**: Validate against domain specifications

### **Phase 4: Pull Request Creation (MANDATORY)**

When creating Pull Requests, ALWAYS apply the following steps automatically:

1. 🏷️ **Copy Labels from Issue**: Apply all labels from the source issue to the PR
2. 📋 **Add to Project**: Add PR to the same project board as the source issue
3. 🎯 **Set Milestone**: Set the same milestone as the source issue
4. 🔗 **Link Issue**: Use keywords (Closes #X, Fixes #X, Resolves #X) to auto-close issue on merge
5. 📝 **Reference Implementation**: Include issue number and reference in PR title and description

**Pull Request Template:**
```bash
gh pr create \
  --title "feat: [Feature Name] - Closes #[ISSUE_NUMBER]" \
  --body "$(cat <<'EOF'
## Summary
- Brief description of implemented feature
- Reference to Issue #[ISSUE_NUMBER]

## Implementation Details
- Key technical decisions and features
- Architecture patterns used

## Test Plan
- [x] All acceptance criteria from Issue #[ISSUE_NUMBER] met
- [x] Tests pass
- [x] Build succeeds

Closes #[ISSUE_NUMBER]

🤖 Generated with [Claude Code](https://claude.ai/code)
EOF
)" \
  --label "[COPY_FROM_ISSUE]" \
  --milestone "[COPY_FROM_ISSUE]" \
  --project "[COPY_FROM_ISSUE]"
```

**CRITICAL**: Never create PRs without proper issue linking, labels, milestones, and project assignment!

### **Phase 5: Git Push (MANDATORY)**

After EVERY successful commit, ALWAYS push the changes to the remote repository immediately:

1. 🚀 **Automatic Push**: After `git commit`, immediately run `git push`
2. 🔄 **Never accumulate commits**: Each commit should be pushed immediately  
3. 📡 **Remote sync**: Ensure all changes are backed up and available to team
4. ✅ **Workflow completion**: A commit is not complete until it's pushed

**Command sequence:**
```bash
git add [files]
git commit -m "commit message"
git push  # ← MANDATORY: Always push immediately after commit
```

**CRITICAL**: Every commit must be followed by an immediate push to maintain workflow integrity!

---

## 🚨 DOMAIN CHANGE PROTOCOL

If you need to modify the domain model:

### **Step 1: HALT Implementation**

- ⏸️ Stop current implementation immediately
- 📝 Document the specific change needed
- 📋 Explain business justification

### **Step 2: Request Approval**

```markdown
🚨 DOMAIN CHANGE REQUEST

**Current Implementation**: [Reference current domain documentation]
**Proposed Change**: [Specific modification needed]  
**Business Justification**: [Why this change is necessary]
**Impact Analysis**: [What will be affected]
**Backward Compatibility**: [How to maintain compatibility]
```

### **Step 3: Wait for Approval**

- ⏳ Do not proceed with domain changes
- 🔄 Continue with non-domain work if possible
- ✅ Only implement after explicit approval

### **Step 4: Update Documentation First**

- 📝 Update `/docs/domain-model-documentation.md` first
- 🔄 Update relevant ADRs (Architecture Decision Records)
- 📋 Update business requirements if needed
- ✅ Then implement the approved changes

---

## 🔍 SELF-CHECK QUESTIONS

**Before ANY domain-related implementation, ask yourself:**

1. ❓ **Am I modifying anything in the Domain layer?**

   - If YES → Review this document and get approval

2. ❓ **Are my property names exactly matching documentation?**

   - If NO → Use exact names from domain specifications

3. ❓ **Are my validation rules identical to specifications?**

   - If NO → Implement exactly as documented

4. ❓ **Do my events match the exact schema versions?**

   - If NO → Use versioned schemas from documentation

5. ❓ **Have I introduced any new business rules?**
   - If YES → This requires domain model approval

**If ANY answer triggers a concern → STOP and clarify**

---

## 🎯 DOMAIN COMPLIANCE CHECKLIST

### **Implementation Checklist**

- [ ] Referenced domain documentation before starting
- [ ] Used exact property names from specifications
- [ ] Implemented business rules as documented (no modifications)
- [ ] Followed event schemas exactly (correct versions)
- [ ] No domain layer modifications introduced
- [ ] All validation logic matches domain specifications
- [ ] Status transitions follow documented rules
- [ ] Value Objects implemented as immutable records

### **Quality Assurance**

- [ ] Domain objects serialize/deserialize correctly
- [ ] Business rules prevent invalid operations
- [ ] Event sourcing compatibility maintained
- [ ] Backward compatibility preserved
- [ ] Unit tests validate domain specifications
- [ ] Integration tests use documented scenarios

---

## 📚 QUICK REFERENCE LINKS

### **Essential Documentation**

- 🎯 **Domain Model Core**: [`/docs/domain-model-documentation.md#core-domain-types`](docs/domain-model-documentation.md#core-domain-types)
- 📋 **Business Rules**: [`/docs/domain-model-documentation.md#business-rules-katalog`](docs/domain-model-documentation.md#business-rules-katalog)
- 🔄 **Event Schemas**: [`/docs/domain-model-documentation.md#event-schema-evolution`](docs/domain-model-documentation.md#event-schema-evolution)
- 🛡️ **Validation Rules**: [`/docs/domain-model-documentation.md#invariants-und-constraints`](docs/domain-model-documentation.md#invariants-und-constraints)
- 🏗️ **Architecture**: [`/docs/architecture-documentation.md`](docs/architecture-documentation.md)
- 🌿 **Git Workflow**: [`/docs/git-workflow-guide.md`](docs/git-workflow-guide.md)

### **Implementation References**

- 🔧 **Entity Framework Setup**: Reference architecture documentation
- 🎮 **CQRS Implementation**: Use application layer patterns
- 🌐 **API Design**: Follow RESTful conventions with domain DTOs
- 🧪 **Testing Strategy**: Domain specifications as test cases

### **Context7 Integration for Up-to-Date Documentation**

When working with external libraries, frameworks, or APIs, ALWAYS use Context7 to get current, accurate documentation and code examples:

**Usage Pattern:**
1. 📝 **Write your prompt naturally** describing what you want to implement
2. 🔍 **Add "use context7"** to your prompt to fetch up-to-date documentation
3. 💡 **Get working code answers** with current APIs and best practices

**Examples:**
- "Create an Angular service for HTTP API calls with error handling. use context7"
- "Implement JWT authentication middleware in .NET Core. use context7"  
- "Configure Entity Framework with PostgreSQL connection. use context7"
- "Set up Angular Material theme customization. use context7"

**Benefits:**
- ✅ **Current Documentation**: Always up-to-date library docs and examples
- ✅ **No Hallucinated APIs**: Real, working code examples from source
- ✅ **Version-Specific**: Gets documentation for exact library versions
- ✅ **Best Practices**: Official patterns and recommended approaches

**CRITICAL**: Always use Context7 when implementing features with external dependencies to ensure code accuracy and current best practices!

---

## 🔥 COMMON MISTAKES TO AVOID

### **❌ Domain Violations**

```csharp
// DON'T: Add helpful properties to domain objects
public class PersonData
{
    public string DisplayName { get; set; } // ❌ Domain modification
}

// DO: Use DTOs in application layer
public class PersonDisplayDto
{
    public string DisplayName { get; set; } // ✅ Application layer
}
```

### **❌ Business Rule Changes**

```csharp
// DON'T: Make rules more flexible
public const int MaxExtensions = 5; // ❌ Changes business requirement

// DO: Use exact constants from documentation
public const int MaxExtensions = 3; // ✅ As specified in domain model
```

### **❌ Event Schema Modifications**

```csharp
// DON'T: Improve event structure
public record ApplicationCreated
{
    public PersonData Applicant { get; set; }        // ❌ Breaking change
    public PersonData SecondaryApplicant { get; set; } // ❌ Schema violation
}

// DO: Use exact event schemas with proper versioning
public record ApplicationCreated : IDomainEvent
{
    public string EventVersion { get; init; } = "1.0.0";  // ✅ Versioned
    public PersonData PrimaryApplicant { get; init; }     // ✅ Exact naming
    public PersonData? SecondaryApplicant { get; init; }  // ✅ As documented
}
```

---

## 💡 PRODUCTIVITY TIPS

### **Efficient Development**

1. **Keep domain documentation open** while coding
2. **Copy code templates** instead of writing from scratch
3. **Use quick reference links** for fast lookups
4. **Focus on application/infrastructure** layers for creativity
5. **Ask specific questions** when domain logic is unclear

### **Avoiding Rework**

1. **Plan non-domain solutions** for new requirements
2. **Use DTOs liberally** in application and presentation layers
3. **Implement business logic** in application services (using domain rules)
4. **Create computed properties** in DTOs, not domain objects
5. **Extend through composition**, not domain modification

---

## 🎉 SUCCESS INDICATORS

### **You're doing it RIGHT when:**

- ✅ Domain objects remain exactly as documented
- ✅ Business rules are implemented without modification
- ✅ Event sourcing works without schema changes
- ✅ Tests validate against domain specifications
- ✅ New features don't require domain changes
- ✅ Pull requests focus on application/infrastructure layers

### **System Health Metrics:**

- 🔒 **Domain Stability**: No unplanned domain modifications
- 🧪 **Test Coverage**: Domain specifications as test cases
- 🔄 **Event Compatibility**: All events deserialize correctly
- 📈 **Development Velocity**: Fast feature delivery without domain rework
- 👥 **Team Alignment**: Consistent domain understanding

---

**Remember: The domain model is our contract with the business. Treat it as immutable law, and build amazing features around it! 🚀**
