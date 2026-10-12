# Vehicle Management API

Eine REST-API zur Verwaltung des Fahrzeugbestands eines Autohausunternehmens mit mehreren Standorten.

## Verwendete Technologien

- **Programmiersprache:** C#
- **Framework:** .NET 10 / ASP.NET Core Web API
- **Datenbank:** PostgreSQL
- **ORM:** Entity Framework Core (Npgsql)
- **API-Dokumentation:** OpenAPI / Swagger UI (Swashbuckle)
- **Logging:** Serilog
- **Tests:** xUnit

## Voraussetzungen und Einrichtung

### Voraussetzungen

Für die lokale Ausführung werden folgende Werkzeuge benötigt:

- **.NET SDK 10**
- **Docker** mit Docker Compose
- **Git**
- **EF Core CLI (dotnet-ef)** zur Ausführung der Datenbankmigrationen



### Projekt einrichten

**1. Repository klonen**

```bash
git clone https://github.com/janosch-pueschel/vehicle-management.git
cd vehicle-management
```

**2. Abhängigkeiten wiederherstellen**

Die benötigten NuGet-Pakete werden mit folgendem Befehl wiederhergestellt:

```bash
dotnet restore
```

**3. PostgreSQL starten**

Die PostgreSQL-Datenbank wird über Docker Compose bereitgestellt:

```bash
docker compose up -d
```

Der Host-Port wurde gegenüber der ursprünglichen Vorlage von 5432 auf 5434 geändert. Der entsprechende Connection String ist in der appsettings.Development.json hinterlegt. Die Datenbankzugangsdaten sind ausschließlich für die lokale Entwicklungsumgebung vorgesehen und entsprechen der angepassten Docker-Compose-Konfiguration. In einer produktiven Umgebung würden Zugangsdaten über eine separate Secret-Verwaltung bereitgestellt.

**4. Datenbankmigrationen anwenden**

Die Datenbankstruktur wird mithilfe von Entity Framework Core erstellt bzw. aktualisiert:

```bash
# Falls die EF Core CLI noch nicht installiert ist:
dotnet tool install --global dotnet-ef --version "10.*"
```

```bash
dotnet ef database update --project VehicleManagement.Infrastructure --startup-project VehicleManagement.Api
```

**5. API starten**

```bash
dotnet run --project VehicleManagement.Api --environment Development
```

Die API kann anschließend über die in der Konsolenausgabe angegebene Adresse aufgerufen werden. Die verfügbaren Endpunkte lassen sich in der Entwicklungsumgebung über die Swagger UI unter /swagger erkunden.

### Tests ausführen

Die Unit-Tests können im Stammverzeichnis der Solution ausgeführt werden:

```bash
dotnet test
```



## Technische Entscheidungen



### Anwendungsszenario und Domänenmodell

Ich habe die Aufgabenstellung als internes Bestandsverwaltungssystem für ein einzelnes Autohausunternehmen interpretiert, das über mehrere Standorte verfügen kann. Ziel ist es, den Fahrzeugbestand standortübergreifend zu verwalten und Fahrzeuge den jeweiligen Autohäusern zuzuordnen.

Daraus ergeben sich die beiden Entitäten `Dealership` und `Vehicle`. Ein `Dealership` repräsentiert einen Autohausstandort, dem mehrere Fahrzeuge zugeordnet sein können. Jedes Fahrzeug gehört genau zu einem Standort. Beide Entitäten werden als eigenständige API-Ressourcen bereitgestellt.

Eine Erweiterung für mehrere voneinander unabhängige Händlerunternehmen wäre perspektivisch denkbar, würde allerdings unter anderem eine Mandantentrennung sowie Authentifizierung und Autorisierung erfordern.

### Architektur

Die Solution folgt der vorgegebenen Clean Architecture und besteht aus vier Projekten:

- **Domain:** Enthält die Domänenentitäten und hat keine Abhängigkeiten zu den anderen Projekten.
- **Application:** Enthält die Anwendungslogik, DTOs und Repository-Schnittstellen.
- **Infrastructure:** Implementiert die Repositories und übernimmt den Datenbankzugriff mit Entity Framework Core.
- **Api:** Stellt die HTTP-Endpunkte bereit und übernimmt die Konfiguration der Anwendung.

Die Trennung von Repository-Schnittstellen und Implementierungen ermöglicht es, die Anwendungslogik unabhängig von der Datenbank zu testen.

### Implementierungsentscheidungen

Für die Umwandlung zwischen Entitäten und DTOs habe ich mich für manuelles Mapping statt AutoMapper entschieden. Bei der überschaubaren Anzahl an Modellen bleibt die Zuordnung dadurch explizit und nachvollziehbar, ohne eine zusätzliche Bibliothek einzuführen.

Für die Aktualisierung von Fahrzeugen verwende ich `PUT` statt `PATCH`, da sämtliche bearbeitbaren Eigenschaften übergeben werden und eine partielle Aktualisierung für den aktuellen Funktionsumfang nicht erforderlich ist.

### Bewusste Vereinfachungen

- **Validierung:** Die Eingabevalidierung erfolgt überwiegend über DataAnnotations in den DTOs. Fachliche Prüfungen, beispielsweise die Existenz eines Autohausstandorts, finden in der Application-Schicht statt. Bei komplexeren Anforderungen würde ich die Validierungslogik stärker dort bündeln.
- **Fehlerbehandlung:** Erwartbare Fehlerfälle werden in der Application-Schicht berücksichtigt und von den Controllern in passende HTTP-Statuscodes übersetzt. Für eine produktive Anwendung würde ich zusätzlich eine zentrale Behandlung unerwarteter Fehler und einheitliche Fehlerantworten vorsehen.
- **Git-Workflow:** Aufgrund des überschaubaren Projektumfangs habe ich direkt auf dem Hauptbranch gearbeitet. In einem Teamprojekt würde ich Feature-Branches, Pull Requests und Code-Reviews einsetzen.



## Einsatz von KI

Bei der Umsetzung der Probeaufgabe habe ich ChatGPT und Cursor als unterstützende Werkzeuge eingesetzt.

Da ich bisher hauptsächlich mit TypeScript im Frontend gearbeitet habe, war KI für mich insbesondere ein Lernpartner, um mich in der für mich neuen Entwicklungsumgebung mit C#, ASP.NET Core und Entity Framework Core zurechtzufinden. Dabei habe ich KI genutzt, um technische Konzepte zu verstehen, Zusammenhänge nachzuvollziehen und Fragen zur Architektur und Implementierung zu klären.

Darüber hinaus habe ich KI bei der Fehleranalyse, der Überprüfung von Code und Tests sowie bei der Erstellung der Dokumentation eingesetzt. Insbesondere die XML-Kommentare für die Swagger-Dokumentation wurden weitgehend mithilfe von KI generiert.

Die Umsetzung habe ich schrittweise erarbeitet und KI-Vorschläge kritisch geprüft und bei Bedarf angepasst. Mein Ziel war es, nicht nur eine funktionierende Lösung zu entwickeln, sondern auch die zugrunde liegenden Konzepte und Entscheidungen zu verstehen.