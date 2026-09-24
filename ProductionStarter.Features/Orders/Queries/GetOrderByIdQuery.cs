using MediatR;

namespace ProductionStarter.Features.Orders.Queries;

public record GetOrderByIdQuery(Guid Id) : IRequest<OrderResponse?>;
