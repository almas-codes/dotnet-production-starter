using MediatR;
using ProductionStarter.Core.Entities;
using ProductionStarter.Core.Events;
using ProductionStarter.Infrastructure.Data;
using System.Text.Json;

namespace ProductionStarter.Features.Orders.Commands;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly AppDbContext _dbContext;

    public CreateOrderCommandHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = request.CustomerId,
            CreatedAt = DateTime.UtcNow,
            TotalAmount = request.TotalAmount,
            Status = "Created",
            TenantId = "Default", // In a real app, this comes from a current tenant service
            Items = request.Items.Select(i => new OrderItem
            {
                Id = Guid.NewGuid(),
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice
            }).ToList()
        };

        _dbContext.Orders.Add(order);

        // Outbox Pattern: Save event to outbox instead of publishing directly
        var orderCreatedEvent = new OrderCreatedEvent(order.Id);
        var outboxMessage = new OutboxMessage
        {
            Id = Guid.NewGuid(),
            Type = orderCreatedEvent.GetType().Name,
            Content = JsonSerializer.Serialize(orderCreatedEvent),
            OccurredOnUtc = DateTime.UtcNow
        };

        _dbContext.OutboxMessages.Add(outboxMessage);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return order.Id;
    }
}
