# KGV-System UX Design Guide

## Überblick

Dieser Design Guide definiert die Benutzererfahrung (UX) und User Interface (UI) Standards für das modernisierte KGV-System. Basierend auf umfangreichem User Research und Accessibility-Anforderungen für Behörden-Software.

## 1. Design-Prinzipien

### 1.1 Zugänglichkeit für alle (Accessibility First)
- **BITV 2.0 Konformität**: Vollständige Barrierefreiheit nach deutschem Standard
- **Screen Reader optimiert**: Semantisches HTML und ARIA-Labels
- **Tastatur-Navigation**: Alle Funktionen ohne Maus bedienbar
- **Hohe Kontraste**: WCAG 2.1 AAA Kontrast-Standards
- **Skalierbare UI**: Bis 200% Zoom ohne Funktionsverlust

### 1.2 Einfachheit und Klarheit
- **Minimalistisches Design**: Fokus auf wesentliche Funktionen
- **Klare Hierarchien**: Eindeutige Informationsarchitektur
- **Verständliche Sprache**: Verzicht auf Behörden-Jargon
- **Fehlertoleranz**: Hilfestellungen und Validierung in Echtzeit
- **Progressive Disclosure**: Komplexe Funktionen schrittweise enthüllen

### 1.3 Effizienz und Produktivität
- **Workflow-optimiert**: Unterstützung natürlicher Arbeitsabläufe
- **Minimale Klicks**: Häufige Aktionen in 1-2 Klicks erreichbar
- **Bulk-Operationen**: Massenaktionen für Sachbearbeiter
- **Smart Defaults**: Vorausgefüllte Felder basierend auf Kontext
- **Keyboard Shortcuts**: Power-User Features für erfahrene Nutzer

## 2. Zielgruppen-spezifisches Design

### 2.1 Bürger-Portal (Public Interface)

#### Persona: Thomas Müller (45, Garteninteressent)
**Kontext**: Möchte sich für Kleingarten bewerben, nutzt hauptsächlich Mobile
**Goals**: Schnelle, verständliche Antragstellung ohne Fehler
**Pain Points**: Komplizierte Formulare, unklare Anforderungen

#### Design-Ansatz:
- **Mobile-First**: Responsive Design mit Touch-Optimierung
- **Wizard-Flow**: Schritt-für-Schritt Antragstellung
- **Progress Indicator**: Klare Fortschrittsanzeige
- **Inline-Validierung**: Sofortiges Feedback bei Eingabefehlern
- **Plain Language**: Verständliche Formulierungen ohne Amtsdeutsch

### 2.2 Verwaltungsportal (Internal Interface)

#### Persona: Maria Schmidt (52, Sachbearbeiterin)
**Kontext**: Bearbeitet täglich 10-15 Anträge, kennt Legacy-System gut
**Goals**: Effiziente Bearbeitung ohne Produktivitätsverlust
**Pain Points**: Zeitaufwändige manuelle Prozesse, schlechte Übersicht

#### Design-Ansatz:
- **Dashboard-fokussiert**: Schneller Überblick über Arbeitslast
- **Batch-Operationen**: Mehrere Anträge gleichzeitig bearbeiten
- **Contextual Actions**: Relevante Aktionen je nach Antragsstatus
- **Quick-Edit**: Inline-Bearbeitung für schnelle Änderungen
- **Power-User Features**: Shortcuts und erweiterte Funktionen

## 3. Information Architecture

### 3.1 Bürger-Portal Struktur
```
KGV Bürger-Portal
├── Startseite
│   ├── Informationen zu Kleingärten
│   ├── Aktueller Stand Wartelisten
│   └── Login / Antrag stellen
├── Antrag stellen
│   ├── Schritt 1: Persönliche Daten
│   ├── Schritt 2: Wunschgemarkungen
│   ├── Schritt 3: Bestätigung & Absendung
│   └── Erfolgsmeldung
├── Mein Antrag (Login erforderlich)
│   ├── Antragsstatus & Timeline
│   ├── Rang in Warteliste
│   ├── Dokumente & Korrespondenz
│   └── Angebote & Rückmeldungen
└── Service & Hilfe
    ├── FAQ
    ├── Kontakt
    ├── Rechtliche Hinweise
    └── Datenschutz
```

### 3.2 Verwaltungsportal Struktur
```
KGV Verwaltungsportal
├── Dashboard
│   ├── Arbeitsbereich (Neue Anträge, Fällige Termine)
│   ├── Statistiken & KPIs
│   └── Notifications
├── Anträge
│   ├── Übersicht / Suche
│   ├── Antrag Details
│   ├── Bearbeitung
│   └── Verlaufshistorie
├── Wartelisten
│   ├── Nach Gemarkungen
│   ├── Ranking-Verwaltung
│   └── Prognosen
├── Vergabe
│   ├── Verfügbare Parzellen
│   ├── Angebotserstellung
│   └── Vertragswesen
├── Dokumente
│   ├── Templates verwalten
│   ├── Generierung
│   └── Archiv
└── Administration
    ├── Benutzerverwaltung
    ├── Systemkonfiguration
    └── Berichte
```

## 4. Interaction Design Patterns

### 4.1 Formulare und Eingaben

#### Multi-Step Wizard (Antragstellung)
```
[Progress Bar: ████████░░░░ 60%]

Schritt 2 von 4: Wunschgemarkungen

🏡 Welche Gemarkungen interessieren Sie?
   (Sie können bis zu 3 auswählen, Reihenfolge = Priorität)

┌─────────────────────────────────────┐
│ ☑️ Niederrad (Warteliste: 45 Pers.) │ 
│ ☐ Oberrad (Warteliste: 62 Pers.)    │
│ ☐ Sachsenhausen (Warteliste: 28 P.) │
│ ☐ Bornheim (Warteliste: 71 Pers.)   │
└─────────────────────────────────────┘

[🗺️ Auf Karte anzeigen] [❓ Was ist eine Gemarkung?]

[← Zurück]  [Weiter →]
```

#### Inline-Validierung (Echtzeit-Feedback)
```
📧 E-Mail-Adresse *
┌─────────────────────────────────────┐
│ max.mustermann@email                │ ⚠️
└─────────────────────────────────────┘
❌ Bitte geben Sie eine vollständige E-Mail-Adresse ein
```

#### Smart Auto-Complete (PLZ/Ort)
```
📍 PLZ / Ort *
┌─────────────────────────────────────┐
│ 60311                               │ ✅
└─────────────────────────────────────┘
🎯 Frankfurt am Main wird automatisch eingetragen
```

### 4.2 Navigation und Orientierung

#### Breadcrumb Navigation
```
🏠 Dashboard > Anträge > Details > Antrag #32.2 15 2024
```

#### Status-Timeline (Antragsverlauf)
```
📋 Ihr Antrag: 32.2 15 2024

✅ 15.01.2024 - Antrag eingegangen
✅ 16.01.2024 - Eingangsbestätigung versendet  
✅ 20.01.2024 - Antrag geprüft und freigegeben
🕐 25.01.2024 - Position 12 in Warteliste (Niederrad)
⏳ Voraussichtliche Wartezeit: 8-12 Monate
```

#### Quick Actions (Verwaltung)
```
Antrag: Hans Müller (#32.2 156 2024)
Status: Warten auf Angebot

[📄 Details] [✉️ E-Mail] [📋 Angebot erstellen] [🗓️ Verlängern] [⋯ Mehr]
```

### 4.3 Feedback und Notifications

#### Success Messages
```
✅ Erfolgreich gespeichert
   Ihr Antrag wurde erfolgreich übermittelt.
   Sie erhalten in Kürze eine Bestätigung per E-Mail.
   
   📋 Ihr Aktenzeichen: 32.2 287 2024
   [Status verfolgen]
```

#### Error Messages (Konstruktiv)
```
❌ Antrag konnte nicht gespeichert werden

🔧 Was können Sie tun:
   • Überprüfen Sie Ihre Internetverbindung
   • Laden Sie die Seite neu (F5)
   • Kontaktieren Sie uns: (069) 12345-67
   
   [🔄 Erneut versuchen]
```

#### Loading States
```
⏳ Antragsdaten werden geladen...
   Dies kann bis zu 10 Sekunden dauern.

████████████████████████░░ 80%
```

## 5. Visual Design System

### 5.1 Farbpalette

#### Primärfarben (Stadt Frankfurt Branding)
- **Primär-Blau**: #003366 (Navigation, Buttons)
- **Sekundär-Blau**: #0066CC (Links, Icons)
- **Akzent-Grün**: #228B22 (Erfolg, Positiv)

#### Funktionale Farben
- **Erfolg**: #28A745 (Grün)
- **Warnung**: #FFC107 (Gelb)
- **Fehler**: #DC3545 (Rot)
- **Information**: #17A2B8 (Cyan)

#### Neutrale Farben
- **Schwarz**: #212529 (Haupt-Text)
- **Dunkelgrau**: #495057 (Sekundär-Text)
- **Mittelgrau**: #6C757D (Platzhalter)
- **Hellgrau**: #E9ECEF (Rahmen, Trennlinien)
- **Weiß**: #FFFFFF (Hintergrund)

### 5.2 Typografie

#### Font Stack
```css
/* Primär - Systemschriften für bessere Performance */
font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 
             'Helvetica Neue', Arial, sans-serif;

/* Alternative - Google Fonts für branding-kritische Bereiche */
font-family: 'Inter', sans-serif;
```

#### Größen-System (rem-basiert)
- **Headline 1**: 2.5rem (40px) - Seitentitel
- **Headline 2**: 2rem (32px) - Hauptbereiche
- **Headline 3**: 1.5rem (24px) - Unterbereiche
- **Body Large**: 1.125rem (18px) - Wichtige Texte
- **Body**: 1rem (16px) - Standard-Text
- **Body Small**: 0.875rem (14px) - Meta-Informationen
- **Caption**: 0.75rem (12px) - Labels, Footnotes

### 5.3 Spacing System

#### 8px Grid System
```css
/* Spacing-Variablen */
--space-xs: 4px;   /* 0.25rem */
--space-sm: 8px;   /* 0.5rem */
--space-md: 16px;  /* 1rem */
--space-lg: 24px;  /* 1.5rem */
--space-xl: 32px;  /* 2rem */
--space-2xl: 48px; /* 3rem */
--space-3xl: 64px; /* 4rem */
```

#### Layout-Abstände
- **Container-Padding**: 16px (mobile), 24px (tablet), 32px (desktop)
- **Element-Spacing**: 8px zwischen verwandten Elementen
- **Section-Spacing**: 24px zwischen Bereichen
- **Page-Spacing**: 48px zwischen Haupt-Sektionen

### 5.4 Komponenten-Bibliothek

#### Buttons
```html
<!-- Primär-Button -->
<button class="btn btn-primary">
  Antrag absenden
</button>

<!-- Sekundär-Button -->
<button class="btn btn-secondary">
  Als Entwurf speichern
</button>

<!-- Link-Button -->
<button class="btn btn-link">
  Abbrechen
</button>

<!-- Icon-Button -->
<button class="btn btn-icon">
  <icon name="download" /> PDF herunterladen
</button>
```

#### Form Elements
```html
<!-- Text Input -->
<div class="form-group">
  <label for="nachname" class="form-label required">
    Nachname
  </label>
  <input 
    type="text" 
    id="nachname" 
    class="form-input" 
    required 
    aria-describedby="nachname-help"
  />
  <div id="nachname-help" class="form-help">
    Ihr vollständiger Nachname wie im Ausweis
  </div>
</div>

<!-- Select Dropdown -->
<div class="form-group">
  <label for="gemarkung" class="form-label">
    Wunschgemarkung
  </label>
  <select id="gemarkung" class="form-select">
    <option value="">Bitte wählen...</option>
    <option value="niederrad">Niederrad</option>
    <option value="oberrad">Oberrad</option>
  </select>
</div>
```

#### Cards und Panels
```html
<!-- Antrag-Card -->
<div class="card">
  <div class="card-header">
    <h3 class="card-title">Antrag #32.2 156 2024</h3>
    <span class="badge badge-warning">Wartend</span>
  </div>
  <div class="card-body">
    <p class="card-text">Hans Müller, Niederrad</p>
    <p class="card-meta">Eingegangen: 15.01.2024</p>
  </div>
  <div class="card-actions">
    <button class="btn btn-primary btn-sm">Bearbeiten</button>
    <button class="btn btn-secondary btn-sm">Details</button>
  </div>
</div>
```

## 6. Responsive Design

### 6.1 Breakpoint-System
```css
/* Mobile First Approach */
:root {
  --bp-xs: 0px;      /* Extra small devices */
  --bp-sm: 576px;    /* Small devices (phones) */
  --bp-md: 768px;    /* Medium devices (tablets) */
  --bp-lg: 992px;    /* Large devices (desktops) */
  --bp-xl: 1200px;   /* Extra large devices */
  --bp-xxl: 1400px;  /* Extra extra large */
}
```

### 6.2 Layout-Adaptionen

#### Mobile (< 768px)
- **Navigation**: Hamburger Menu mit Overlay
- **Forms**: Single Column Layout
- **Tables**: Horizontal Scroll oder Card-Layout
- **Actions**: Bottom Sheet für Context-Menüs
- **Touch Targets**: Minimum 44x44px

#### Tablet (768px - 1024px)
- **Navigation**: Tab Bar oder Sidebar
- **Forms**: Two Column für verwandte Felder
- **Tables**: Responsive mit horizontalem Scroll
- **Sidebars**: Collapsible Off-Canvas

#### Desktop (> 1024px)
- **Navigation**: Persistent Sidebar
- **Forms**: Multi-Column Layouts möglich
- **Tables**: Full-Featured mit Sorting/Filtering
- **Shortcuts**: Keyboard Navigation unterstützt

## 7. Accessibility Implementation

### 7.1 Semantic HTML Structure
```html
<main role="main">
  <header>
    <h1>Antrag für Kleingartenfläche</h1>
    <nav aria-label="Fortschritt">
      <ol class="breadcrumb">
        <li><a href="#step1">Persönliche Daten</a></li>
        <li aria-current="page">Wunschgemarkungen</li>
        <li>Bestätigung</li>
      </ol>
    </nav>
  </header>
  
  <form aria-labelledby="form-title">
    <fieldset>
      <legend>Gewünschte Gemarkungen</legend>
      <!-- Form content -->
    </fieldset>
  </form>
</main>
```

### 7.2 Screen Reader Optimizations
```html
<!-- Aussagekräftige Labels -->
<label for="email">
  E-Mail-Adresse für Benachrichtigungen
</label>

<!-- ARIA-Beschreibungen -->
<input 
  type="email" 
  id="email"
  aria-describedby="email-help"
  aria-required="true"
/>
<div id="email-help">
  Wir senden Ihnen Statusupdates zu Ihrem Antrag
</div>

<!-- Live Regions für dynamische Updates -->
<div aria-live="polite" id="form-status">
  <!-- Erfolgs-/Fehlermeldungen -->
</div>

<!-- Skip Links -->
<a href="#main-content" class="skip-link">
  Zum Hauptinhalt springen
</a>
```

### 7.3 Keyboard Navigation
```css
/* Fokus-Indikatoren */
:focus {
  outline: 2px solid #0066CC;
  outline-offset: 2px;
}

/* Custom Focus für Komponenten */
.btn:focus {
  box-shadow: 0 0 0 3px rgba(0, 102, 204, 0.3);
}

/* Focus-Visible für moderne Browser */
.btn:focus-visible {
  outline: 2px solid #0066CC;
}
```

## 8. Performance UX

### 8.1 Loading States
```javascript
// Perceived Performance durch Progressive Loading
const LoadingState = {
  skeleton: 'Skeleton Screens für Layout-Stabilität',
  progressive: 'Wichtige Inhalte zuerst laden',
  feedback: 'Explicit Loading Indicators',
  chunked: 'Große Listen in Chunks laden'
};
```

### 8.2 Optimistic UI
```javascript
// Sofortiges Feedback ohne Server-Roundtrip
function submitForm(data) {
  // UI sofort aktualisieren
  showSuccessMessage('Daten werden gespeichert...');
  enableButton(false);
  
  // Server-Request im Hintergrund
  api.save(data)
    .then(() => updateMessage('✅ Erfolgreich gespeichert'))
    .catch(() => showError('Fehler beim Speichern'));
}
```

## 9. Testing & Validation

### 9.1 Accessibility Testing
- **Automatisiert**: axe-core, Lighthouse CI
- **Manuell**: Screen Reader Testing (NVDA, JAWS)
- **Keyboard**: Tab-Navigation ohne Maus
- **Kontrast**: Colour Contrast Analyser
- **Zoom**: 200% Zoom-Test

### 9.2 Usability Testing Plan
1. **Moderated Testing**: 5 Nutzer pro Persona
2. **A/B Testing**: Kritische Workflows optimieren  
3. **Analytics**: Heatmaps und Conversion-Tracking
4. **Feedback Loops**: Kontinuierliche Verbesserung
5. **Performance Monitoring**: Real User Metrics

### 9.3 Browser Compatibility
- **Primary Support**: Chrome 90+, Firefox 88+, Safari 14+, Edge 90+
- **Secondary Support**: Chrome 80+, Firefox 78+, Safari 13+
- **Graceful Degradation**: Basis-Funktionalität in älteren Browsern
- **Progressive Enhancement**: Modern Features als Bonus

---

Dieser UX Design Guide stellt sicher, dass das KGV-System nicht nur funktional, sondern auch benutzerfreundlich, zugänglich und effizient ist. Die definierten Standards schaffen eine konsistente Erfahrung für alle Nutzergruppen und erfüllen die hohen Anforderungen an Behörden-Software.