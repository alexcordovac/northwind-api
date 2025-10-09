# AGENTS

## Project Snapshot
- Solution `NorthWind.API.sln` holds starter `.NET 8` minimal API plus empty `Domain`, `Application`, and `Infrastructure` class library projects.
- Web project `NorthWind.API` still exposes the template `WeatherForecast` endpoint and only references `ServiceDefaults`; no wiring to other layers exists yet.
- No persistence, mediator, validation, or CQRS scaffolding is in place; bin/obj directories contain build artifacts only.

## Architectural Targets
- **Minimal API composition**: group endpoints per feature using extension methods or modular registrars to keep Program.cs thin.
- **Domain-centric design**: encapsulate business invariants inside the `NorthWind.Domain` project using rich entities and value objects.
- **CQRS + Mediator (no packages)**: define internal `IRequest<TResponse>` / `IRequestHandler<TRequest, TResponse>` abstractions in `Application`, with mediator pipeline components (logging, validation, transaction) implemented manually.
- **Persistence**: place EF Core DbContext, migrations, repositories in `Infrastructure`; expose only interfaces to `Application`.
- **Result and validation flow**: standardize command/query outputs with `FluentResults` and plug `FluentValidation` validators into the mediator pipeline.
- **ProblemDetails**: configure consistent error responses (validation, domain, infrastructure) surfaced to API clients using RFC 7807 format.

## Recommended Solution Layout
- `NorthWind.Domain`: aggregates, value objects, domain services, domain events.
- `NorthWind.Application`: CQRS requests, handlers, mediator, pipelines, DTO mapping, contracts for infrastructure (repositories, unit of work).
- `NorthWind.Infrastructure`: EF Core context, entity configurations, migrations, repository/unit of work implementations, external integrations.
- `NorthWind.API`: dependency injection, minimal API endpoints, global middleware (ProblemDetails, exception handling), authentication/authorization, API contracts.
- `NorthWind.API.ServiceDefaults`: shared Observability/Resilience bootstrap (keep isolated from vertical slices).

## Implementation Backlog (initial)
1. **Clean Program.cs**: remove weather sample, introduce endpoint registration pattern (e.g., feature modules).
2. **Domain model kickoff**: model core entities (Customers, Orders, Products) and domain primitives.
3. **Mediator core**: implement `IMediator`, request/handler abstractions, and pipeline behaviors (validation, logging, transaction).
4. **CQRS slice**: add a sample query (e.g., `GetCustomerById`) demonstrating handler + validator + endpoint + FluentResults.
5. **Persistence**: configure EF Core DbContext, entity type configurations, DbContext factory, migrations strategy.
6. **Validation & ProblemDetails**: wire `FluentValidation` into mediator pipeline; map validation/domain errors to `ProblemDetails`.
7. **Results & mapping**: adopt `FluentResults` for output and create helper extensions translating to HTTP responses.
8. **Testing setup**: establish unit tests for domain and application layers, and integration tests leveraging `WebApplicationFactory`.

## Working Agreements
- Favor vertical slices that flow Domain → Application → Infrastructure → API without circular references.
- Keep Program.cs focused on composition; business logic lives in handlers/services.
- Surface failures as `FluentResults` errors, convert centrally to ProblemDetails for HTTP responses.
- Add XML documentation or concise comments only where intent is non-obvious (complex pipelines, cross-cutting helpers).
- Enforce async end-to-end, cancellation tokens, and guard clauses for input validation.
