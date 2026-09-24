using MediatR;

namespace ProductionStarter.Features.Orders.Commands;

public record CreateOrderCommand(string CustomerId, decimal TotalAmount, List<OrderItemDto> Items) : IRequest<Guid>;
