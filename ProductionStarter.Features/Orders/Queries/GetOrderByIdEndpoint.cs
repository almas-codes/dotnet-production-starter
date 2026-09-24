using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ProductionStarter.Features.Orders.Queries;

public static class GetOrderByIdEndpoint
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/orders/{id:guid}", async (Guid id, IMediator mediator, CancellationToken ct) =>
        {
            var result = await mediator.Send(new GetOrderByIdQuery(id), ct);
            return result is not null ? Results.Ok(result) : Results.NotFound();
        })
        .WithName("GetOrderById")
        .WithTags("Orders")
        .Produces<OrderResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
