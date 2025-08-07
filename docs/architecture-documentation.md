# KGV-System Architektur-Dokumentation

## Überblick

Diese Dokumentation beschreibt die technische Architektur der modernen KGV (Kleingartenverwaltung) Lösung basierend auf .NET 9 Web API und Angular Frontend.

## 1. Architektur-Übersicht

### High-Level Architektur
```
┌─────────────────────────────────────────────────────────────┐
│                    Client Tier                              │
├─────────────────────────────────────────────────────────────┤
│  Angular SPA (PWA)     │  Mobile Browser    │  Desktop      │
│  - TypeScript          │  - Responsive      │  - Chrome     │
│  - Angular Material    │  - Touch-optimiert │  - Edge       │
│  - RxJS State          │  - Offline-fähig   │  - Firefox    │
└─────────────────────────────────────────────────────────────┘
                               │ HTTPS/REST
┌─────────────────────────────────────────────────────────────┐
│                 Application Tier                            │
├─────────────────────────────────────────────────────────────┤
│              ASP.NET Core Web API (.NET 9)                 │
│  ┌─────────────────────────────────────────────────────────┐│
│  │                Presentation Layer                       ││
│  │  - Controllers (RESTful APIs)                          ││
│  │  - Middleware (Auth, Logging, CORS)                    ││
│  │  - OpenAPI/Swagger Documentation                       ││
│  └─────────────────────────────────────────────────────────┘│
│  ┌─────────────────────────────────────────────────────────┐│
│  │                Application Layer                        ││
│  │  - Use Cases / Application Services                    ││
│  │  - CQRS Commands & Queries                             ││
│  │  - MediatR Pipeline                                    ││
│  │  - Validation, Mapping                                 ││
│  └─────────────────────────────────────────────────────────┘│
│  ┌─────────────────────────────────────────────────────────┐│
│  │                 Domain Layer                            ││
│  │  - Domain Entities & Value Objects                     ││
│  │  - Domain Services                                     ││
│  │  - Repository Interfaces                               ││
│  │  - Domain Events                                       ││
│  └─────────────────────────────────────────────────────────┘│
│  ┌─────────────────────────────────────────────────────────┐│
│  │              Infrastructure Layer                       ││
│  │  - Entity Framework Core                               ││
│  │  - Database Context                                    ││
│  │  - External Services                                   ││
│  │  - File System, Email, PDF Generation                 ││
│  └─────────────────────────────────────────────────────────┘│
└─────────────────────────────────────────────────────────────┘
                               │ EF Core
┌─────────────────────────────────────────────────────────────┐
│                     Data Tier                               │
├─────────────────────────────────────────────────────────────┤
│               SQL Server 2019/2022                         │
│  - Relationale Datenbank                                   │
│  - Optimierte Indizes                                      │
│  - Stored Procedures (bei Bedarf)                          │
│  - Backup & Recovery                                       │
└─────────────────────────────────────────────────────────────┘
```

## 2. Domain-Driven Design

### Bounded Contexts
```
┌─────────────────────────────────────────────────────────────┐
│                     KGV Domain                              │
├─────────────────────────────────────────────────────────────┤
│  Application Management  │  User Management  │  Document    │
│  - Antrag               │  - Benutzer       │  Generation   │
│  - Ranking              │  - Rollen         │  - Templates  │  
│  - Status-Workflow      │  - Berechtigungen │  - PDF/Word   │
│                         │                   │               │
│  Location Management    │  Notification     │  Audit        │
│  - Gemarkungen         │  - E-Mail         │  - Change Log │
│  - Parzellen           │  - SMS            │  - Events     │
│  - Bezirke             │  - Push           │               │
└─────────────────────────────────────────────────────────────┘
```

### Core Domain Entities

#### Antrag (Application) - Aggregate Root
```csharp
public class Antrag : AggregateRoot<AntragId>
{
    public AntragId Id { get; private set; }
    public string Aktenzeichen { get; private set; } // Format: 32.2 [Nummer] [Jahr]
    public PersonenDaten Antragsteller { get; private set; }
    public DateTime Bewerbungsdatum { get; private set; }
    public DateTime Loeschdatum { get; private set; }
    public AntragStatus Status { get; private set; }
    public RangPosition Position { get; private set; }
    public string Vermerk { get; private set; }
    
    // Wunschgemarkungen als Value Object Collection
    public IReadOnlyCollection<WunschGemarkung> WunschGemarkungen { get; private set; }
    
    // Verlauf als Child-Entitäten
    public IReadOnlyCollection<VerlaufsEintrag> Verlauf { get; private set; }

    // Domain Methods
    public void StatusAendern(AntragStatus neuerStatus, string grund, BenutzerInfo bearbeiter)
    {
        if (!KannStatusGeaendertWerden(neuerStatus))
            throw new DomainException($\"Statuswechsel von {Status} zu {neuerStatus} nicht erlaubt\");
            
        var statusWechsel = new StatusWechsel(Id, Status, neuerStatus, grund, bearbeiter);
        AddDomainEvent(new AntragStatusGeaendertEvent(Id, Status, neuerStatus));
        
        Status = neuerStatus;
        Verlauf.Add(new VerlaufsEintrag(statusWechsel));
    }
    
    public void GueltikeitVerlaengern(DateTime neuesLoeschdatum, BenutzerInfo bearbeiter)
    {
        if (neuesLoeschdatum <= Loeschdatum)
            throw new DomainException(\"Neues Löschdatum muss in der Zukunft liegen\");
            
        var verlaengerung = new Verlaengerung(Loeschdatum, neuesLoeschdatum, bearbeiter);
        AddDomainEvent(new AntragVerlaengertEvent(Id, Loeschdatum, neuesLoeschdatum));
        
        Loeschdatum = neuesLoeschdatum;
        Verlauf.Add(new VerlaufsEintrag(verlaengerung));
    }
}

public class PersonenDaten : ValueObject
{
    public string Anrede { get; }
    public string Titel { get; }
    public string Vorname { get; }
    public string Nachname { get; }
    public DateTime Geburtsdatum { get; }
    public Adresse Adresse { get; }
    public KontaktDaten Kontakt { get; }
    public string Briefanrede { get; }
    
    // Konstruktor mit Validierung
    public PersonenDaten(string anrede, string titel, string vorname, 
                        string nachname, DateTime geburtsdatum, 
                        Adresse adresse, KontaktDaten kontakt, string briefanrede)
    {
        if (string.IsNullOrWhiteSpace(nachname))
            throw new ArgumentException(\"Nachname ist erforderlich\", nameof(nachname));
            
        // weitere Validierungen...
        
        Anrede = anrede;
        Titel = titel;
        Vorname = vorname;
        Nachname = nachname;
        Geburtsdatum = geburtsdatum;
        Adresse = adresse;
        Kontakt = kontakt;
        Briefanrede = briefanrede;
    }
}
```

### Repository Pattern
```csharp
public interface IAntragRepository : IRepository<Antrag, AntragId>
{
    Task<Antrag?> GetByAktenzeichenAsync(string aktenzeichen);
    Task<PagedResult<Antrag>> GetByGemarkungAsync(GemarkungId gemarkung, 
                                                  int page, int pageSize);
    Task<IEnumerable<Antrag>> GetWartelisteAsync(GemarkungId gemarkung);
    Task<int> GetNaechsteRangPositionAsync(GemarkungId gemarkung);
    Task<IEnumerable<Antrag>> GetAbgelaufeneAntraegeAsync(DateTime stichtag);
}
```

## 3. CQRS mit MediatR

### Command/Query Separation
```csharp
// Commands (Write Operations)
public record AntragErstellenCommand(
    PersonenDaten Antragsteller,
    List<GemarkungId> WunschGemarkungen,
    string? Vermerk) : IRequest<AntragId>;

public record AntragStatusAendernCommand(
    AntragId AntragId,
    AntragStatus NeuerStatus,
    string Grund) : IRequest;

// Queries (Read Operations)  
public record AntragslisteAbfragenQuery(
    GemarkungId? Gemarkung = null,
    AntragStatus? Status = null,
    DateTime? BewerbungVon = null,
    DateTime? BewerbungBis = null,
    int Seite = 1,
    int SeitenGroesse = 20) : IRequest<PagedResult<AntragListenDto>>;

public record AntragDetailsAbfragenQuery(AntragId AntragId) : IRequest<AntragDetailsDto>;
```

### Command Handlers mit Validation
```csharp
public class AntragErstellenCommandHandler : IRequestHandler<AntragErstellenCommand, AntragId>
{
    private readonly IAntragRepository _antragRepository;
    private readonly IGemarkungRepository _gemarkungRepository;
    private readonly IAktenzeichenGenerator _aktenzeichenGenerator;
    private readonly IUnitOfWork _unitOfWork;

    public async Task<AntragId> Handle(AntragErstellenCommand request, CancellationToken cancellationToken)
    {
        // Geschäftsvalidierung
        await ValidateWunschGemarkungen(request.WunschGemarkungen);
        
        // Aktenzeichen generieren
        var aktenzeichen = await _aktenzeichenGenerator.GeneriereNaechstesAsync();
        
        // Rang für erste Wunschgemarkung ermitteln
        var position = await ErmittleNaechstePosition(request.WunschGemarkungen.First());
        
        // Domain Entity erstellen
        var antrag = Antrag.Erstellen(
            aktenzeichen: aktenzeichen,
            antragsteller: request.Antragsteller,
            wunschGemarkungen: request.WunschGemarkungen,
            position: position,
            vermerk: request.Vermerk);
            
        // Speichern
        await _antragRepository.AddAsync(antrag);
        await _unitOfWork.SaveChangesAsync();
        
        return antrag.Id;
    }
}
```

## 4. API-Design

### RESTful API-Struktur
```
GET    /api/antraege                     # Paginierte Liste aller Anträge
POST   /api/antraege                     # Neuen Antrag erstellen  
GET    /api/antraege/{id}                # Antrag-Details abrufen
PUT    /api/antraege/{id}                # Antrag-Daten aktualisieren
DELETE /api/antraege/{id}                # Antrag löschen

GET    /api/antraege/{id}/verlauf        # Verlaufs-Historie
POST   /api/antraege/{id}/verlaengerung  # Gültigkeit verlängern
POST   /api/antraege/{id}/angebot        # Angebot erstellen

GET    /api/gemarkungen                  # Verfügbare Gemarkungen
GET    /api/gemarkungen/{id}/warteliste  # Warteliste einer Gemarkung

POST   /api/dokumente/eingangsbestaetigung  # PDF generieren
POST   /api/dokumente/angebot               # Angebots-PDF

GET    /api/benutzer                     # Benutzerverwaltung (Admin)
POST   /api/benutzer                     # Benutzer anlegen
```

### API Response-Modelle
```csharp
public class AntragListenDto
{
    public Guid Id { get; set; }
    public string Aktenzeichen { get; set; }
    public string AntragstellerName { get; set; }
    public DateTime Bewerbungsdatum { get; set; }
    public DateTime Loeschdatum { get; set; }
    public string Status { get; set; }
    public int RangPosition { get; set; }
    public string WunschGemarkungen { get; set; }
}

public class AntragDetailsDto
{
    public Guid Id { get; set; }
    public string Aktenzeichen { get; set; }
    public PersonenDatenDto Antragsteller { get; set; }
    public DateTime Bewerbungsdatum { get; set; }
    public DateTime Loeschdatum { get; set; }
    public string Status { get; set; }
    public int RangPosition { get; set; }
    public List<GemarkungDto> WunschGemarkungen { get; set; }
    public string? Vermerk { get; set; }
    public List<VerlaufsEintragDto> Verlauf { get; set; }
}
```

## 5. Entity Framework Configuration

### DbContext Setup
```csharp
public class KgvDbContext : DbContext
{
    public DbSet<Antrag> Antraege { get; set; }
    public DbSet<Gemarkung> Gemarkungen { get; set; }
    public DbSet<Benutzer> Benutzer { get; set; }
    public DbSet<VerlaufsEintrag> VerlaufsEintraege { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(KgvDbContext).Assembly);
        
        // Global Query Filters
        modelBuilder.Entity<Antrag>().HasQueryFilter(a => !a.IsDeleted);
    }
}

public class AntragEntityConfiguration : IEntityTypeConfiguration<Antrag>
{
    public void Configure(EntityTypeBuilder<Antrag> builder)
    {
        builder.ToTable(\"Antraege\");
        
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
               .HasConversion(id => id.Value, guid => new AntragId(guid));
               
        builder.Property(x => x.Aktenzeichen)
               .HasMaxLength(20)
               .IsRequired();
               
        builder.HasIndex(x => x.Aktenzeichen)
               .IsUnique();
               
        // Owned Types für Value Objects
        builder.OwnsOne(x => x.Antragsteller, nav =>
        {
            nav.Property(p => p.Nachname).HasMaxLength(100).IsRequired();
            nav.Property(p => p.Vorname).HasMaxLength(100);
            nav.OwnsOne(p => p.Adresse, addr =>
            {
                addr.Property(a => a.Strasse).HasMaxLength(200);
                addr.Property(a => a.PLZ).HasMaxLength(10);
                addr.Property(a => a.Ort).HasMaxLength(100);
            });
        });
        
        // Navigation Properties
        builder.HasMany(x => x.Verlauf)
               .WithOne()
               .HasForeignKey(\"AntragId\")
               .OnDelete(DeleteBehavior.Cascade);
    }
}
```

## 6. Authentication & Authorization

### JWT-basierte Authentifizierung
```csharp
public class JwtSettings
{
    public string SecretKey { get; set; } = default!;
    public string Issuer { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public int ExpirationMinutes { get; set; } = 60;
}

[ApiController]
[Route(\"api/[controller]\")]
public class AuthController : ControllerBase
{
    private readonly IAuthenticationService _authService;

    [HttpPost(\"login\")]
    public async Task<ActionResult<LoginResponse>> LoginAsync([FromBody] LoginRequest request)
    {
        var result = await _authService.AuthenticateAsync(request.Email, request.Password);
        
        if (!result.IsSuccess)
            return Unauthorized(result.ErrorMessage);
            
        return Ok(new LoginResponse
        {
            Token = result.Token,
            ExpiresAt = result.ExpiresAt,
            User = result.User
        });
    }
}
```

### Role-based Authorization
```csharp
[Authorize(Roles = \"Sachbearbeiter,Administrator\")]
[HttpPut(\"api/antraege/{id}\")]
public async Task<IActionResult> UpdateAntragAsync(Guid id, [FromBody] UpdateAntragRequest request)
{
    var command = new AntragAktualisierenCommand(new AntragId(id), request);
    await _mediator.Send(command);
    return NoContent();
}

[Authorize(Roles = \"Administrator\")]
[HttpPost(\"api/benutzer\")]
public async Task<IActionResult> CreateBenutzerAsync([FromBody] CreateBenutzerRequest request)
{
    // Nur für Administratoren
}
```

## 7. Frontend-Architektur (Angular)

### Feature-based Module Structure
```
src/app/
├── core/
│   ├── auth/
│   ├── guards/
│   ├── interceptors/
│   └── services/
├── shared/
│   ├── components/
│   ├── directives/
│   └── models/
├── features/
│   ├── antraege/
│   │   ├── components/
│   │   ├── services/
│   │   └── antraege.module.ts
│   ├── benutzerverwaltung/
│   └── dashboard/
└── layout/
    ├── header/
    ├── navigation/
    └── footer/
```

### State Management mit NgRx
```typescript
// State Definition
export interface AntragState {
  antraege: Antrag[];
  selectedAntrag: Antrag | null;
  loading: boolean;
  error: string | null;
  pagination: Pagination;
}

// Actions
export const loadAntraege = createAction(
  '[Antrag] Load Antraege',
  props<{ filter: AntragFilter }>()
);

export const loadAntraegeSuccess = createAction(
  '[Antrag] Load Antraege Success',
  props<{ antraege: Antrag[]; pagination: Pagination }>()
);

// Reducer
const antragReducer = createReducer(
  initialState,
  on(loadAntraege, state => ({ ...state, loading: true, error: null })),
  on(loadAntraegeSuccess, (state, { antraege, pagination }) => ({
    ...state,
    antraege,
    pagination,
    loading: false
  }))
);

// Effects
@Injectable()
export class AntragEffects {
  loadAntraege$ = createEffect(() =>
    this.actions$.pipe(
      ofType(loadAntraege),
      switchMap(({ filter }) =>
        this.antragService.getAntraege(filter).pipe(
          map(result => loadAntraegeSuccess({ 
            antraege: result.data, 
            pagination: result.pagination 
          })),
          catchError(error => of(loadAntraegeFailed({ error: error.message })))
        )
      )
    )
  );
}
```

## 8. Security Architecture

### API Security Headers
```csharp
public class SecurityHeadersMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        context.Response.Headers.Add(\"X-Frame-Options\", \"DENY\");
        context.Response.Headers.Add(\"X-Content-Type-Options\", \"nosniff\");
        context.Response.Headers.Add(\"Referrer-Policy\", \"strict-origin-when-cross-origin\");
        context.Response.Headers.Add(\"X-Permitted-Cross-Domain-Policies\", \"none\");
        
        await next(context);
    }
}
```

### Input Validation
```csharp
public class AntragErstellenCommandValidator : AbstractValidator<AntragErstellenCommand>
{
    public AntragErstellenCommandValidator()
    {
        RuleFor(x => x.Antragsteller.Nachname)
            .NotEmpty().WithMessage(\"Nachname ist erforderlich\")
            .MaximumLength(100).WithMessage(\"Nachname zu lang\");
            
        RuleFor(x => x.WunschGemarkungen)
            .NotEmpty().WithMessage(\"Mindestens eine Wunschgemarkung erforderlich\")
            .Must(list => list.Count <= 3).WithMessage(\"Maximal 3 Wunschgemarkungen\");
    }
}
```

## 9. Performance & Scalability

### Caching Strategy
```csharp
public class CachedGemarkungRepository : IGemarkungRepository
{
    private readonly IGemarkungRepository _repository;
    private readonly IMemoryCache _cache;
    
    public async Task<IEnumerable<Gemarkung>> GetAllAsync()
    {
        return await _cache.GetOrCreateAsync(\"gemarkungen\", async entry =>
        {
            entry.SetAbsoluteExpiration(TimeSpan.FromHours(24));
            return await _repository.GetAllAsync();
        });
    }
}
```

### Database Optimization
```sql
-- Optimierte Indizes für häufige Queries
CREATE NONCLUSTERED INDEX IX_Antraege_Status_Bewerbungsdatum 
ON Antraege (Status, Bewerbungsdatum);

CREATE NONCLUSTERED INDEX IX_Antraege_Gemarkung_Position
ON Antraege (WunschGemarkung1, RangPosition);

-- Covering Index für Listen-Abfragen
CREATE NONCLUSTERED INDEX IX_Antraege_Liste_Covering
ON Antraege (Status)
INCLUDE (Aktenzeichen, AntragstellerName, Bewerbungsdatum, RangPosition);
```

## 10. Monitoring & Observability

### Application Insights Integration
```csharp
public class TelemetryService
{
    private readonly TelemetryClient _telemetryClient;
    
    public void TrackAntragCreated(AntragId antragId, TimeSpan processingTime)
    {
        _telemetryClient.TrackEvent(\"AntragCreated\", new Dictionary<string, string>
        {
            [\"AntragId\"] = antragId.ToString(),
            [\"ProcessingTimeMs\"] = processingTime.TotalMilliseconds.ToString()
        });
    }
}
```

### Health Checks
```csharp
public class DatabaseHealthCheck : IHealthCheck
{
    private readonly KgvDbContext _dbContext;
    
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, 
                                                         CancellationToken cancellationToken = default)
    {
        try
        {
            await _dbContext.Database.ExecuteSqlRawAsync(\"SELECT 1\", cancellationToken);
            return HealthCheckResult.Healthy(\"Database connection is healthy\");
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(\"Database connection failed\", ex);
        }
    }
}
```

---

Diese Architektur bietet eine solide, skalierbare und wartbare Grundlage für das moderne KGV-System mit klarer Trennung der Verantwortlichkeiten und etablierten Patterns für Enterprise-Anwendungen.