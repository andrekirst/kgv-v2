# Wireframe-Konzepte für KGV-System

## Bürger-Portal Wireframes

### 1. Landing Page (Responsive)

```
Desktop Layout (1200px+):
┌─────────────────────────────────────────────────────────────────┐
│  🏛️ Stadt Logo    KLEINGARTEN-PORTAL      🔍 Suche  👤 Anmelden │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│    🌱 Ihr Weg zum eigenen Kleingarten                          │
│    ════════════════════════════════                            │
│                                                                 │
│  ┌─── Hero Section ────────────────────┐  ┌─ Quick Actions ─┐   │
│  │                                     │  │                 │   │
│  │  [Großes Gartenbild]                │  │ 📝 Antrag       │   │
│  │                                     │  │    stellen      │   │
│  │  Online beantragen ─ schnell,      │  │                 │   │
│  │  transparent, jederzeit             │  │ 📊 Status       │   │
│  │                                     │  │    prüfen       │   │
│  │     [JETZT BEANTRAGEN] 🡪            │  │                 │   │
│  └─────────────────────────────────────┘  │ ❓ Häufige      │   │
│                                           │    Fragen       │   │
│  ┌─── Vorteile ──────────┐ ┌─ Statistik ┐ │                 │   │
│  │ ✅ 24/7 online        │ │ 1.247       │ │ 📞 Kontakt     │   │
│  │ ✅ Status-Tracking    │ │ Anträge     │ └─────────────────┘   │
│  │ ✅ Mobile optimiert   │ │ in 2024     │                       │
│  │ ✅ Sofort-Bestätigung │ │             │                       │
│  └───────────────────────┘ └─────────────┘                       │
└─────────────────────────────────────────────────────────────────┘

Mobile Layout (< 768px):
┌─────────────────────┐
│ 🏛️ Kleingartenportal │
├─────────────────────┤
│                     │
│  [Hero Image]       │
│                     │
│  Ihr Kleingarten    │
│  Online beantragen  │
│                     │
│  [ANTRAG STELLEN]   │ 
│                     │
├─────────────────────┤
│ 📊 Mein Status      │
├─────────────────────┤
│ ❓ Hilfe & FAQ      │
├─────────────────────┤
│ 📞 Kontakt          │
└─────────────────────┘
```

### 2. Antragsformular (Multi-Step)

```
Step 1/3: Persönliche Daten
┌─────────────────────────────────────────────────────────────────┐
│ ← Zurück    🌱 Kleingartenantrag         Schritt 1 von 3   [●○○] │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  Persönliche Angaben                                            │
│  ═══════════════════                                            │
│                                                                 │
│  Anrede *        [Herr ▼]              Titel      [       ]    │
│                                                                 │
│  Vorname *       [Thomas        ]       Nachname * [Müller   ] │
│                                                                 │
│  Straße *        [Musterstraße 123                          ]  │
│                                                                 │
│  PLZ *    [12345]    Ort * [Berlin           ]                 │
│                      └─ automatisch ausgefüllt                  │
│                                                                 │
│  Telefon         [+49 30 1234567                            ]  │
│                                                                 │
│  E-Mail *        [thomas.mueller@example.com                ]  │
│                  ✅ Gültige E-Mail-Adresse                     │
│                                                                 │
│  Geburtsdatum *  [📅 15.03.1972]                               │
│                                                                 │
│  ┌─ Weitere Person hinzufügen? ─────────────────────────────┐   │
│  │ ☐ Partner/Ehepartner als Mitantragsteller              │   │
│  └─────────────────────────────────────────────────────────┘   │
│                                                                 │
│                                   [SPEICHERN & WEITER] 🡪       │
└─────────────────────────────────────────────────────────────────┘

Step 2/3: Gartenwünsche
┌─────────────────────────────────────────────────────────────────┐
│ ← Zurück    🌱 Kleingartenantrag         Schritt 2 von 3   [●●○] │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  Ihre Gartenwünsche                                             │
│  ══════════════════                                             │
│                                                                 │
│  Bevorzugte Bezirke (max. 3 auswählen) *                       │
│                                                                 │
│  ┌─ Interaktive Karte ──────────────────────────────────────┐   │
│  │                                                          │   │
│  │     [Bezirk A]    [Bezirk B]     [Bezirk C]             │   │
│  │  ✅ 25 Gärten    ○ 12 Gärten    ○ 8 Gärten             │   │
│  │   ⏱ ~18 Mon.      ⏱ ~24 Mon.     ⏱ ~36 Mon.            │   │
│  │                                                          │   │
│  │         [Bezirk D]         [Bezirk E]                   │   │
│  │      ○ 5 Gärten        ○ 15 Gärten                     │   │
│  │       ⏱ ~48 Mon.        ⏱ ~12 Mon.                      │   │
│  └──────────────────────────────────────────────────────────┘   │
│                                                                 │
│  Bevorzugte Gartengröße                                         │
│  ○ Kleiner Garten (200-300 m²)     ○ Mittlerer Garten (300-400m²) │
│  ○ Großer Garten (400+ m²)         ☑ Größe ist flexibel        │
│                                                                 │
│  Besondere Wünsche (optional)                                  │
│  [Suche einen ruhigen Garten für die Familie. Nähe zu        ] │
│  [Spielplatz wäre optimal. Habe Erfahrung im Gartenbau.      ] │
│                                                                 │
│                                   [SPEICHERN & WEITER] 🡪       │
└─────────────────────────────────────────────────────────────────┘

Step 3/3: Bestätigung
┌─────────────────────────────────────────────────────────────────┐
│ ← Zurück    🌱 Kleingartenantrag         Schritt 3 von 3   [●●●] │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  Antrag überprüfen und abschicken                               │
│  ═════════════════════════════                                  │
│                                                                 │
│  ┌─ Ihre Angaben ─────────────────────────────────────────────┐ │
│  │ Thomas Müller                              [✏ Bearbeiten] │ │
│  │ Musterstraße 123, 12345 Berlin                           │ │
│  │ thomas.mueller@example.com, +49 30 1234567               │ │
│  │ Geboren: 15.03.1972                                      │ │
│  └───────────────────────────────────────────────────────────┘ │
│                                                                 │
│  ┌─ Ihre Wünsche ────────────────────────────────────────────┐ │
│  │ Bezirke: Bezirk A, Bezirk E                [✏ Bearbeiten] │ │
│  │ Gartengröße: Flexibel                                    │ │
│  │ Besondere Wünsche: Suche einen ruhigen Garten...        │ │
│  └───────────────────────────────────────────────────────────┘ │
│                                                                 │
│  ┌─ Rechtliche Hinweise ────────────────────────────────────┐   │
│  │ ☑ Ich bestätige die Richtigkeit meiner Angaben         │   │
│  │ ☑ Ich habe die Datenschutzerklärung gelesen             │   │
│  │ ☑ Ich stimme der elektronischen Bearbeitung zu          │   │
│  └───────────────────────────────────────────────────────────┘   │
│                                                                 │
│                         [ANTRAG ABSCHICKEN] ✅                  │
└─────────────────────────────────────────────────────────────────┘
```

### 3. Status-Dashboard (Bürger)

```
Mein Antrag - Status Dashboard
┌─────────────────────────────────────────────────────────────────┐
│ 🏛️ Kleingartenportal          🔔 2 neue Updates    👤 T. Müller │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  Antrag #KGV-2025-00847                                        │
│  ═══════════════════════                                        │
│                                                                 │
│  ┌─ Status-Timeline ──────────────────────────────────────────┐ │
│  │                                                            │ │
│  │ ✅ Antrag eingegangen         📅 15.01.2025                │ │
│  │ ✅ Eingangsbestätigung       📅 16.01.2025                │ │
│  │ ✅ Vollständigkeitsprüfung   📅 18.01.2025                │ │
│  │ 🔄 Warteliste eingeordnet    📅 20.01.2025                │ │
│  │ ⏳ Warten auf Verfügbarkeit  📅 Geschätzt: Q3 2025        │ │
│  │ ⚪ Angebot unterbreiten                                    │ │
│  │ ⚪ Vertragsabschluss                                       │ │
│  └────────────────────────────────────────────────────────────┘ │
│                                                                 │
│  ┌─ Aktuelle Position ────────┐  ┌─ Ihre Präferenzen ────────┐ │
│  │                            │  │                           │ │
│  │    Position auf             │  │ 📍 Bezirk A, E           │ │
│  │    Warteliste              │  │ 📏 Größe flexibel        │ │
│  │                            │  │ ⏱️ Wartezeit ca. 15 Mon. │ │
│  │      #23                   │  │                           │ │
│  │   (von 45)                 │  │ [PRÄFERENZEN ÄNDERN]     │ │
│  │                            │  │                           │ │
│  └────────────────────────────┘  └───────────────────────────┘ │
│                                                                 │
│  ┌─ Letzte Aktivitäten ──────────────────────────────────────┐ │
│  │ 📧 20.01.2025: Wartelisten-Position mitgeteilt           │ │
│  │ 📧 18.01.2025: Antrag vollständig - Bearbeitung beginnt  │ │
│  │ 📧 16.01.2025: Eingangsbestätigung per E-Mail           │ │
│  │                                              [Alle anzeigen] │ │
│  └────────────────────────────────────────────────────────────┘ │
│                                                                 │
│  ┌─ Verfügbare Aktionen ─────────────────────────────────────┐ │
│  │ 📝 Daten aktualisieren     📞 Sachbearbeiter kontaktieren│ │
│  │ 📄 Dokumente hochladen     ❌ Antrag zurückziehen         │ │
│  └────────────────────────────────────────────────────────────┘ │
└─────────────────────────────────────────────────────────────────┘
```

## Verwaltungs-Portal Wireframes

### 1. Dashboard (Sachbearbeiter)

```
KGV-Verwaltungssystem - Dashboard
┌─────────────────────────────────────────────────────────────────┐
│ 🏛️ Kleingartenverwaltung        🔔 8 neue    ⚙️ Einstellungen  │
│                                                   👤 M. Schmidt │
├─────────────────────────────────────────────────────────────────┤
│ 📊 Dashboard    📋 Anträge    👥 Wartelisten    📊 Berichte     │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  ┌─ Übersicht heute ──────────┐ ┌─ Dringende Aufgaben ────────┐ │
│  │                            │ │                             │ │
│  │ 📥 Neue Anträge:       7   │ │ 🔴 Rückfragen offen:    3   │ │
│  │ ⏳ In Bearbeitung:    23   │ │ ⚠️ Fristen ablaufend:   2   │ │
│  │ ✅ Heute abgeschlossen: 5  │ │ 📞 Rückrufe geplant:    4   │ │
│  │ 📞 Bürgerkontakte:    12   │ │                             │ │
│  │                            │ │ [ALLE AUFGABEN]             │ │
│  └────────────────────────────┘ └─────────────────────────────┘ │
│                                                                 │
│  ┌─ Wartelisten-Status ──────────────────────────────────────┐ │
│  │                                                            │ │
│  │ Bezirk A: ████████████░░░ 247 Anträge (↑12 diese Woche)  │ │
│  │ Bezirk B: ██████░░░░░░░░░  89 Anträge (↓3 diese Woche)   │ │
│  │ Bezirk C: ████████░░░░░░░ 156 Anträge (→0 diese Woche)   │ │
│  │ Bezirk D: ███░░░░░░░░░░░░   45 Anträge (↑7 diese Woche)  │ │
│  │ Bezirk E: ██████████░░░░  189 Anträge (↑5 diese Woche)   │ │
│  │                                                            │ │
│  └────────────────────────────────────────────────────────────┘ │
│                                                                 │
│  ┌─ Kürzlich bearbeitet ─────────────────────────────────────┐ │
│  │ #KGV-2025-00847  T. Müller      Warteliste    [ÖFFNEN]   │ │
│  │ #KGV-2025-00846  S. Weber       Angebot       [ÖFFNEN]   │ │
│  │ #KGV-2025-00845  M. Klein       Vollständig   [ÖFFNEN]   │ │
│  │ #KGV-2025-00844  P. König       Rückfrage     [ÖFFNEN]   │ │
│  │                                              [Alle anzeigen] │
│  └────────────────────────────────────────────────────────────┘ │
│                                                                 │
│  ┌─ Quick Actions ──────────────────────────────────────────┐  │
│  │ 📝 Neuen Antrag erfassen    📊 Bericht erstellen        │  │
│  │ 🔍 Antrag suchen            📧 Serien-E-Mail senden     │  │
│  │ 📋 Warteliste verwalten     ⚙️ Vorlagen bearbeiten      │  │
│  └────────────────────────────────────────────────────────────┘  │
└─────────────────────────────────────────────────────────────────┘
```

### 2. Antragsliste (mit Filtern)

```
Anträge verwalten
┌─────────────────────────────────────────────────────────────────┐
│ ← Dashboard                                          👤 M. Schmidt │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  Filter & Suche                                                 │
│  ═══════════════                                                │
│                                                                 │
│  🔍 [Suche: Name, Antragsnummer...         ]  [SUCHEN]         │
│                                                                 │
│  Status:    [Alle Status ▼]     Bezirk: [Alle Bezirke ▼]      │
│  Zeitraum:  [Letzter Monat ▼]   Sort.:  [Datum ↓ ▼]           │
│                                                                 │
│  Ergebnisse: 127 Anträge                           [EXPORT CSV] │
│  ═══════════════════════════════════════════════════════════════│
│                                                                 │
│  ☐ Antragsnr.    Name            Status        Bezirk   Datum  │
│  ┌─────────────────────────────────────────────────────────────┐ │
│  │☐ KGV-2025-00847 Müller, Thomas  ⏳Warteliste A,E   20.01.25│ │
│  │☐ KGV-2025-00846 Weber, Sabine   📧Angebot    B      18.01.25│ │
│  │☐ KGV-2025-00845 Klein, Markus   ✅Vollständig C      17.01.25│ │
│  │☐ KGV-2025-00844 König, Petra    ❓Rückfrage  A      16.01.25│ │
│  │☐ KGV-2025-00843 Schulz, Frank   🔄Bearbeitung E      15.01.25│ │
│  │☐ KGV-2025-00842 Meyer, Lisa     📝Neu        D      14.01.25│ │
│  │☐ KGV-2025-00841 Wagner, Klaus   ❌Abgelehnt  B      13.01.25│ │
│  └─────────────────────────────────────────────────────────────┘ │
│                                                                 │
│  ☐ Alle auswählen                                               │
│                                                                 │
│  Aktionen für ausgewählte Anträge:                              │
│  [STATUS ÄNDERN] [E-MAIL SENDEN] [DOKUMENT ERSTELLEN] [LÖSCHEN] │
│                                                                 │
│  ← 1 2 3 ... 13 →                               Seite 1 von 13 │
└─────────────────────────────────────────────────────────────────┘
```

### 3. Antrag bearbeiten (Detailansicht)

```
Antrag #KGV-2025-00847 bearbeiten
┌─────────────────────────────────────────────────────────────────┐
│ ← Antragsliste                                   👤 M. Schmidt │
├─────────────────────────────────────────────────────────────────┤
│                                                                 │
│  Thomas Müller - Kleingartenantrag                              │
│  ═════════════════════════════════                              │
│                                                                 │
│  ┌─ Status & Workflow ────────────────────────────────────────┐ │
│  │ Aktueller Status: ⏳ Warteliste eingeordnet               │ │
│  │                                                            │ │
│  │ Nächste Schritte:                                         │ │
│  │ [WARTEN] [ANGEBOT ERSTELLEN] [RÜCKFRAGE] [ABLEHNEN]       │ │
│  │                                                            │ │
│  │ Priorität: ○ Niedrig ●Normal ○ Hoch                       │ │
│  │ Sachbearbeiter: Maria Schmidt           [ZUWEISEN]         │ │
│  └────────────────────────────────────────────────────────────┘ │
│                                                                 │
│  📋 Grunddaten              📁 Dokumente         💬 Verlauf     │
│  ▔▔▔▔▔▔▔▔▔▔▔                                                    │
│                                                                 │
│  ┌─ Persönliche Daten ───────┐  ┌─ Kontaktdaten ─────────────┐ │
│  │ Anrede: Herr               │  │ Telefon: +49 30 1234567   │ │
│  │ Name: Thomas Müller        │  │ Mobil: +49 179 9876543    │ │  
│  │ Geboren: 15.03.1972        │  │ E-Mail: t.mueller@web.de  │ │
│  │ Adresse: Musterstr. 123    │  │ Status: ✅ E-Mail bestätigt│ │
│  │          12345 Berlin      │  │                           │ │
│  └────────────────────────────┘  └───────────────────────────┘ │
│                                                                 │
│  ┌─ Gartenwünsche ──────────────────────────────────────────┐  │
│  │ Bevorzugte Bezirke: A (Rang #23), E (Rang #45)          │  │
│  │ Gewünschte Größe: Flexibel (200-400m²)                  │  │
│  │ Besondere Wünsche: "Suche einen ruhigen Garten für..."  │  │
│  │                                              [BEARBEITEN] │  │
│  └────────────────────────────────────────────────────────────┘  │
│                                                                 │
│  ┌─ Interne Vermerke ───────────────────────────────────────┐  │
│  │ 20.01.2025 - M.Schmidt: Auf Warteliste eingeordnet      │  │
│  │ 18.01.2025 - M.Schmidt: Vollständigkeitsprüfung OK      │  │
│  │ 16.01.2025 - System: Eingang bestätigt                  │  │
│  │                                                          │  │
│  │ Neuer Vermerk:                                           │  │
│  │ [Vermerk eingeben...                                   ] │  │
│  │                                        [VERMERK HINZUFÜGEN] │  │
│  └────────────────────────────────────────────────────────────┘  │
│                                                                 │
│  [SPEICHERN] [E-MAIL SENDEN] [DOKUMENT ERSTELLEN] [DRUCKEN]    │
└─────────────────────────────────────────────────────────────────┘
```

## Mobile Wireframes (Responsive Patterns)

### Bürger-Portal Mobile Navigation

```
Mobile Bottom Navigation:
┌─────────────────────┐
│                     │
│   [Hauptinhalt]     │
│                     │
│                     │
├─────────────────────┤
│🏠 Home │📋 Status │❓ Hilfe │👤 Profil│
│ Start │Anträge│ FAQ │Account│
└─────────────────────┘

Mobile Hamburger Menu (Verwaltung):
┌─────────────────────┐
│☰ Dashboard    🔔 👤 │
├─────────────────────┤
│                     │
│ [Hauptinhalt]       │
│                     │
└─────────────────────┘

Ausgeklapptes Menu:
┌─────────────────────┐
│📊 Dashboard         │
│📋 Anträge           │
│👥 Wartelisten       │
│📊 Berichte          │
│⚙️ Administration    │
│❓ Hilfe             │
│🚪 Abmelden          │
└─────────────────────┘
```

## Design System Komponenten

### Form Elements

```
Input Field States:
┌─────────────────────┐
│ Default State       │
│ [Eingabefeld     ]  │
└─────────────────────┘

┌─────────────────────┐
│ Focus State         │
│ [Eingabefeld     ]  │ ← Blauer Rahmen
└─────────────────────┘

┌─────────────────────┐
│ Error State         │
│ [Ungültiger Wert ]  │ ← Roter Rahmen  
│ ❌ Fehlermeldung    │
└─────────────────────┘

┌─────────────────────┐
│ Success State       │
│ [Korrekter Wert  ]  │ ← Grüner Rahmen
│ ✅ Validiert        │
└─────────────────────┘
```

### Status Indicators

```
Antragsstatus:
📝 Neu eingegangen    (Gelb)
🔄 In Bearbeitung     (Blau)
⏳ Auf Warteliste     (Orange) 
📧 Angebot versandt   (Lila)
✅ Abgeschlossen      (Grün)
❌ Abgelehnt          (Rot)
⚠️ Rückfrage nötig    (Gelb-Orange)
```

### Responsive Data Tables

```
Desktop Ansicht:
┌──────────────────────────────────────────────────────┐
│Antragsnr.    Name         Status    Bezirk    Datum  │
│KGV-00847    T. Müller     ⏳Liste    A,E      20.01 │
│KGV-00846    S. Weber      📧Angebot  B        18.01 │
└──────────────────────────────────────────────────────┘

Mobile Ansicht (Card Layout):
┌─────────────────────┐
│ 📋 KGV-00847        │
│ Thomas Müller       │
│ ⏳ Warteliste       │
│ Bezirke: A, E       │
│ 20.01.2025          │
│            [ÖFFNEN] │
└─────────────────────┘
┌─────────────────────┐
│ 📋 KGV-00846        │
│ Sabine Weber        │
│ 📧 Angebot          │
│ Bezirk: B           │
│ 18.01.2025          │
│            [ÖFFNEN] │
└─────────────────────┘
```

## Interaction Patterns

### Progressive Disclosure

```
Antragsliste - Collapsed:
┌─────────────────────────────────────────────────────┐
│ KGV-00847  T. Müller  ⏳Warteliste  A,E  [▼ Mehr] │
└─────────────────────────────────────────────────────┘

Antragsliste - Expanded:
┌─────────────────────────────────────────────────────┐
│ KGV-00847  T. Müller  ⏳Warteliste  A,E  [▲ Weniger] │
│ ├─ Tel: +49 30 1234567                             │
│ ├─ E-Mail: thomas.mueller@example.com              │
│ ├─ Eingegangen: 15.01.2025                         │
│ ├─ Position A: #23, Position E: #45                │
│ └─ [BEARBEITEN] [E-MAIL] [DOKUMENT]                │
└─────────────────────────────────────────────────────┘
```

### Loading States

```
Lade-Zustand:
┌─────────────────────────────────────┐
│ 🔄 Antrag wird verarbeitet...       │
│ ████████░░░░  67%                   │
│                                     │
│ Schritt 2 von 3: Vollständigkeits- │
│ prüfung läuft                       │
└─────────────────────────────────────┘

Skeleton Loading:
┌─────────────────────────────────────┐
│ ▓▓▓▓▓▓▓▓▓▓  ▓▓▓▓▓▓▓▓  ▓▓▓  ▓▓▓▓   │
│ ▓▓▓▓▓▓▓▓▓▓  ▓▓▓▓▓▓▓▓  ▓▓▓  ▓▓▓▓   │
│ ▓▓▓▓▓▓▓▓▓▓  ▓▓▓▓▓▓▓▓  ▓▓▓  ▓▓▓▓   │
└─────────────────────────────────────┘
```

Diese Wireframes bilden die Grundlage für das High-Fidelity Design und die Implementierung des KGV-Systems. Sie berücksichtigen:

- **Benutzerfreundlichkeit**: Intuitive Navigation und klare Hierarchien
- **Responsiveness**: Optimiert für alle Gerätegrößen
- **Accessibility**: Klare Labels, Fokus-Management, Screen-Reader-Support
- **Workflow-Optimierung**: Effiziente Prozesse für Sachbearbeiter
- **Transparenz**: Klare Status-Anzeigen für Bürger
- **Modern UX**: Zeitgemäße Interface-Patterns und Interaktionen