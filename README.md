# Vehicle Management API

A REST API for managing a car dealership's vehicle inventory, with support for multiple dealership locations.

## Tech Stack

- **Language:** C#
- **Framework:** .NET 10 / ASP.NET Core Web API
- **Database:** PostgreSQL
- **ORM:** Entity Framework Core (Npgsql)
- **API Documentation:** OpenAPI / Swagger UI (Swashbuckle)
- **Logging:** Serilog
- **Testing:** xUnit

## Architecture

The solution follows a four-project Clean Architecture structure:

- **VehicleManagement.Domain** — Domain entities and business rules
- **VehicleManagement.Application** — Application services, DTOs, and repository interfaces
- **VehicleManagement.Infrastructure** — Database access and repository implementations
- **VehicleManagement.Api** — HTTP endpoints and application configuration

## Status

The project is currently in the initial setup phase. API endpoints, database integration, and tests will be implemented in subsequent steps.