# BITV 2.0 Accessibility Audit Checklist für KGV-System

## Executive Summary

Dieses Dokument definiert die Barrierefreiheits-Anforderungen nach BITV 2.0 (Barrierefreie-Informationstechnik-Verordnung) für das KGV-System. Als behördliche Software muss das System vollständig den Konformitätslevel AA der WCAG 2.1 erfüllen.

## BITV 2.0 Grundlagen

### Rechtliche Basis
- **BITV 2.0**: Verordnung zur Schaffung barrierefreier Informationstechnik
- **BGG**: Behindertengleichstellungsgesetz
- **EU-Richtlinie 2016/2102**: Web-Accessibility-Richtlinie
- **Zielgruppe**: Alle öffentlichen Stellen des Bundes

### Konformitätslevel
- **Level A**: Grundlegende Barrierefreiheit (Minimum)
- **Level AA**: Erweiterte Barrierefreiheit (BITV 2.0 Standard)
- **Level AAA**: Höchste Barrierefreiheit (empfohlen für kritische Bereiche)

## 1. Wahrnehmbarkeit (Perceivable)

### 1.1 Textalternativen

#### 1.1.1 Nicht-Text-Inhalte (Level A)
- [ ] **Alt-Text für alle Bilder**
  ```html
  <!-- Gut -->
  <img src="garden.jpg" alt="Kleingarten mit Gemüsebeet und kleiner Hütte">
  
  <!-- Schlecht -->
  <img src="garden.jpg" alt="Bild1">
  ```

- [ ] **Aussagekräftige Alt-Texte für Icons**
  ```html
  <!-- Status-Icon -->
  <img src="pending-icon.svg" alt="Status: In Bearbeitung">
  
  <!-- Funktions-Icon -->
  <button>
    <img src="edit-icon.svg" alt="Antrag bearbeiten">
  </button>
  ```

- [ ] **Leere Alt-Attribute für dekorative Bilder**
  ```html
  <img src="decoration.png" alt="" role="presentation">
  ```

**Test-Kriterien:**
- Screen Reader liest alle relevanten Bildinformationen vor
- Dekorative Bilder werden ignoriert
- Icons vermitteln ihre Funktion auch ohne visuellen Kontext

### 1.2 Zeitbasierte Medien

#### 1.2.1 Reine Audio- und Videoinhalte (Level A)
- [ ] **Transkripte für Audio-Inhalte**
- [ ] **Audio-Deskription oder Transkript für Videos**
- [ ] **Untertitel für Videos mit gesprochenem Inhalt**

#### 1.2.2 Untertitel (Live) (Level AA)
- [ ] **Live-Untertitel für Streaming-Inhalte**
- [ ] **Audio-Deskription für aufgezeichnete Videos**

### 1.3 Anpassbarkeit

#### 1.3.1 Informationen und Beziehungen (Level A)
- [ ] **Semantisches HTML verwenden**
  ```html
  <!-- Korrekte Überschriften-Hierarchie -->
  <h1>Kleingartenantrag</h1>
    <h2>Persönliche Daten</h2>
      <h3>Kontaktinformationen</h3>
      
  <!-- Korrekte Formular-Labels -->
  <label for="vorname">Vorname *</label>
  <input id="vorname" type="text" required>
  
  <!-- Listen für strukturierte Inhalte -->
  <ul>
    <li>Bezirk A: 23 Plätze verfügbar</li>
    <li>Bezirk B: 12 Plätze verfügbar</li>
  </ul>
  ```

- [ ] **ARIA-Labels für komplexe Strukturen**
  ```html
  <div role="tabpanel" aria-labelledby="tab1" aria-describedby="tabdesc1">
    <p id="tabdesc1">Übersicht über Ihre Anträge</p>
  </div>
  ```

#### 1.3.2 Sinnvolle Reihenfolge (Level A)
- [ ] **Logische Tab-Reihenfolge**
- [ ] **DOM-Reihenfolge entspricht visueller Reihenfolge**
- [ ] **CSS-basierte Layoutänderungen beachten**

#### 1.3.3 Sensorische Eigenschaften (Level A)
- [ ] **Anweisungen nicht nur über Position/Form**
  ```html
  <!-- Gut -->
  <p>Klicken Sie auf den "Weiter"-Button am Ende der Seite</p>
  
  <!-- Schlecht -->
  <p>Klicken Sie auf den grünen Button rechts</p>
  ```

### 1.4 Unterscheidbarkeit

#### 1.4.1 Farbe (Level A)
- [ ] **Information nicht nur über Farbe vermitteln**
  ```css
  /* Fehlerfeld mit Icon UND Farbe */
  .error {
    border: 2px solid #d32f2f;
    background: url('error-icon.svg') no-repeat;
  }
  
  .error::before {
    content: "❌ ";
  }
  ```

#### 1.4.2 Audiokontrolle (Level A)
- [ ] **Auto-Play Audio vermeiden oder kontrollierbar machen**

#### 1.4.3 Kontrast (Minimum) (Level AA)
- [ ] **Kontrastverhältnis 4.5:1 für normalen Text**
- [ ] **Kontrastverhältnis 3:1 für großen Text (18pt+ oder 14pt+ fett)**

**Farbpalette für KGV-System:**
```scss
// Primärfarben (4.5:1 Kontrast auf Weiß)
$primary-blue: #1976d2;     // Kontrast: 5.14:1
$success-green: #2e7d32;    // Kontrast: 6.25:1  
$error-red: #d32f2f;       // Kontrast: 5.44:1
$warning-orange: #f57c00;   // Kontrast: 4.52:1

// Sekundärfarben
$text-primary: #212121;     // Kontrast: 16.26:1
$text-secondary: #757575;   // Kontrast: 4.54:1

// Hintergrundfarben
$background-light: #fafafa;
$background-paper: #ffffff;
```

#### 1.4.4 Text skalieren (Level AA)
- [ ] **200% Zoom ohne horizontalen Scroll**
- [ ] **Text bleibt lesbar bei Vergrößerung**
- [ ] **Keine Informationen gehen verloren**

**Test mit verschiedenen Zoom-Levels:**
```css
/* Responsive Design für Zoom */
@media (min-resolution: 2dppx) {
  .form-field {
    min-height: 48px; /* Touch-Target-Size */
    font-size: 16px;   /* Verhindert Zoom auf iOS */
  }
}
```

#### 1.4.5 Bilder von Text (Level AA)
- [ ] **Text als HTML, nicht als Bild**
- [ ] **Ausnahme: Logos und wesentliche grafische Darstellungen**

## 2. Bedienbarkeit (Operable)

### 2.1 Über Tastatur zugänglich

#### 2.1.1 Tastatur (Level A)
- [ ] **Alle Funktionen per Tastatur erreichbar**
- [ ] **Tab-Navigation funktional**
- [ ] **Enter/Space für Buttons/Links**

**Tastatur-Shortcuts für KGV-System:**
```javascript
// Wichtige Shortcuts implementieren
const shortcuts = {
  'Alt+S': 'Speichern',
  'Alt+N': 'Neuer Antrag', 
  'Ctrl+F': 'Suchen',
  'Esc': 'Modal schließen',
  'F1': 'Hilfe öffnen'
};
```

#### 2.1.2 Keine Tastaturfalle (Level A)
- [ ] **Fokus kann alle Bereiche verlassen**
- [ ] **Modale Dialoge korrekt fokussiert**
- [ ] **Dropdown-Menüs mit Escape schließbar**

```javascript
// Focus-Trap für Modals
class ModalFocusTrap {
  constructor(modalElement) {
    this.modal = modalElement;
    this.focusableElements = this.modal.querySelectorAll(
      'button, [href], input, select, textarea, [tabindex]:not([tabindex="-1"])'
    );
    this.firstFocusable = this.focusableElements[0];
    this.lastFocusable = this.focusableElements[this.focusableElements.length - 1];
  }
  
  trapFocus(e) {
    if (e.key === 'Tab') {
      if (e.shiftKey && document.activeElement === this.firstFocusable) {
        this.lastFocusable.focus();
        e.preventDefault();
      } else if (!e.shiftKey && document.activeElement === this.lastFocusable) {
        this.firstFocusable.focus(); 
        e.preventDefault();
      }
    }
  }
}
```

### 2.2 Ausreichend Zeit

#### 2.2.1 Zeitbegrenzung anpassbar (Level A)
- [ ] **Sessions sind verlängerbar**
- [ ] **Automatisches Speichern bei Formularen**
- [ ] **Warnung vor Session-Ablauf**

```typescript
// Session-Management mit Warnung
class SessionManager {
  private warningTimeout: number = 25 * 60 * 1000; // 25 Min
  private sessionTimeout: number = 30 * 60 * 1000;  // 30 Min
  
  showWarning() {
    const dialog = new ConfirmDialog({
      title: 'Sitzung läuft ab',
      message: 'Ihre Sitzung läuft in 5 Minuten ab. Möchten Sie verlängern?',
      confirmButton: 'Verlängern',
      cancelButton: 'Abmelden'
    });
    
    return dialog.show();
  }
}
```

#### 2.2.2 Pausieren, beenden, ausblenden (Level A)
- [ ] **Automatische Updates pausierbar**
- [ ] **Bewegte Inhalte kontrollierbar**

### 2.3 Anfälle und körperliche Reaktionen

#### 2.3.1 Grenzwert von dreimaligem Blitzen (Level A)
- [ ] **Keine blitzenden Elemente über 3Hz**
- [ ] **Alternative für blitzende Warnungen**

### 2.4 Navigierbar

#### 2.4.1 Bereiche überspringen (Level A)
- [ ] **Skip-Links implementiert**
  ```html
  <a href="#main-content" class="skip-link">Zum Hauptinhalt springen</a>
  <a href="#navigation" class="skip-link">Zur Navigation springen</a>
  
  <main id="main-content">
    <!-- Hauptinhalt -->
  </main>
  ```

#### 2.4.2 Seiten haben Titel (Level A)
- [ ] **Aussagekräftige Seitentitel**
  ```html
  <!-- Dynamische Titel je Kontext -->
  <title>Antrag #KGV-2025-00847 bearbeiten - Kleingartenverwaltung</title>
  <title>Neuen Antrag stellen - Kleingarten-Portal der Stadt</title>
  ```

#### 2.4.3 Fokus-Reihenfolge (Level A)  
- [ ] **Logische Tab-Reihenfolge**
- [ ] **Keine Tab-Index über 0 (außer -1)**

#### 2.4.4 Linkzweck im Kontext (Level A)
- [ ] **Aussagekräftige Link-Texte**
  ```html
  <!-- Gut -->
  <a href="/antrag/847/bearbeiten">Antrag #KGV-2025-00847 bearbeiten</a>
  
  <!-- Schlecht -->  
  <a href="/antrag/847/bearbeiten">Hier klicken</a>
  ```

#### 2.4.5 Verschiedene Wege (Level AA)
- [ ] **Multiple Navigation (Menü + Suche + Sitemap)**
- [ ] **Breadcrumb-Navigation**
  ```html
  <nav aria-label="Breadcrumb">
    <ol>
      <li><a href="/">Startseite</a></li>
      <li><a href="/antraege">Anträge</a></li>
      <li aria-current="page">Antrag bearbeiten</li>
    </ol>
  </nav>
  ```

#### 2.4.6 Überschriften und Labels (Level AA)
- [ ] **Beschreibende Überschriften**
- [ ] **Präzise Labels für Formularfelder**

#### 2.4.7 Fokus sichtbar (Level AA)
- [ ] **Sichtbare Fokus-Indikatoren**
  ```css
  /* Hochkontrast-Fokus-Indikator */
  .form-input:focus {
    outline: 3px solid #005fcc;
    outline-offset: 2px;
    box-shadow: 0 0 0 2px #ffffff;
  }
  
  .button:focus {
    outline: 3px solid #005fcc;
    outline-offset: 2px;
  }
  ```

## 3. Verständlichkeit (Understandable)

### 3.1 Lesbar

#### 3.1.1 Sprache der Seite (Level A)
- [ ] **Lang-Attribut gesetzt**
  ```html
  <html lang="de">
  <head>
    <title>Kleingarten-Portal</title>
  </head>
  ```

#### 3.1.2 Sprache von Teilen (Level AA)
- [ ] **Fremdsprachige Begriffe markiert**
  ```html
  <p>Der <span lang="en">City Garden Club</span> organisiert regelmäßige Treffen.</p>
  ```

### 3.2 Vorhersagbar

#### 3.2.1 Bei Fokus (Level A)
- [ ] **Fokus löst keine Kontextänderungen aus**
- [ ] **Keine automatischen Form-Submits**

#### 3.2.2 Bei Eingabe (Level A) 
- [ ] **Eingaben ändern nicht automatisch den Kontext**
- [ ] **Vorwarnung vor Kontextänderungen**

#### 3.2.3 Konsistente Navigation (Level AA)
- [ ] **Einheitliche Navigationsreihenfolge**
- [ ] **Konsistente UI-Elemente**

#### 3.2.4 Konsistente Identifizierung (Level AA)
- [ ] **Gleiche Funktionen haben gleiche Labels**
- [ ] **Konsistente Icon-Bedeutungen**

### 3.3 Eingabehilfe

#### 3.3.1 Fehlerkennung (Level A)
- [ ] **Fehler werden erkannt und beschrieben**
  ```html
  <div class="form-field error">
    <label for="email">E-Mail-Adresse *</label>
    <input id="email" type="email" required aria-describedby="email-error">
    <div id="email-error" role="alert">
      ❌ Bitte geben Sie eine gültige E-Mail-Adresse ein.
    </div>
  </div>
  ```

#### 3.3.2 Labels oder Anweisungen (Level A)
- [ ] **Klare Labels für alle Eingabefelder**
- [ ] **Erforderliche Felder markiert**
- [ ] **Format-Hinweise gegeben**
  ```html
  <label for="phone">Telefonnummer</label>
  <input id="phone" type="tel" aria-describedby="phone-format">
  <div id="phone-format">Format: +49 30 12345678</div>
  ```

#### 3.3.3 Fehlervorschlag (Level AA)
- [ ] **Korrekturvorschläge für Fehler**
- [ ] **Inline-Validierung mit Hilfestellung**

#### 3.3.4 Fehlervermeidung (Legal, Financial, Data) (Level AA)
- [ ] **Bestätigungsdialog vor kritischen Aktionen**
- [ ] **Überprüfungsseite vor Antrag-Übermittlung**
- [ ] **Rückgängig-Machen für Löschungen**

```html
<!-- Bestätigungsdialog -->
<div role="dialog" aria-labelledby="confirm-title" aria-describedby="confirm-desc">
  <h2 id="confirm-title">Antrag löschen bestätigen</h2>
  <p id="confirm-desc">
    Sind Sie sicher, dass Sie den Antrag #KGV-2025-00847 von Thomas Müller 
    unwiderruflich löschen möchten?
  </p>
  <button type="button">Abbrechen</button>
  <button type="button" class="danger">Endgültig löschen</button>
</div>
```

## 4. Robustheit (Robust)

### 4.1 Kompatibel

#### 4.1.1 Parsing (Level A)
- [ ] **Valides HTML**
- [ ] **Eindeutige IDs**
- [ ] **Korrekt verschachtelte Elemente**

#### 4.1.2 Name, Rolle, Wert (Level A)
- [ ] **ARIA-Rollen korrekt verwendet**
- [ ] **Programmatisch bestimmbare Namen**
- [ ] **Status-Updates erkennbar**

```html
<!-- Korrekte ARIA-Verwendung -->
<div role="alert" aria-live="polite" id="status-update">
  Ihr Antrag wurde erfolgreich aktualisiert.
</div>

<button aria-expanded="false" aria-controls="dropdown-menu">
  Bezirk auswählen
</button>
```

## Screen Reader Testing

### Empfohlene Screen Reader
- **NVDA** (Windows) - Kostenlos
- **JAWS** (Windows) - Kommerzielle Lösung  
- **VoiceOver** (macOS) - Eingebaut
- **TalkBack** (Android) - Eingebaut

### Test-Szenarien

#### Bürger-Portal Tests:
1. **Antrag ausfüllen** - Kompletter Workflow nur mit Tastatur
2. **Status prüfen** - Navigation zu Antragsstatus 
3. **Hilfe finden** - FAQ durchsuchen
4. **Kontakt aufnehmen** - Kontaktformular ausfüllen

#### Verwaltungs-Portal Tests:
1. **Dashboard nutzen** - Übersicht erfassen
2. **Antrag bearbeiten** - Status ändern, Vermerke hinzufügen
3. **Suche verwenden** - Anträge finden
4. **Berichte generieren** - Export-Funktionen nutzen

### Testing Checklist

```markdown
## Screen Reader Test Protokoll

### Getestet mit: [NVDA 2024.1 / Chrome 120]
### Datum: [TT.MM.YYYY]
### Tester: [Name]

#### Grundfunktionen
- [ ] Alle Inhalte werden vorgelesen
- [ ] Navigation mit Überschriften (H1-H6)
- [ ] Navigation mit Landmarks
- [ ] Formulare sind vollständig bedienbar
- [ ] Listen werden korrekt erkannt
- [ ] Tabellen haben Header-Zuordnungen

#### Interaktive Elemente
- [ ] Buttons haben aussagekräftige Namen
- [ ] Links beschreiben ihr Ziel
- [ ] Formulare haben Labels
- [ ] Fehlermeldungen werden vorgelesen
- [ ] Modal-Dialoge sind fokussiert

#### Status-Änderungen
- [ ] Live-Regions funktionieren
- [ ] ARIA-Updates werden mitgeteilt
- [ ] Lade-Zustände sind erkennbar
- [ ] Erfolgs-/Fehlermeldungen audible

#### Navigation
- [ ] Tab-Reihenfolge ist logisch
- [ ] Skip-Links funktionieren
- [ ] Breadcrumbs sind erkennbar
- [ ] Menüs sind bedienbar
```

## Automatisierte Tests

### Testing Tools Integration

```typescript
// Axe-Core Integration für automatisierte Tests
import { axe, toHaveNoViolations } from 'jest-axe';

expect.extend(toHaveNoViolations);

describe('Accessibility Tests', () => {
  test('Antragsliste has no accessibility violations', async () => {
    const { container } = render(<AntragslisteComponent />);
    const results = await axe(container);
    expect(results).toHaveNoViolations();
  });

  test('Form validation messages are accessible', async () => {
    const { container } = render(<AntragFormComponent />);
    
    // Trigger validation
    fireEvent.submit(container.querySelector('form'));
    
    const results = await axe(container);
    expect(results).toHaveNoViolations();
  });
});
```

### Lighthouse CI Integration

```yaml
# .github/workflows/accessibility.yml
name: Accessibility Tests

on: [push, pull_request]

jobs:
  a11y-tests:
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v2
      - name: Run Lighthouse CI
        uses: treosh/lighthouse-ci-action@v9
        with:
          configPath: './lighthouse-ci.json'
          
      - name: Run Pa11y
        run: |
          npm install -g pa11y-ci
          pa11y-ci --sitemap http://localhost:4200/sitemap.xml
```

```json
// lighthouse-ci.json
{
  "ci": {
    "collect": {
      "url": [
        "http://localhost:4200/",
        "http://localhost:4200/antrag/neu",  
        "http://localhost:4200/status",
        "http://localhost:4200/verwaltung"
      ],
      "settings": {
        "onlyCategories": ["accessibility"]
      }
    },
    "assert": {
      "assertions": {
        "categories:accessibility": ["error", {"minScore": 0.95}]
      }
    }
  }
}
```

## Dokumentation & Schulungen

### Accessibility Statement (BITV-Test)

```html
<!-- Barrierefreiheits-Erklärung -->
<section>
  <h1>Erklärung zur Barrierefreiheit</h1>
  
  <p>Die Stadt [Name] ist bemüht, ihre Website im Einklang mit der 
  Barrierefreie-Informationstechnik-Verordnung (BITV 2.0) barrierefrei 
  zugänglich zu machen.</p>
  
  <h2>Stand der Vereinbarkeit mit den Anforderungen</h2>
  <p>Diese Website ist mit der BITV 2.0 vollständig vereinbar.</p>
  
  <h2>Nicht barrierefreie Inhalte</h2>
  <p>Die nachstehend aufgeführten Inhalte sind aus den folgenden Gründen 
  nicht barrierefrei:</p>
  
  <h3>Unverhältnismäßige Belastung</h3>
  <ul>
    <li>Legacy PDF-Dokumente vor 2020 (werden nach und nach ersetzt)</li>
  </ul>
  
  <h2>Erstellung dieser Erklärung zur Barrierefreiheit</h2>
  <p>Diese Erklärung wurde am [Datum] erstellt und zuletzt am [Datum] 
  überprüft.</p>
  
  <h2>Feedback und Kontaktangaben</h2>
  <p>Teilen Sie uns mit, wenn Sie auf Inhalte stoßen, die nicht 
  barrierefrei sind:</p>
  <address>
    E-Mail: <a href="mailto:barrierefreiheit@stadt.de">barrierefreiheit@stadt.de</a><br>
    Telefon: <a href="tel:+4930123456789">+49 30 12345-6789</a>
  </address>
</section>
```

### Schulungsplan für Entwickler

#### Module 1: BITV 2.0 Grundlagen (4 Stunden)
- Rechtliche Anforderungen
- WCAG 2.1 Prinzipien  
- Screen Reader Simulation
- Häufige Fehlerquellen

#### Module 2: HTML Semantik (2 Stunden)
- Korrekte HTML-Struktur
- ARIA-Rollen und Properties
- Formulare barrierefrei gestalten

#### Module 3: CSS & Design (2 Stunden)
- Kontraste und Farben
- Responsive Design für Barrierefreiheit
- Fokus-Management

#### Module 4: JavaScript Interaktionen (3 Stunden)
- Tastatur-Navigation
- Screen Reader API
- Live Regions und Updates

#### Module 5: Testing & QA (2 Stunden)
- Automatisierte Tests
- Manuelle Testverfahren  
- Screen Reader Testing

### Support-Ressourcen

```typescript
// Accessibility Helper Service
@Injectable()
export class AccessibilityService {
  
  // Dynamische ARIA-Labels setzen
  setAriaLabel(element: HTMLElement, label: string) {
    element.setAttribute('aria-label', label);
  }
  
  // Live Region Updates
  announceToScreenReader(message: string, priority: 'polite' | 'assertive' = 'polite') {
    const announcement = document.createElement('div');
    announcement.setAttribute('aria-live', priority);
    announcement.setAttribute('aria-atomic', 'true');
    announcement.className = 'sr-only';
    announcement.textContent = message;
    
    document.body.appendChild(announcement);
    
    setTimeout(() => {
      document.body.removeChild(announcement);
    }, 1000);
  }
  
  // Fokus-Management für SPAs
  setPageFocus(heading: string) {
    const h1 = document.querySelector('h1');
    if (h1) {
      h1.setAttribute('tabindex', '-1');
      h1.focus();
    }
    
    this.announceToScreenReader(`Seite ${heading} geladen`);
  }
}
```

## Ongoing Monitoring

### Performance Monitoring
- Monatliche automatisierte Accessibility-Scans
- Quarterly Screen Reader Tests mit echten Nutzern
- Kontinuierliches Feedback-System
- Jährliche BITV-Zertifizierung

### KPIs für Barrierefreiheit
- **Lighthouse Accessibility Score**: > 95%
- **Axe-Core Violations**: 0 kritische/schwere Fehler
- **Manual Testing Success Rate**: > 90%
- **User Feedback Response Time**: < 48 Stunden

### Continuous Improvement
- Regelmäßige Updates der Accessibility Guidelines
- Integration neuer WCAG-Versionen
- Schulungen für neue Teammitglieder
- Community Feedback Integration

Diese umfassende BITV 2.0-Checkliste gewährleistet, dass das KGV-System allen rechtlichen Anforderungen für barrierefreie behördliche Software entspricht und ein inklusives Benutzererlebnis für alle Bürger bietet.