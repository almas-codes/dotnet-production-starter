# Production-Ready .NET Architecture Starter

Start a production-grade ASP.NET Core application in 5 minutes.

Not another "Clean Architecture demo." This is a serious template designed for scale and maintainability using **Vertical Slice Architecture**.

## Features Included

- **ASP.NET Core 10**
- **Vertical Slice Architecture:** Grouped by Feature, not by technical concern.
- **CQRS & MediatR:** Commands and Queries separated for performance and readability.
- **EF Core & PostgreSQL:** Reliable data writing.
- **Dapper:** High-performance data reading.
- **Outbox Pattern:** Transactional messaging to prevent data loss.
- **OpenTelemetry:** Traces, metrics, and logs built in.
- **Global Error Handling:** Native ProblemDetails middleware.
- **Testing:** Integration tests configured against real databases.

## Getting Started

1. Clone this repository.
2. Update the connection string in `appsettings.json`.
3. Run `dotnet run --project ProductionStarter.Api`.

## Architecture Overview

Instead of hunting through `Core`, `Application`, and `Infrastructure` layers, you will find self-contained Vertical Slices in `src/Features`.

Example: `Features/Orders`
- `CreateOrderCommand.cs`
- `CreateOrderCommandHandler.cs` (Uses EF Core to write)
- `CreateOrderEndpoint.cs`
- `GetOrderByIdQuery.cs`
- `GetOrderByIdQueryHandler.cs` (Uses Dapper to read)
- `GetOrderByIdEndpoint.cs`

---
*Tags: ASP.NET Core, Vertical Slice, CQRS, DDD, EF Core, Dapper, PostgreSQL, Redis, Outbox Pattern, Idempotency, OpenTelemetry, ProblemDetails.*
