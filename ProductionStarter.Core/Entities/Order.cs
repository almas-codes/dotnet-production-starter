namespace ProductionStarter.Core.Entities;

public class Order
{
    public Guid Id { get; set; }
    public string CustomerId { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; } = "Pending";
    public string TenantId { get; set; } = string.Empty;
    
    public List<OrderItem> Items { get; set; } = new();
}
