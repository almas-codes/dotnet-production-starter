# Production-Ready .NET Architecture Starter 🚀

Start a production-grade ASP.NET Core application in 5 minutes. 

I got tired of the usual "Clean Architecture" templates where I had to jump across 5 different projects (Domain, Application, Infrastructure, etc.) just to add a single database column or endpoint. So, I built this template using **Vertical Slice Architecture** combined with Minimal APIs. 

It scales incredibly well for large projects because things that change together, stay together.

## What's in the box?
- **.NET 10 Minimal APIs:** Super fast, minimal boilerplate.
- **Vertical Slice Architecture:** Code is grouped by *Feature* (e.g., `Orders`), not by technical layers.
- **CQRS via MediatR:** Commands (writes) and Queries (reads) are strictly separated.
- **PostgreSQL & EF Core:** Used for safe, transactional writes.
- **Dapper:** Used for blazing-fast reads directly from the database.
- **The Outbox Pattern:** Guarantees you never lose a domain event if a background process fails.
- **Global Error Handling:** Clean `ProblemDetails` responses out of the box.
- **OpenTelemetry & Serilog:** Built-in observability for traces, metrics, and logs.
- **Integration Tests:** End-to-end tests using real databases.

---

## 🛠️ How to Add a New Feature / Endpoint

One of the best things about Vertical Slice Architecture is how easy it is to add a new feature. You don't need to touch 10 files across the solution. Everything lives in one place.

Let's say you want to add a feature to **Create a Product**.

**Step 1: Create the Feature Folder**
Inside the `ProductionStarter.Features` project, create a new folder: `Features/Products/Commands/`.

**Step 2: Define your Command**
Create a new file `CreateProductCommand.cs`:
```csharp
public record CreateProductCommand(string Name, decimal Price) : IRequest<Guid>;
```

**Step 3: Handle the Logic**
Create `CreateProductCommandHandler.cs` to handle the database write using EF Core:
```csharp
public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
{
    private readonly AppDbContext _dbContext;

    public CreateProductCommandHandler(AppDbContext dbContext) { _dbContext = dbContext; }

    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken ct)
    {
        var product = new Product { Id = Guid.NewGuid(), Name = request.Name, Price = request.Price };
        _dbContext.Products.Add(product);
        await _dbContext.SaveChangesAsync(ct);
        return product.Id;
    }
}
```

**Step 4: Expose the Endpoint via Minimal API**
Create `CreateProductEndpoint.cs` and wire it up to MediatR:
```csharp
public static class CreateProductEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/products", async (CreateProductCommand command, IMediator mediator, CancellationToken ct) =>
        {
            var id = await mediator.Send(command, ct);
            return Results.Created($"/api/products/{id}", new { Id = id });
        })
        .WithTags("Products");
    }
}
```

**Step 5: Register the Endpoint**
Finally, open `ProductionStarter.Features/DependencyInjection.cs` and map your new endpoint inside `MapFeatureEndpoints()`:
```csharp
CreateProductEndpoint.MapEndpoint(app);
```
That's it! You've just added a fully functioning, production-ready endpoint in one folder.

---

## Getting Started

1. Clone this repository.
2. Update the connection string in `ProductionStarter.Api/appsettings.json` to point to your local PostgreSQL instance.
3. Run the API:
```bash
dotnet run --project ProductionStarter.Api
```
4. The database tables will be created automatically on startup, and you can hit the `/api/orders` endpoints immediately!

## Contributing
If you've got ideas on how to make this template even better, feel free to open a PR! 

---

### SEO & Keywords
*ASP.NET Core 10 Web API Template, Vertical Slice Architecture C#, CQRS MediatR Example .NET, Dapper vs EF Core CQRS, Open Source .NET Boilerplate, Production Ready .NET Microservices, PostgreSQL EF Core Outbox Pattern, Minimal API Best Practices, OpenTelemetry C# Setup, High Performance API .NET 10, Enterprise .NET Architecture Github.*
