using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ProductionStarter.Features.Orders.Commands;

public static class CreateOrderEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/orders", async (CreateOrderCommand command, IMediator mediator, CancellationToken ct) =>
        {
            var orderId = await mediator.Send(command, ct);
            return Results.Created($"/api/orders/{orderId}", new { Id = orderId });
        })
        .WithName("CreateOrder")
        .WithTags("Orders")
        .Produces<object>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
