# KGV-System Business Requirements

## Überblick

Dieses Dokument definiert die fachlichen Anforderungen für die Modernisierung des Kleingartenverwaltungs-Systems (KGV). Die Requirements basieren auf der Analyse des Legacy Visual Basic Systems und berücksichtigen moderne behördliche Anforderungen.

## 1. Stakeholder und Benutzergruppen

### Primäre Benutzergruppen

#### 1.1 Bürger/Antragsteller
**Beschreibung**: Personen, die sich für Kleingartenflächen bewerben
**Anzahl**: Ca. 500-1.000 jährlich
**Charakteristika**:
- Verschiedene Altersgruppen (25-75 Jahre)
- Unterschiedliche IT-Affinität
- Erwarten einfache, verständliche Prozesse
- Benötigen Transparenz über Antragsstatus

#### 1.2 Sachbearbeiter
**Beschreibung**: Verwaltungsmitarbeiter für die Antragsbearbeitung
**Anzahl**: 3-5 Personen
**Charakteristika**:
- Erfahrung mit dem Legacy-System
- Bearbeiten täglich 5-15 Anträge
- Benötigen effiziente Arbeitsabläufe
- Rechtssichere Dokumentation erforderlich

#### 1.3 Verwaltungsleitung
**Beschreibung**: Abteilungs-/Amtsleitung für strategische Entscheidungen
**Anzahl**: 2-3 Personen  
**Charakteristika**:
- Benötigen Kennzahlen und Berichte
- Interessiert an Kosteneinsparungen
- Compliance und Rechtssicherheit wichtig

### Sekundäre Stakeholder
- **IT-Administrator**: Systemwartung und Benutzerverwaltung
- **Datenschutzbeauftragte**: DSGVO-Compliance
- **Rechtsabteilung**: Vergaberecht und Vertragsgestaltung

## 2. Geschäftsprozesse

### 2.1 Hauptprozess: Antragstellung und -bearbeitung

#### 2.1.1 Antragstellung (Bürger-Perspektive)
**Ziel**: Vereinfachte, digitale Antragstellung

**IST-Prozess (Legacy)**:
1. Schriftlichen Antrag ausdrucken und ausfüllen
2. Per Post an Verwaltung senden
3. Warten auf postalische Eingangsbestätigung
4. Ungewissheit über Bearbeitungsstatus
5. Benachrichtigung über Angebot nur per Post

**SOLL-Prozess (Modern)**:
1. Online-Formular aufrufen (responsive, barrierefrei)
2. Antragsdaten digital eingeben mit Validierung
3. Sofortige Eingangsbestätigung per E-Mail
4. Tracking-Link für Statusverfolgung
5. Benachrichtigungen per E-Mail/SMS
6. Online-Rückmeldung zu Angeboten möglich

#### 2.1.2 Antragsbearbeitung (Sachbearbeiter-Perspektive)
**Ziel**: Effiziente, fehlerfreie Bearbeitung

**IST-Prozess (Legacy)**:
1. Papier-Eingang manuell sortieren
2. Daten in VB-Anwendung eingeben
3. Aktenzeichen manuell generieren
4. Word-Dokument für Eingangsbestätigung erstellen
5. Ausdrucken, unterschreiben, versenden
6. Bei Vergabe: erneut Word-Dokument erstellen

**SOLL-Prozess (Modern)**:
1. Automatische Benachrichtigung bei neuem Antrag
2. Daten bereits digital vorhanden und validiert
3. Automatische Aktenzeichen-Generierung
4. Ein-Klick Dokumentenerstellung (PDF)
5. Automatischer E-Mail-Versand
6. Dashboard für Übersicht und Priorisierung

### 2.2 Ranking und Wartelisten-Verwaltung

#### Business Rules für Ranking:
- **Grundprinzip**: First-In-First-Out (FIFO) basierend auf Bewerbungsdatum
- **Gemarkungsspezifisch**: Separate Wartelisten pro Gemarkung
- **Position**: Automatische Berechnung bei Antragstellung
- **Neuberechnung**: Bei Löschung/Rückzug eines Antrags

#### Ranking-Algorithmus:
```
Rang = Position in chronologisch sortierter Liste (nach Bewerbungsdatum ASC)
WHERE Gemarkung = WunschGemarkung AND Status = 'Aktiv'
```

### 2.3 Angebotsprozess

#### Angebotserstellung:
1. **Trigger**: Verfügbare Parzelle wird zur Vergabe freigegeben
2. **Kandidatenauswahl**: Höchstrangiger Antragsteller für entsprechende Gemarkung
3. **Prüfung**: Zusätzliche Kriterien (Flächengröße, Bedingungen)
4. **Angebotserstellung**: Automatische PDF-Generierung
5. **Versendung**: E-Mail mit Angebot und Rückmeldefrist
6. **Tracking**: Automatische Erinnerungen bei fehlender Rückmeldung

### 2.4 Gültigkeits- und Verlängerungsmanagement

#### Gültigkeitsregeln:
- **Standard-Gültigkeit**: 12 Monate ab Antragsdatum
- **Verlängerung**: Auf Antrag um weitere 12 Monate
- **Maximale Verlängerungen**: 3x (insgesamt 4 Jahre)
- **Automatische Benachrichtigung**: 4 Wochen vor Ablauf

#### Ablaufprozess:
1. **30 Tage vor Ablauf**: E-Mail-Erinnerung an Antragsteller
2. **14 Tage vor Ablauf**: Zweite Erinnerung
3. **Bei Ablauf**: Automatische Archivierung
4. **Rang-Neuberechnung**: Für betroffene Gemarkung

## 3. Funktionale Anforderungen

### 3.1 Bürger-Portal (Public Frontend)

#### REQ-BP-001: Online-Antragsformular
**Beschreibung**: Bürger können online Anträge für Kleingartenflächen stellen
**Akzeptanzkriterien**:
- Responsive Design für alle Geräte
- Schritt-für-Schritt Wizard mit Validierung
- Pflichtfelder klar markiert
- Automatische PLZ/Ort-Vervollständigung
- Gemarkungsauswahl mit Karte
- Speichern als Entwurf möglich
- Barrierefreiheit nach BITV 2.0

#### REQ-BP-002: Antragsstatus-Tracking
**Beschreibung**: Antragsteller können jederzeit den Status ihres Antrags einsehen
**Akzeptanzkriterien**:
- Login über Aktenzeichen + E-Mail
- Status-Dashboard mit Timeline
- Aktuelle Rang-Position anzeigen
- Geschätzte Wartezeit (basierend auf historischen Daten)
- Downloadbare Dokumente (Bestätigungen, Angebote)
- Push-Benachrichtigungen bei Status-Änderungen

#### REQ-BP-003: Angebots-Rückmeldung
**Beschreibung**: Online-Rückmeldung zu erhaltenen Angeboten
**Akzeptanzkriterien**:
- Angebot-Details anzeigen (Lage, Größe, Bedingungen)
- Annahme/Ablehnung mit einem Klick
- Rückfragen-Funktion an Sachbearbeiter
- Automatische Frist-Überwachung
- Bestätigungs-E-Mail nach Rückmeldung

### 3.2 Verwaltungsportal (Internal Frontend)

#### REQ-VP-001: Dashboard für Sachbearbeiter
**Beschreibung**: Übersichtliche Arbeitsoberfläche für tägliche Aufgaben
**Akzeptanzkriterien**:
- Neue Anträge (erfordern Bearbeitung)
- Fällige Termine (Fristen, Erinnerungen)
- Warteschlangen pro Gemarkung
- Persönliche Arbeitsstatistiken
- Quick-Actions für häufige Aufgaben
- Konfigurierbare Widgets

#### REQ-VP-002: Antragsverwaltung
**Beschreibung**: Vollständige CRUD-Operationen für Anträge
**Akzeptanzkriterien**:
- Erweiterte Suchfunktionen (nach Name, Aktenzeichen, Gemarkung, Status)
- Massenaktion-Möglichkeiten
- Inline-Editing für schnelle Änderungen
- Verlaufshistorie mit Audit-Log
- Kommentar-/Notiz-Funktion
- Dokumenten-Upload (Nachweise, Korrespondenz)

#### REQ-VP-003: Wartelisten-Management
**Beschreibung**: Verwaltung und Anzeige der Wartelisten
**Akzeptanzkriterien**:
- Sortierbare Listen pro Gemarkung
- Rang-Position mit manueller Korrekturmöglichkeit
- Filter nach Status, Zeitraum, Kriterien
- Export-Funktionen (Excel, PDF)
- Prognose-Tool für Vergabe-Zeiträume
- Grafische Auswertungen

#### REQ-VP-004: Angebotserstellung
**Beschreibung**: Erstellung und Verwaltung von Vergabeangeboten
**Akzeptanzkriterien**:
- Wizard für Angebotserstellung
- Parzellen-Datenbank mit Verfügbarkeitsstatus
- Template-basierte Dokumentgenerierung
- Mehrfach-Angebote (bei Ablehnungen)
- Frist-Management mit automatischen Erinnerungen
- Integration mit E-Mail-Versand

#### REQ-VP-005: Dokumentenerstellung
**Beschreibung**: Automatische Generierung aller relevanten Dokumente
**Akzeptanzkriterien**:
- Eingangsbestätigungen (personalisiert)
- Angebotsbriefe mit Parzellen-Details
- Verlängerungs-Bestätigungen
- Ablehnungsschreiben
- Anpassbare Templates
- Digitale Signatur-Integration

### 3.3 Administrations-Portal

#### REQ-AP-001: Benutzerverwaltung
**Beschreibung**: Verwaltung von Benutzerkonten und Berechtigungen
**Akzeptanzkriterien**:
- CRUD für Sachbearbeiter-Accounts
- Rollen-basierte Rechteverwaltung
- Aktivierung/Deaktivierung von Accounts
- Passwort-Reset-Funktion
- Aktivitätsprotokolle
- Integration mit Active Directory (optional)

#### REQ-AP-002: Systemkonfiguration
**Beschreibung**: Konfiguration systemweiter Einstellungen
**Akzeptanzkriterien**:
- E-Mail-Templates bearbeiten
- Geschäftsregeln konfigurieren (Fristen, Limits)
- Gemarkungen und Parzellen verwalten
- Gebühren und Tarife definieren
- Backup-/Archivierungs-Richtlinien
- System-Monitoring Dashboard

#### REQ-AP-003: Berichtswesen
**Beschreibung**: Umfassende Berichte und Statistiken
**Akzeptanzkriterien**:
- Vordefinierte Standard-Berichte
- Benutzerdefinierte Report-Builder
- Echtzeit-Dashboards
- Export in verschiedene Formate
- Automatische Berichtsverteilung
- Historische Trend-Analysen

### 3.4 Integration und Schnittstellen

#### REQ-INT-001: E-Mail-Integration
**Beschreibung**: Automatisierter E-Mail-Versand für alle Benachrichtigungen
**Akzeptanzkriterien**:
- SMTP-Server Anbindung
- Template-basierte E-Mails
- Attachment-Support (PDF-Dokumente)
- Delivery-Status-Tracking
- Queue-Management bei Ausfällen
- Spam-Filter-Kompatibilität

#### REQ-INT-002: Dokumenten-Management
**Beschreibung**: Integration mit bestehenden DMS-Systemen
**Akzeptanzkriterien**:
- PDF/A-konforme Archivierung
- Volltextsuche in Dokumenten
- Versionierung von Dokumenten
- Bulk-Import von Legacy-Dokumenten
- Rechtssichere elektronische Signatur
- OCR für gescannte Dokumente

## 4. Non-funktionale Anforderungen

### 4.1 Performance

#### REQ-NF-001: Antwortzeiten
- **Web-Interface**: < 2 Sekunden für 95% der Requests
- **API-Calls**: < 1 Sekunde für CRUD-Operationen
- **Berichte**: < 10 Sekunden für Standard-Reports
- **Dokumentenerstellung**: < 5 Sekunden für PDF-Generierung

#### REQ-NF-002: Concurrent Users
- **Minimum**: 50 gleichzeitige Benutzer ohne Performance-Degradation
- **Peak**: 100 Benutzer während Stoßzeiten
- **Bürger-Portal**: 200 gleichzeitige Besucher

### 4.2 Verfügbarkeit

#### REQ-NF-003: System-Verfügbarkeit
- **Bürozeiten (Mo-Fr 7-18h)**: 99.5% Uptime
- **Außerhalb Bürozeiten**: 95% Uptime
- **Wartungsfenster**: Sonntags 2-6 Uhr
- **Recovery Time Objective (RTO)**: 4 Stunden
- **Recovery Point Objective (RPO)**: 1 Stunde

### 4.3 Sicherheit

#### REQ-NF-004: Authentifizierung & Autorisierung
- **Multi-Factor Authentication** für Verwaltungsportal
- **Role-based Access Control** mit granularen Berechtigungen
- **Session-Timeout**: 30 Minuten Inaktivität
- **Passwort-Richtlinien**: Mindestens 8 Zeichen, komplexe Anforderungen
- **Account-Lockout**: Nach 5 fehlerhaften Login-Versuchen

#### REQ-NF-005: Datenschutz (DSGVO)
- **Datenminimierung**: Nur erforderliche Daten erfassen
- **Zweckbindung**: Daten nur für Kleingartenvergabe verwenden
- **Löschfristen**: Automatische Löschung nach 10 Jahren
- **Betroffenenrechte**: Auskunft, Berichtigung, Löschung, Portabilität
- **Einwilligungen**: Dokumentation und Widerrufsmöglichkeit

#### REQ-NF-006: Audit & Compliance
- **Vollständiges Audit-Log** aller Datenänderungen
- **Unveränderbarkeit** von Protokoll-Einträgen
- **Rechtssichere Archivierung** für 10 Jahre
- **Compliance-Berichte** für Datenschutzaufsicht

### 4.4 Benutzerfreundlichkeit

#### REQ-NF-007: Barrierefreiheit
- **BITV 2.0 Konformität** (entspricht WCAG 2.1 Level AA)
- **Screen Reader** Kompatibilität
- **Tastatur-Navigation** für alle Funktionen
- **Hohe Kontraste** und skalierbare Schriftarten
- **Einfache Sprache** in Benutzertexten

#### REQ-NF-008: Mobile Responsiveness
- **Mobile-First Design** für Bürger-Portal
- **Touch-optimierte Navigation** (mind. 44px Touch-Targets)
- **Offline-Funktionalität** für Antragsentwürfe
- **Progressive Web App** Features

### 4.5 Integration

#### REQ-NF-009: Datenintegration
- **Legacy-Datenübernahme** ohne Datenverlust
- **Real-time Synchronisation** während Parallelbetrieb
- **Export/Import** in Standard-Formaten (CSV, XML, JSON)
- **REST API** für Drittsystem-Integration

## 5. Business Rules

### 5.1 Antragsbearbeitung

#### BR-001: Aktenzeichen-Format
- **Format**: 32.2 [laufende Nummer] [Jahr]
- **Beispiel**: 32.2 128 2024 (128. Antrag in 2024)
- **Eindeutigkeit**: Systemweit eindeutig
- **Automatische Generierung**: Beim Antragserstellunng

#### BR-002: Wartelisten-Ranking
- **Sortierung**: Bewerbungsdatum ASC (First-In-First-Out)
- **Gemarkungsspezifisch**: Separate Listen pro Wunschgemarkung
- **Neuberechnung**: Bei Status-Änderungen automatisch
- **Manuelle Korrektur**: Nur durch Administratoren mit Begründung

#### BR-003: Antragsgültigkeit
- **Standard-Laufzeit**: 12 Monate ab Bewerbungsdatum
- **Verlängerung**: Auf schriftlichen Antrag um 12 Monate
- **Maximum**: 3 Verlängerungen (insgesamt 4 Jahre)
- **Erinnerungen**: 30 und 14 Tage vor Ablauf
- **Automatische Archivierung**: Bei Ablauf ohne Verlängerung

### 5.2 Vergabeprozess

#### BR-004: Angebotsberechtigung
- **Rang-Voraussetzung**: Position 1 in Warteliste der Wunschgemarkung
- **Antragsstatus**: Muss 'Aktiv' und gültig sein
- **Verfügbarkeit**: Passende Parzelle muss verfügbar sein
- **Vorpächter-Zustimmung**: Falls erforderlich

#### BR-005: Angebotsfrist
- **Standard-Frist**: 4 Wochen ab Versendung
- **Verlängerung**: Auf Antrag um 2 Wochen möglich
- **Erinnerungen**: 1 Woche vor Ablauf
- **Automatische Ablehnung**: Bei Fristüberschreitung

### 5.3 Datenqualität

#### BR-006: Pflichtfelder Antragstellung
- **Person**: Nachname, Vorname, Geburtsdatum
- **Adresse**: Straße, PLZ, Ort
- **Kontakt**: E-Mail-Adresse (für Benachrichtigungen)
- **Gemarkung**: Mindestens eine Wunschgemarkung
- **Rechtliches**: Einverständnis Datenverarbeitung

#### BR-007: Validierungsregeln
- **E-Mail**: RFC-konforme Validierung
- **PLZ**: Deutsche PLZ-Format (5 Ziffern)
- **Telefon**: Optionale Normalisierung auf deutsches Format
- **Geburtsdatum**: Mindestalter 18 Jahre
- **Duplikatsprüfung**: Eindeutigkeit Name + Adresse + Gemarkung

## 6. Akzeptanzkriterien

### 6.1 Go-Live Kriterien
- [ ] Alle funktionalen Requirements implementiert und getestet
- [ ] Performance-Tests bestanden (100 concurrent users)
- [ ] Security-Audit ohne kritische Findings
- [ ] BITV 2.0 Konformität zertifiziert
- [ ] Vollständige Datenmigration validiert
- [ ] Benutzer-Schulungen durchgeführt
- [ ] 24/7 Support-Prozesse etabliert

### 6.2 Success Metrics (nach 6 Monaten)
- **Bearbeitungszeit**: -60% gegenüber Legacy-System
- **Bürgerzufriedenheit**: >80% positive Bewertungen
- **Fehlerrate**: <1% bei Datenerfassung
- **System-Verfügbarkeit**: >99% während Bürozeiten
- **Kosteneinsparung**: €40.000 jährlich nachweisbar

---

Diese Business Requirements bilden die Grundlage für die technische Umsetzung und dienen als Referenz für alle Entwicklungs- und Testaktivitäten des KGV-Modernisierungsprojekts.