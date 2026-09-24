namespace ProductionStarter.Features.Orders.Queries;

public record OrderResponse(Guid Id, string CustomerId, decimal TotalAmount, string Status);
