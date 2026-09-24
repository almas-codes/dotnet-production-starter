using Microsoft.EntityFrameworkCore;
using ProductionStarter.Core.Entities;

namespace ProductionStarter.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<OutboxMessage> OutboxMessages => Set<OutboxMessage>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().HasKey(o => o.Id);
        modelBuilder.Entity<OrderItem>().HasKey(o => o.Id);
        modelBuilder.Entity<OutboxMessage>().HasKey(o => o.Id);
        
        base.OnModelCreating(modelBuilder);
    }
}
