# 🛠️ KGV-v2 Development Setup Guide

## 🎯 Übersicht

Diese Anleitung führt Sie durch die komplette Einrichtung der lokalen Entwicklungsumgebung für das KGV-v2 Projekt mit Frontend (Angular) und Backend (.NET 9).

---

## 📋 Systemanforderungen

### **Minimum Anforderungen**

- **OS**: Windows 10/11, macOS 12+, Ubuntu 20.04+
- **RAM**: 8 GB (16 GB empfohlen)
- **Speicher**: 10 GB freier Speicherplatz
- **Internet**: Breitbandverbindung für Package-Downloads

### **Empfohlene Hardware**

- **CPU**: Intel i5/AMD Ryzen 5 oder besser
- **RAM**: 16 GB oder mehr
- **SSD**: NVMe SSD für bessere Performance
- **Monitor**: 1920x1080 oder höher (für IDE-Komfort)

---

## 🔧 Software-Installation

### **1. Grundlegende Entwicklungstools**

#### **Git (Version Control)**

```bash
# Windows (mit Chocolatey)
choco install git

# macOS (mit Homebrew)
brew install git

# Ubuntu
sudo apt update && sudo apt install git

# Konfiguration
git config --global user.name "Ihr Name"
git config --global user.email "ihre.email@example.com"
```

#### **Node.js & npm (Frontend)**

```bash
# Version 18.x oder höher erforderlich
# Download: https://nodejs.org/

# Überprüfung
node --version  # v18.x.x oder höher
npm --version   # 9.x.x oder höher

# Optional: Yarn als Alternative zu npm
npm install -g yarn
```

#### **Angular CLI (Frontend)**

```bash
# Global Installation
npm install -g @angular/cli@latest

# Überprüfung
ng version  # Angular CLI 17.x.x oder höher
```

### **2. Backend-Entwicklungstools**

#### **.NET 9 SDK**

```bash
# Download: https://dotnet.microsoft.com/download/dotnet/9.0

# Überprüfung
dotnet --version  # 9.0.x

# Zusätzliche Tools installieren
dotnet tool install --global dotnet-ef  # Entity Framework CLI
dotnet tool install --global dotnet-aspnet-codegenerator  # Scaffolding
```

#### **SQL Server (Database)**

**Option A: SQL Server LocalDB (Windows)**

```bash
# Download SQL Server Express
# https://www.microsoft.com/sql-server/sql-server-downloads

# LocalDB installieren
SqlLocalDB create MSSQLLocalDB
SqlLocalDB start MSSQLLocalDB
```

**Option B: Docker SQL Server (Cross-Platform)**

```bash
# SQL Server Container starten
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourStrong@Passw0rd" \
  -p 1433:1433 --name kgv-sqlserver \
  -d mcr.microsoft.com/mssql/server:2022-latest
```

**Option C: PostgreSQL (Alternative)**

```bash
# PostgreSQL mit Docker
docker run --name kgv-postgres -e POSTGRES_PASSWORD=yourpassword \
  -e POSTGRES_DB=kgv -p 5432:5432 -d postgres:15
```

### **3. IDE-Installation**

#### **Visual Studio Code (Empfohlen für Frontend)**

```bash
# Download: https://code.visualstudio.com/

# Empfohlene Extensions installieren
code --install-extension angular.ng-template
code --install-extension ms-vscode.vscode-typescript-next
code --install-extension bradlc.vscode-tailwindcss
code --install-extension esbenp.prettier-vscode
code --install-extension ms-vscode.vscode-eslint
code --install-extension ms-dotnettools.csharp
```

#### **Visual Studio 2022 (Empfohlen für Backend)**

```bash
# Download: https://visualstudio.microsoft.com/

# Erforderliche Workloads:
# - ASP.NET and web development
# - .NET desktop development
# - Data storage and processing
```

#### **JetBrains Rider (Premium Alternative)**

```bash
# Download: https://www.jetbrains.com/rider/
# Unterstützt sowohl .NET als auch Angular
```

---

## 📁 Projekt Setup

### **1. Repository klonen**

```bash
# Repository klonen
git clone https://github.com/andrekirst/kgv-v2.git
cd kgv-v2

# Domain Protection Hooks installieren
chmod +x .domain-guard/install-hooks.sh
./.domain-guard/install-hooks.sh
```

### **2. Projektstruktur erstellen**

```bash
# Grundstruktur anlegen
mkdir -p src/{frontend,backend}
mkdir -p tests/{frontend,backend}
mkdir -p tools
```

---

## 🌐 Backend Setup (`src/backend/`)

### **1. .NET Solution erstellen**

```bash
cd src/backend

# Solution und Projekte erstellen
dotnet new sln -n KGV

# Domain Layer
dotnet new classlib -n KGV.Domain
dotnet sln add KGV.Domain/KGV.Domain.csproj

# Application Layer
dotnet new classlib -n KGV.Application
dotnet sln add KGV.Application/KGV.Application.csproj

# Infrastructure Layer
dotnet new classlib -n KGV.Infrastructure
dotnet sln add KGV.Infrastructure/KGV.Infrastructure.csproj

# Web API
dotnet new webapi -n KGV.WebApi
dotnet sln add KGV.WebApi/KGV.WebApi.csproj

# Shared Library
dotnet new classlib -n KGV.Shared
dotnet sln add KGV.Shared/KGV.Shared.csproj
```

### **2. Project References konfigurieren**

```bash
# Application → Domain, Shared
dotnet add KGV.Application/KGV.Application.csproj reference KGV.Domain/KGV.Domain.csproj
dotnet add KGV.Application/KGV.Application.csproj reference KGV.Shared/KGV.Shared.csproj

# Infrastructure → Domain, Application
dotnet add KGV.Infrastructure/KGV.Infrastructure.csproj reference KGV.Domain/KGV.Domain.csproj
dotnet add KGV.Infrastructure/KGV.Infrastructure.csproj reference KGV.Application/KGV.Application.csproj

# WebApi → Application, Infrastructure
dotnet add KGV.WebApi/KGV.WebApi.csproj reference KGV.Application/KGV.Application.csproj
dotnet add KGV.WebApi/KGV.WebApi.csproj reference KGV.Infrastructure/KGV.Infrastructure.csproj
```

### **3. NuGet Packages installieren**

```bash
# Domain Layer (minimal dependencies)
dotnet add KGV.Domain package Microsoft.Extensions.DependencyInjection.Abstractions

# Application Layer
dotnet add KGV.Application package MediatR
dotnet add KGV.Application package FluentValidation
dotnet add KGV.Application package AutoMapper
dotnet add KGV.Application package Microsoft.Extensions.Logging.Abstractions

# Infrastructure Layer
dotnet add KGV.Infrastructure package Microsoft.EntityFrameworkCore.SqlServer
dotnet add KGV.Infrastructure package Microsoft.EntityFrameworkCore.Tools
dotnet add KGV.Infrastructure package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add KGV.Infrastructure package Microsoft.Extensions.Configuration

# Web API
dotnet add KGV.WebApi package Microsoft.AspNetCore.Authentication.JwtBearer
dotnet add KGV.WebApi package Swashbuckle.AspNetCore
dotnet add KGV.WebApi package Microsoft.AspNetCore.Cors
dotnet add KGV.WebApi package Serilog.AspNetCore
```

### **4. Database Setup**

```bash
# Connection String konfigurieren (appsettings.json)
cat > KGV.WebApi/appsettings.Development.json << 'EOF'
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=KgvDatabase;Trusted_Connection=true;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}
EOF

# Erste Migration erstellen (nach DbContext Implementation)
dotnet ef migrations add InitialCreate -p KGV.Infrastructure -s KGV.WebApi
dotnet ef database update -p KGV.Infrastructure -s KGV.WebApi
```

### **5. Backend Tests Setup (in derselben Solution)**

```bash
# Bleiben im Backend-Verzeichnis
cd src/backend

# Test Projekte erstellen (werden automatisch zur Solution hinzugefügt)
dotnet new xunit -n KGV.Domain.Tests
dotnet sln add KGV.Domain.Tests/KGV.Domain.Tests.csproj

dotnet new xunit -n KGV.Application.Tests
dotnet sln add KGV.Application.Tests/KGV.Application.Tests.csproj

dotnet new xunit -n KGV.Infrastructure.Tests
dotnet sln add KGV.Infrastructure.Tests/KGV.Infrastructure.Tests.csproj

dotnet new xunit -n KGV.WebApi.Tests
dotnet sln add KGV.WebApi.Tests/KGV.WebApi.Tests.csproj

# Test Project References konfigurieren
dotnet add KGV.Domain.Tests/KGV.Domain.Tests.csproj reference KGV.Domain/KGV.Domain.csproj
dotnet add KGV.Application.Tests/KGV.Application.Tests.csproj reference KGV.Application/KGV.Application.csproj
dotnet add KGV.Infrastructure.Tests/KGV.Infrastructure.Tests.csproj reference KGV.Infrastructure/KGV.Infrastructure.csproj
dotnet add KGV.WebApi.Tests/KGV.WebApi.Tests.csproj reference KGV.WebApi/KGV.WebApi.csproj

# Test Packages installieren
dotnet add KGV.Domain.Tests package FluentAssertions
dotnet add KGV.Domain.Tests package Moq
dotnet add KGV.Application.Tests package Microsoft.EntityFrameworkCore.InMemory
dotnet add KGV.Application.Tests package FluentAssertions
dotnet add KGV.Application.Tests package Moq
dotnet add KGV.Infrastructure.Tests package Microsoft.EntityFrameworkCore.InMemory
dotnet add KGV.Infrastructure.Tests package FluentAssertions
dotnet add KGV.WebApi.Tests package Microsoft.AspNetCore.Mvc.Testing
dotnet add KGV.WebApi.Tests package FluentAssertions
```

---

## 🖥️ Frontend Setup (`src/frontend/`)

### **1. Angular Projekt erstellen**

```bash
cd src/frontend

# Angular Projekt mit Routing und SCSS
ng new . --routing=true --style=scss --skip-git=true --directory=.

# Antworten auf Prompts:
# ? Would you like to add Angular routing? Yes
# ? Which stylesheet format would you like to use? SCSS
```

### **2. Angular Material installieren**

```bash
# Angular Material mit Theme
ng add @angular/material

# Theme auswählen: Indigo/Pink oder Custom
# HammerJS für Gestures: Yes
# Browser-Animationen: Yes
```

### **3. Zusätzliche Frontend Packages**

```bash
# State Management (optional)
ng add @ngrx/store @ngrx/effects @ngrx/store-devtools

# HTTP Client und Forms (bereits in Angular enthalten)
# Utility Libraries
npm install date-fns lodash-es
npm install -D @types/lodash-es

# Testing Libraries
npm install -D @testing-library/angular
npm install -D jest @types/jest
npm install -D @playwright/test

# Code Quality
npm install -D eslint @typescript-eslint/eslint-plugin
npm install -D prettier
npm install -D husky lint-staged
```

### **4. Frontend Project Structure Setup**

```bash
# Feature Module Struktur erstellen
mkdir -p src/app/{core,shared,features,layout}
mkdir -p src/app/core/{guards,interceptors,services,models,constants}
mkdir -p src/app/shared/{components,directives,pipes,validators,utils}
mkdir -p src/app/features/{auth,applications,waiting-list,admin,public}
mkdir -p src/app/layout/{header,sidebar,footer,main-layout,breadcrumb}

# Assets Struktur
mkdir -p src/assets/{images,fonts,styles,i18n}
mkdir -p src/assets/images/{logo,icons,backgrounds}
mkdir -p src/assets/styles
```

### **5. Configuration Files erstellen**

#### **TypeScript Configuration**

```json
// tsconfig.json Pfad-Mappings hinzufügen
{
  "compilerOptions": {
    "baseUrl": "./",
    "paths": {
      "@core/*": ["src/app/core/*"],
      "@shared/*": ["src/app/shared/*"],
      "@features/*": ["src/app/features/*"],
      "@layout/*": ["src/app/layout/*"],
      "@assets/*": ["src/assets/*"],
      "@environments/*": ["src/environments/*"]
    }
  }
}
```

#### **Environment Configuration**

```typescript
// src/environments/environment.ts
export const environment = {
  production: false,
  apiUrl: 'https://localhost:7001/api',
  apiTimeout: 30000,
  enableDevTools: true,
  logLevel: 'debug'
};

// src/environments/environment.prod.ts
export const environment = {
  production: true,
  apiUrl: 'https://api.kgv-system.de/api',
  apiTimeout: 10000,
  enableDevTools: false,
  logLevel: 'error'
};
```

---

## 🔧 Entwicklungstools Konfiguration

### **1. EditorConfig erstellen**

```bash
# Root EditorConfig für konsistente Formatierung
cat > .editorconfig << 'EOF'
# EditorConfig is awesome: https://EditorConfig.org

root = true

[*]
indent_style = space
indent_size = 2
end_of_line = lf
charset = utf-8
trim_trailing_whitespace = true
insert_final_newline = true

[*.{cs,csx}]
indent_size = 4

[*.md]
trim_trailing_whitespace = false

[*.{yml,yaml}]
indent_size = 2

[Makefile]
indent_style = tab
EOF
```

### **2. Git Hooks Setup**

```bash
# Husky für Git Hooks (Frontend)
cd src/frontend
npx husky-init && npm install
echo "npm run lint && npm run test -- --passWithNoTests" > .husky/pre-commit

# Package.json Scripts erweitern
npm pkg set scripts.lint="ng lint"
npm pkg set scripts.format="prettier --write \"src/**/*.{ts,html,scss}\""
npm pkg set scripts.lint:fix="ng lint --fix"
```

### **3. VSCode Workspace Setup**

```json
// .vscode/settings.json
{
  "typescript.preferences.importModuleSpecifier": "relative",
  "editor.formatOnSave": true,
  "editor.defaultFormatter": "esbenp.prettier-vscode",
  "files.exclude": {
    "**/node_modules": true,
    "**/dist": true,
    "**/bin": true,
    "**/obj": true
  },
  "omnisharp.enableRoslynAnalyzers": true,
  "dotnet.defaultSolution": "src/backend/KGV.sln"
}

// .vscode/launch.json
{
  "version": "0.2.0",
  "configurations": [
    {
      "name": ".NET Core Launch (web)",
      "type": "coreclr",
      "request": "launch",
      "preLaunchTask": "build",
      "program": "${workspaceFolder}/src/backend/KGV.WebApi/bin/Debug/net9.0/KGV.WebApi.dll",
      "args": [],
      "cwd": "${workspaceFolder}/src/backend/KGV.WebApi",
      "stopAtEntry": false,
      "serverReadyAction": {
        "action": "openExternally",
        "pattern": "\\bNow listening on:\\s+(https?://\\S+)"
      },
      "env": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    {
      "name": "Angular Serve",
      "type": "node",
      "request": "launch",
      "cwd": "${workspaceFolder}/src/frontend",
      "program": "${workspaceFolder}/src/frontend/node_modules/@angular/cli/bin/ng",
      "args": ["serve", "--port", "4200"],
      "console": "integratedTerminal"
    }
  ]
}
```

---

## 🚀 Lokale Entwicklung starten

### **1. Backend starten**

```bash
cd src/backend

# Restore packages
dotnet restore

# Database migrations ausführen
dotnet ef database update -p KGV.Infrastructure -s KGV.WebApi

# API starten (Development)
dotnet run --project KGV.WebApi

# Alternatív: Mit Hot Reload
dotnet watch run --project KGV.WebApi
```

### **2. Frontend starten**

```bash
cd src/frontend

# Dependencies installieren
npm install

# Development Server starten
ng serve

# Mit spezifischem Port
ng serve --port 4200 --open

# Mit Proxy für Backend API
ng serve --proxy-config proxy.conf.json
```

### **3. Docker Development (Optional)**

```bash
# Docker Compose für lokale Services
cat > docker-compose.dev.yml << 'EOF'
version: '3.8'
services:
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    environment:
      - ACCEPT_EULA=Y
      - MSSQL_SA_PASSWORD=DevPassword123!
    ports:
      - "1433:1433"
    volumes:
      - sqlserver_data:/var/opt/mssql

  redis:
    image: redis:7-alpine
    ports:
      - "6379:6379"
    volumes:
      - redis_data:/data

volumes:
  sqlserver_data:
  redis_data:
EOF

# Services starten
docker-compose -f docker-compose.dev.yml up -d
```

---

## 🧪 Testing Setup

### **Backend Tests ausführen**

```bash
cd src/backend

# Alle Tests in der Solution ausführen
dotnet test

# Mit Code Coverage
dotnet test --collect:"XPlat Code Coverage"

# Spezifisches Test-Projekt ausführen
dotnet test KGV.Domain.Tests
dotnet test KGV.Application.Tests
dotnet test KGV.Infrastructure.Tests
dotnet test KGV.WebApi.Tests

# Tests parallel ausführen
dotnet test --parallel
```

### **Frontend Tests ausführen**

```bash
cd src/frontend

# Unit Tests (Jest) - konfiguriert für tests/ Verzeichnis
npm run test

# E2E Tests (Playwright) - aus Root-Verzeichnis
cd ../../
npx playwright test

# Test Coverage für Frontend
cd src/frontend
npm run test -- --code-coverage
```

---

## 🔍 Debugging Setup

### **Backend Debugging**

```bash
# Launch Profile für verschiedene Environments
# Properties/launchSettings.json wird automatisch erstellt

# Debugging mit dotnet CLI
dotnet run --project KGV.WebApi --launch-profile Development
```

### **Frontend Debugging**

```typescript
// Angular DevTools Extension installieren
// Chrome: Angular DevTools
// Firefox: Angular DevTools

// Console Debugging
console.log('Debug info:', data);

// Angular Debug Service
import { isDevMode } from '@angular/core';

if (isDevMode()) {
  console.log('Development mode debugging');
}
```

---

## 📊 Performance Monitoring

### **Backend Monitoring**

```csharp
// Application Insights (optional)
// dotnet add KGV.WebApi package Microsoft.ApplicationInsights.AspNetCore

// Serilog Structured Logging
// dotnet add KGV.WebApi package Serilog.Extensions.Hosting
// dotnet add KGV.WebApi package Serilog.Sinks.Console
```

### **Frontend Monitoring**

```bash
# Bundle Analyzer für Angular
ng build --stats-json
npx webpack-bundle-analyzer dist/stats.json

# Lighthouse für Performance
npm install -g @lhci/cli
lhci autorun
```

---

## 🚨 Troubleshooting

### **Häufige Backend-Probleme**

#### **1. Database Connection Issues**

```bash
# LocalDB Status prüfen
SqlLocalDB info MSSQLLocalDB

# Connection String testen
dotnet ef dbcontext info -p KGV.Infrastructure -s KGV.WebApi
```

#### **2. Package Restore Issues**

```bash
# NuGet Cache leeren
dotnet nuget locals all --clear

# Packages neu installieren
dotnet restore --force
```

### **Häufige Frontend-Probleme**

#### **1. Node/NPM Version Issues**

```bash
# Node Version prüfen
node --version
npm --version

# Node Version Management
# Windows: nvm-windows
# macOS/Linux: nvm

nvm install 18
nvm use 18
```

#### **2. Package Installation Issues**

```bash
# Node modules löschen und neu installieren
rm -rf node_modules package-lock.json
npm install

# Alternative: Yarn verwenden
npm install -g yarn
yarn install
```

### **3. Port Konflikte**

```bash
# Backend Port ändern (launchSettings.json)
"applicationUrl": "https://localhost:7001;http://localhost:5000"

# Frontend Port ändern
ng serve --port 4201
```

---

## ✅ Setup Validierung

### **1. Backend Validierung**

```bash
# API Health Check
curl https://localhost:7001/health

# Swagger UI öffnen
# https://localhost:7001/swagger

# Database Test
dotnet ef migrations list -p KGV.Infrastructure -s KGV.WebApi
```

### **2. Frontend Validierung**

```bash
# Angular Build Test
ng build

# Linting prüfen
ng lint

# Tests ausführen
npm run test -- --watch=false
```

### **3. Integration Test**

```bash
# Backend und Frontend gleichzeitig starten
# Terminal 1: cd src/backend && dotnet run --project KGV.WebApi
# Terminal 2: cd src/frontend && ng serve

# Browser öffnen: http://localhost:4200
# API Test: http://localhost:7001/swagger
```

---

## 🎯 Nächste Schritte

Nach erfolgreichem Setup können Sie mit der Entwicklung beginnen:

1. **Issue #4**: Angular Foundation implementieren
2. **Issue #1**: Domain Model implementieren  
3. **Issue #2**: Entity Framework Setup
4. **Issue #3**: JWT Authentication

### **Empfohlene Entwicklungsreihenfolge:**

1. Backend Foundation (Issues #1, #2)
2. Frontend Foundation (Issue #4)
3. Authentication (Issue #3)
4. Feature Development (Milestone 1 Issues)

---

**🚀 Ihre Entwicklungsumgebung ist jetzt bereit für die KGV-v2 Entwicklung!**
