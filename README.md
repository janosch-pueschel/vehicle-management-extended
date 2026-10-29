# Vehicle Management API

Eine REST-API zur Verwaltung des Fahrzeugbestands eines Autohausunternehmens mit mehreren Standorten.

Das Projekt dient der praktischen Vertiefung meiner Kenntnisse in der Backend-Entwicklung mit C#, ASP.NET Core und Entity Framework Core. Im Mittelpunkt stehen die Entwicklung einer RESTful API, die Anbindung einer relationalen Datenbank sowie die Strukturierung der Anwendung nach Clean-Architecture-Prinzipien.

## Tech-Stack

| Bereich | Technologie |
|---|---|
| Sprache | C# |
| Framework | .NET 10 / ASP.NET Core Web API |
| Datenbank | PostgreSQL |
| ORM | Entity Framework Core (Npgsql) |
| API-Dokumentation | OpenAPI / Swagger UI (Swashbuckle) |
| Logging | Serilog |
| Tests | xUnit |
| Entwicklungsumgebung | Docker Compose |

## Funktionen

- Verwaltung von Fahrzeugen und Autohausstandorten über REST-Endpunkte
- CRUD-Operationen für beide Ressourcen
- Zuordnung von Fahrzeugen zu Autohausstandorten
- Datenbankzugriff mit Entity Framework Core
- Datenbankmigrationen und initiale Testdaten
- API-Dokumentation über Swagger UI
- Strukturiertes Logging mit Serilog
- Automatisierte Tests mit xUnit

## Architektur

Die Anwendung ist nach Clean-Architecture-Prinzipien in vier Projekte aufgeteilt:

- **Domain:** Domänenentitäten und fachliche Modelle ohne Abhängigkeiten zu anderen Projekten.
- **Application:** Anwendungslogik, DTOs und Repository-Schnittstellen.
- **Infrastructure:** Repository-Implementierungen und Datenbankzugriff mit Entity Framework Core.
- **Api:** HTTP-Endpunkte, Dependency Injection und Anwendungskonfiguration.

Die Trennung der Verantwortlichkeiten soll die Wartbarkeit und Testbarkeit der Anwendung verbessern. Insbesondere ermöglicht die Abstraktion des Datenbankzugriffs über Repository-Schnittstellen, die Anwendungslogik unabhängig von der konkreten Datenbankimplementierung zu testen.

## Domänenmodell

Die Anwendung bildet ein internes Bestandsverwaltungssystem für ein Autohausunternehmen mit mehreren Standorten ab.

Dafür werden zwei Entitäten verwendet:

- **Dealership:** Repräsentiert einen Autohausstandort, dem mehrere Fahrzeuge zugeordnet sein können.
- **Vehicle:** Repräsentiert ein Fahrzeug, das genau einem Autohausstandort zugeordnet ist.

Beide Entitäten werden als eigenständige API-Ressourcen bereitgestellt.

## Lokale Einrichtung

### Voraussetzungen

- .NET SDK 10
- Docker mit Docker Compose
- Git
- EF Core CLI (`dotnet-ef`)

### 1. Repository klonen

```bash
git clone https://github.com/janosch-pueschel/vehicle-management.git
cd vehicle-management
```

### 2. Abhängigkeiten installieren

```bash
dotnet restore
```

### 3. PostgreSQL starten

```bash
docker compose up -d
```

Die PostgreSQL-Datenbank wird über Docker Compose bereitgestellt. Der lokale Host-Port ist auf `5434` konfiguriert. Der entsprechende Connection String befindet sich in der `appsettings.Development.json`.

Die Datenbankzugangsdaten sind ausschließlich für die lokale Entwicklungsumgebung vorgesehen. In einer produktiven Umgebung sollten Zugangsdaten über eine geeignete Secret-Verwaltung bereitgestellt werden.

### 4. Datenbankmigrationen anwenden

Falls die EF Core CLI noch nicht installiert ist:

```bash
dotnet tool install --global dotnet-ef --version "10.*"
```

Anschließend:

```bash
dotnet ef database update --project VehicleManagement.Infrastructure --startup-project VehicleManagement.Api
```

### 5. API starten

```bash
dotnet run --project VehicleManagement.Api --environment Development
```

Die API ist anschließend unter der in der Konsolenausgabe angegebenen Adresse erreichbar.

Die Endpunkte können in der Entwicklungsumgebung über `/swagger` eingesehen und getestet werden.

### Tests ausführen

```bash
dotnet test
```

## Technische Entscheidungen

### Manuelles Mapping

Die Umwandlung zwischen Entitäten und DTOs erfolgt über manuelles Mapping anstelle einer zusätzlichen Mapping-Bibliothek.

Aufgrund der überschaubaren Anzahl an Modellen bleiben die Zuordnungen dadurch explizit und nachvollziehbar.

### Aktualisierung von Ressourcen

Für vollständige Aktualisierungen von Fahrzeugen wird `PUT` verwendet. Eine partielle Aktualisierung über `PATCH` ist im aktuellen Funktionsumfang nicht vorgesehen.

### Validierung

Die Eingabevalidierung erfolgt überwiegend über DataAnnotations in den DTOs. Fachliche Prüfungen, beispielsweise die Existenz eines Autohausstandorts, werden in der Application-Schicht durchgeführt.

Bei steigender Komplexität wäre eine stärkere Bündelung der Validierungslogik in der Anwendungsschicht sinnvoll.

### Fehlerbehandlung

Erwartbare Fehlerfälle werden in der Application-Schicht berücksichtigt und durch die Controller in passende HTTP-Statuscodes übersetzt.

Eine mögliche Weiterentwicklung wäre eine zentrale Fehlerbehandlung mit einheitlichen Fehlerantworten.

## Mögliche Erweiterungen

Das Projekt konzentriert sich bewusst auf die grundlegenden Funktionen einer Bestandsverwaltung.

Mögliche Erweiterungen sind:

- Authentifizierung und rollenbasierte Autorisierung
- Zentrale Fehlerbehandlung
- Erweiterte Validierung
- Filter- und Suchfunktionen
- Unterstützung mehrerer unabhängiger Autohausunternehmen mit Mandantentrennung
- Deployment in einer Cloud-Umgebung

## Hintergrund

Das Projekt entstand im Rahmen meiner praktischen Einarbeitung in C# und ASP.NET Core. Ziel war es, die Konzepte moderner Backend-Entwicklung anhand einer zusammenhängenden Anwendung kennenzulernen und eigenständig umzusetzen.