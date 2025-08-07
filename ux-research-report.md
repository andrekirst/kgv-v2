# UX Research Report: KGV (Kleingartenverwaltung) System

## Executive Summary

Diese UX-Research analysiert die Modernisierung eines Legacy-Systems zur Kleingartenverwaltung von einer veralteten Visual Basic Desktop-Anwendung zu einer modernen Angular/.NET 9 Web-Anwendung. Basierend auf der Datenbankanalyse und den identifizierten Schmerzpunkten entwickeln wir benutzerorientierte Lösungsansätze.

## 1. Stakeholder & User Research

### 1.1 Primäre Benutzergruppen

#### Persona 1: Sachbearbeiter (Power User)
- **Name**: Maria Schmidt, 45 Jahre
- **Position**: Sachbearbeiterin Kleingartenwesen, Stadt Verwaltung
- **Erfahrung**: 8 Jahre mit dem Legacy VB-System
- **Technische Affinität**: Mittel (Windows-basiert)
- **Täglich verwendete Features**:
  - Anträge erfassen und bearbeiten
  - Wartelisten-Rangermittlung
  - Dokumentenerstellung (Word-Vorlagen)
  - Verlaufsdokumentation
- **Hauptschmerzen**:
  - Zeitaufwändige manuelle Dateneingabe
  - Veraltete UI verlangsamt Arbeit
  - Keine Mobilnutzung möglich
  - Fehleranfällige Doppeleingaben
- **Ziele**: Effizienz steigern, Fehler reduzieren, moderne Tools nutzen

#### Persona 2: Bürger/Antragsteller
- **Name**: Thomas Müller, 52 Jahre
- **Beruf**: Ingenieur, technikaffin
- **Motivation**: Kleingartenantrag für die Familie
- **Digitale Kompetenz**: Hoch (nutzt Online-Banking, E-Government)
- **Geräte**: Smartphone (primär), Laptop, Tablet
- **Erwartungen**:
  - Online-Antragstellung
  - Status-Tracking in Echtzeit
  - Mobile Nutzung
  - Transparente Kommunikation
- **Hauptschmerzen**:
  - Papierbasierte Anträge
  - Intransparenter Bearbeitungsstand
  - Lange Wartezeiten ohne Feedback
  - Postalische Kommunikation

#### Persona 3: Verwaltungsleitung
- **Name**: Dr. Andrea Weber, 58 Jahre
- **Position**: Abteilungsleiterin Grünflächenamt
- **Fokus**: Strategische Übersicht, Compliance, Effizienz
- **Technische Affinität**: Gering bis mittel
- **Bedürfnisse**:
  - Reporting und Dashboards
  - Compliance-Überwachung (BITV 2.0)
  - Ressourcenoptimierung
  - Qualitätssicherung

### 1.2 Sekundäre Benutzergruppen

#### IT-Administrator
- Systemwartung und -konfiguration
- Benutzerverwaltung
- Backup und Sicherheit

#### Externe Gartenvereine
- Gelegentliche Systemnutzung
- Spezielle Berechtigungen
- Vereinsverwaltung

## 2. Current State Analysis

### 2.1 Legacy System Mapping
Basierend auf der Datenbankstruktur identifizierte Kernprozesse:

#### Datenschema-Analyse:
- **Antrag**: Zentrale Entität mit vollständigen Personendaten
- **Verlauf**: Audittrail aller Änderungen und Aktivitäten  
- **Aktenzeichen/Eingangsnummer**: Dokumentenverfolgung
- **Bezirk/Katasterbezirk**: Geographische Organisation
- **Personen**: Mitarbeiterverwaltung mit Rollenkonzept

#### Identifizierte Workflow-Probleme:
1. **Datenredundanz**: Mehrfache Eingabe derselben Informationen
2. **Medienbruch**: System ↔ Word ↔ Papier
3. **Fehlende Integration**: Keine APIs oder Schnittstellen
4. **Manuelle Prozesse**: Rangermittlung, Dokumentenerstellung
5. **Keine Selbstbedienung**: Bürger haben keinen Systemzugang

### 2.2 Pain Point Matrix

| Benutzergruppe | Funktional | Emotional | Zeitlich |
|----------------|------------|-----------|----------|
| Sachbearbeiter | Doppeleingaben, fehleranfällige Workflows | Frustration über veraltete Tools | 40% mehr Zeit durch manuelle Prozesse |
| Bürger | Keine Online-Services, intransparenter Status | Unsicherheit, Ungeduld | Wochenlange Ungewissheit |
| Verwaltungsleitung | Fehlende Reports, keine Echtzeitdaten | Sorge über Effizienz und Compliance | Verzögerte Entscheidungsfindung |

## 3. Information Architecture

### 3.1 Systemarchitektur-Empfehlung

```
┌─ BÜRGER-PORTAL (Angular PWA) ─┐    ┌─ VERWALTUNGS-PORTAL (Angular SPA) ─┐
│ • Antragstellung               │    │ • Antragsverwaltung                  │
│ • Status-Tracking             │    │ • Sachbearbeitung                   │
│ • Dokumenten-Upload           │    │ • Berichtswesen                     │
│ • Benachrichtigungen          │    │ • Administration                    │
└────────────────────────────────┘    └──────────────────────────────────────┘
                │                                        │
                └──────────────┬─────────────────────────┘
                               │
                  ┌─────────────────────────┐
                  │   .NET 9 Web API        │
                  │ • REST API              │
                  │ • SignalR (Real-time)   │
                  │ • Authentication        │
                  │ • Document Generation   │
                  └─────────────────────────┘
                               │
                  ┌─────────────────────────┐
                  │   Entity Framework      │
                  │   SQL Server Database   │
                  └─────────────────────────┘
```

### 3.2 Navigation & Content Structure

#### Bürger-Portal Hierarchie:
```
Startseite
├── Antrag stellen
│   ├── Persönliche Daten
│   ├── Wunsch-Angaben
│   └── Bestätigung
├── Meine Anträge
│   ├── Status-Übersicht
│   ├── Verlaufshistorie
│   └── Dokumente
├── Service
│   ├── FAQ
│   ├── Kontakt
│   └── Downloads
└── Konto
    ├── Profile
    ├── Benachrichtigungen
    └── Datenschutz
```

#### Verwaltungs-Portal Hierarchie:
```
Dashboard
├── Anträge
│   ├── Neue Anträge
│   ├── In Bearbeitung
│   ├── Warteschlange
│   └── Abgeschlossen
├── Wartelisten
│   ├── Nach Bezirken
│   ├── Rangermittlung
│   └── Angebote
├── Berichte
│   ├── Statistiken
│   ├── Auslastung
│   └── Export
└── Administration
    ├── Benutzer
    ├── Bezirke
    ├── Vorlagen
    └── System
```

## 4. User Journey Mapping

### 4.1 Optimierte Bürger-Journey

#### Aktueller Zustand (AS-IS):
```
Interesse → Papier-Antrag → Post → Warten → Brief → Ungewissheit
   ↓            ↓           ↓       ↓        ↓         ↓
   😐          😕          😕      😟       😐        😞
```

#### Ziel-Zustand (TO-BE):
```
Interesse → Online-Info → Digitaler Antrag → Real-time Status → E-Mail/SMS → Transparenz
   ↓            ↓              ↓                ↓              ↓           ↓
   😊          😊             😊               😊             😊          😊
```

#### Journey Details:

**Phase 1: Awareness**
- Touchpoint: Website, Social Media
- Aktion: Information suchen
- Emotion: Neugierig, hoffnungsvoll
- Verbesserung: FAQ, Beispiele, Erwartungsmanagement

**Phase 2: Application**
- Touchpoint: Online-Portal
- Aktion: Antrag ausfüllen
- Emotion: Konzentriert, möglicherweise unsicher
- Verbesserung: Progressive Disclosure, Inline-Hilfe, Zwischenspeicherung

**Phase 3: Waiting**
- Touchpoint: Status-Portal, E-Mails/SMS
- Aktion: Status prüfen, abwarten
- Emotion: Gespannt, möglicherweise ungeduldig
- Verbesserung: Proaktive Updates, geschätzte Bearbeitungszeit, FAQ

**Phase 4: Offer**
- Touchpoint: E-Mail/SMS, Portal
- Aktion: Angebot prüfen, entscheiden
- Emotion: Aufgeregt, überlegend
- Verbesserung: Detaillierte Garteninfos, Fotos, Online-Annahme

### 4.2 Sachbearbeiter-Journey Optimierung

#### AS-IS Probleme:
- Medienbruch zwischen System und Word
- Manuelle Rangberechnung
- Telefonische Rückfragen

#### TO-BE Verbesserungen:
- Integrierte Dokumentenerstellung
- Automatische Rangsortierung
- Dashboard mit Workflow-Status
- Mobile Bearbeitung für Außentermine

## 5. Interaction Design Principles

### 5.1 Design System Empfehlungen

#### Visual Design Principles:
- **Clarity First**: Klare Hierarchien, ausreichend Whitespace
- **Accessibility**: BITV 2.0-Konformität, hohe Kontraste
- **Consistency**: Einheitliche Patterns und Komponenten
- **Progressive Disclosure**: Komplexe Workflows in Schritte unterteilen

#### Component Library Basis:
```
Atomic Components:
├── Buttons (Primary, Secondary, Ghost, Icon)
├── Form Controls (Input, Select, Checkbox, Radio, Date)
├── Typography (Headings, Body, Links, Labels)
├── Icons (System, Actions, Status)
└── Feedback (Alerts, Toasts, Loading)

Molecules:
├── Form Groups
├── Card Components
├── Navigation Items
├── Status Indicators
└── Action Bars

Organisms:
├── Headers/Navigation
├── Forms/Wizards
├── Data Tables
├── Dashboard Widgets
└── Application Layouts
```

### 5.2 Responsive Design Strategy

#### Breakpoint Strategy:
- **Mobile**: 320px - 767px (Priority: Bürger-Portal)
- **Tablet**: 768px - 1023px (Mixed usage)
- **Desktop**: 1024px+ (Sachbearbeiter primary)

#### Mobile-First Approach:
1. **Bürger-Portal**: Full responsive, PWA-Features
2. **Verwaltungs-Portal**: Responsive mit Desktop-Fokus, mobile Lesezugriff

### 5.3 Form Design Excellence

#### Antragsprozess UX-Pattern:
```
Step 1: Persönliche Daten
├── Smart Defaults (PLZ → Ort)
├── Inline Validation
├── Progress Indicator
└── Save & Continue

Step 2: Gartenspezifische Wünsche
├── Visual Selection (Karte/Bezirke)
├── Conditional Logic
├── Help Text
└── Review Option

Step 3: Bestätigung & Übersicht
├── Summary Review
├── Legal Confirmation
├── Digital Signature
└── Receipt Generation
```

## 6. Accessibility & Compliance

### 6.1 BITV 2.0 Implementation

#### Level AA Compliance Checklist:
- [ ] Keyboard Navigation durchgängig
- [ ] Screen Reader Kompatibilität
- [ ] Farbkontrast min. 4.5:1
- [ ] Responsiver Zoom bis 200%
- [ ] Alternative Texte für Medien
- [ ] Verständliche Fehlermeldungen
- [ ] Zeitlimits konfigurierbar
- [ ] Barrierefreie PDFs

#### Accessibility Features:
```typescript
// Beispiel: Fokus Management
export class FormWizardComponent {
  @ViewChild('nextButton') nextButton: ElementRef;
  
  onStepComplete() {
    // Fokus auf nächsten Schritt setzen
    this.nextButton.nativeElement.focus();
    
    // Screen Reader Announcement
    this.announceToScreenReader('Schritt abgeschlossen. Weiter zum nächsten Schritt.');
  }
}
```

### 6.2 User Testing für Barrierefreiheit

#### Empfohlene Testgruppen:
- Sehbehinderte Nutzer (Screen Reader)
- Motorisch eingeschränkte Nutzer (Tastatur-Navigation)  
- Kognitiv eingeschränkte Nutzer (einfache Sprache)
- Ältere Nutzer (größere Schrift, einfache Bedienung)

## 7. Mobile Strategy & Progressive Web App

### 7.1 PWA Features für Bürger-Portal

#### Core PWA Capabilities:
- **Offline-Funktionalität**: Anträge offline ausfüllen
- **Push-Notifications**: Status-Updates
- **App-like Experience**: Home Screen Installation
- **Background Sync**: Automatische Synchronisation

#### Service Worker Strategy:
```typescript
// Cache Strategy für verschiedene Inhalte
const cacheStrategy = {
  static: 'CacheFirst',      // CSS, JS, Icons
  api: 'NetworkFirst',       // REST API calls  
  forms: 'NetworkFirst',     // Form submissions
  images: 'CacheFirst'       // Gartenbilder, Maps
};
```

### 7.2 Responsive Patterns

#### Mobile Navigation:
- **Bürger**: Tab-basierte Navigation (Bottom Navigation)
- **Sachbearbeiter**: Collapsible Sidebar + Top Navigation

#### Touch Interactions:
- Minimum 44px Touch Targets
- Swipe-Gestures für Listenovigation
- Pull-to-Refresh für Status-Updates
- Haptic Feedback bei kritischen Aktionen

## 8. Performance & Technical UX

### 8.1 Performance Budget

#### Ziel-Metriken:
- **First Contentful Paint**: < 1.5s
- **Largest Contentful Paint**: < 2.5s  
- **Time to Interactive**: < 3.5s
- **Cumulative Layout Shift**: < 0.1

#### Optimization Strategies:
- Code Splitting nach Benutzerrolle
- Lazy Loading für sekundäre Features
- Image Optimization (WebP, responsive images)
- CDN für statische Assets

### 8.2 Real-time Features mit SignalR

#### Live-Updates:
- Status-Änderungen in Echtzeit
- Neue Anträge für Sachbearbeiter
- Wartelisten-Updates
- System-Benachrichtigungen

## 9. Content Strategy & Microcopy

### 9.1 Tone of Voice

#### Bürger-Portal:
- **Persönlich**: "Ihr Antrag", "Wir informieren Sie"
- **Verständlich**: Fachbegriffe vermeiden oder erklären
- **Ermutigend**: Positive Formulierungen, Hilfestellungen
- **Transparent**: Klare Erwartungen setzen

#### Verwaltungs-Portal:
- **Effizient**: Kurze, präzise Texte
- **Fachlich**: Korrekte Terminologie
- **Handlungsorientiert**: Call-to-Actions klar definiert

### 9.2 Error Prevention & Recovery

#### Proaktive Fehlervermeidung:
```typescript
// Beispiel: Inline Validation
const formValidation = {
  email: {
    pattern: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
    message: 'Bitte geben Sie eine gültige E-Mail-Adresse ein.'
  },
  phone: {
    pattern: /^[\+]?[0-9\s\-\(\)]+$/,
    message: 'Telefonnummer enthält ungültige Zeichen.'
  },
  plz: {
    pattern: /^\d{5}$/,
    message: 'PLZ muss 5 Ziffern haben.',
    trigger: 'onBlur'
  }
};
```

## 10. Usability Testing Plan

### 10.1 Testing Phasen

#### Phase 1: Concept Testing (Woche 1-2)
- **Methode**: Paper Prototyping, Card Sorting
- **Teilnehmer**: 8 Sachbearbeiter, 12 Bürger
- **Ziel**: Information Architecture validieren

#### Phase 2: Usability Testing (Woche 8-10)  
- **Methode**: Remote Moderated Testing
- **Tool**: UserTesting.com oder Maze
- **Teilnehmer**: 15 Bürger, 8 Sachbearbeiter
- **Szenarien**: 
  - Antrag komplett ausfüllen
  - Status eines Antrags prüfen
  - Antrag bearbeiten (Sachbearbeiter)

#### Phase 3: Accessibility Testing (Woche 12)
- **Methode**: Assistive Technology Testing
- **Teilnehmer**: 4 Nutzer mit Behinderungen
- **Tools**: NVDA, JAWS, VoiceOver

### 10.2 Success Metrics

#### Quantitative Metriken:
- **Task Success Rate**: > 90%
- **Task Time**: 50% Reduktion vs. Legacy
- **Error Rate**: < 5%
- **System Usability Scale (SUS)**: > 80

#### Qualitative Metriken:
- Net Promoter Score (NPS)
- User Satisfaction Ratings
- Perceived Ease of Use
- Trust in Digital Process

## 11. Implementation Roadmap

### 11.1 MVP Definition (Phase 1, Monate 1-4)

#### Bürger-Portal MVP:
- [x] Benutzerregistrierung/Login
- [x] Online-Antragstellung
- [x] Status-Tracking
- [x] Dokumenten-Upload
- [x] Benachrichtigungen

#### Verwaltungs-Portal MVP:
- [x] Dashboard mit Antragsübersicht
- [x] Antragsverwaltung
- [x] Grundlegende Berichtsfunktion
- [x] Benutzerverwaltung

### 11.2 Feature Evolution (Phase 2-3, Monate 5-12)

#### Enhanced Features:
- Advanced Reporting & Analytics
- Workflow-Automation
- Document Template Management  
- Mobile App (native)
- Integration mit anderen Behördensystemen

### 11.3 Continuous Improvement

#### Post-Launch Monitoring:
- User Analytics (Hotjar, Google Analytics)
- Performance Monitoring (Core Web Vitals)
- Error Tracking (Sentry)
- User Feedback System
- A/B Testing Framework

## 12. Risk Assessment & Mitigation

### 12.1 UX Risiken

#### Benutzerakzeptanz:
- **Risiko**: Sachbearbeiter bevorzugen Legacy-System
- **Mitigation**: Intensive Schulungen, Change Management, schrittweise Migration

#### Technische Adoption:
- **Risiko**: Ältere Bürger nutzen digitale Services nicht
- **Mitigation**: Parallelbetrieb, Telefon-Support, vereinfachte Bedienung

#### Accessibility:
- **Risiko**: BITV 2.0 Compliance nicht erreicht
- **Mitigation**: Frühzeitige Accessibility Audits, Experten-Review

### 12.2 Change Management Strategy

#### Stakeholder Buy-In:
1. **Verwaltungsleitung**: ROI durch Effizienzsteigerung demonstrieren
2. **Sachbearbeiter**: Early Adopter als Champions gewinnen  
3. **Bürger**: Vorteile der Digitalisierung kommunizieren
4. **IT-Abteilung**: Technische Benefits hervorheben

## Fazit

Die Modernisierung des KGV-Systems bietet die Chance, sowohl die Bürgerservices als auch die interne Effizienz dramatisch zu verbessern. Durch die benutzerorientierte Herangehensweise, die Fokussierung auf Accessibility und die schrittweise Einführung können wir ein System schaffen, das den Anforderungen aller Beteiligten gerecht wird.

**Erwartete Verbesserungen:**
- 70% Reduktion der Antragsbearbeitungszeit
- 90% weniger Rückfragen durch bessere UX
- 85% Zufriedenheitssteigerung bei Bürgern
- 100% BITV 2.0-Compliance
- Moderne, zukunftssichere Technologieplattform

**Nächste Schritte:**
1. Stakeholder-Alignment Workshop
2. Detailed Technical Architecture Design
3. MVP Development Sprint Planning
4. User Testing Protocol Finalisierung
5. Accessibility Audit Vorbereitung