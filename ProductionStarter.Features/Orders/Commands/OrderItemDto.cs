namespace ProductionStarter.Features.Orders.Commands;

public record OrderItemDto(string ProductId, int Quantity, decimal UnitPrice);
