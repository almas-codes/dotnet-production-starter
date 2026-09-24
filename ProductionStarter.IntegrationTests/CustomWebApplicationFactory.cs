using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProductionStarter.Infrastructure.Data;

namespace ProductionStarter.IntegrationTests;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    // Using the local Postgres instance provided by the user
    private readonly string _connectionString = "Host=localhost;Port=5432;Database=test_production_starter;Username=postgres;Password=admin;";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            if (descriptor != null) services.Remove(descriptor);

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(_connectionString);
            });
            
            var connectionFactoryDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(ISqlConnectionFactory));
            if (connectionFactoryDescriptor != null) services.Remove(connectionFactoryDescriptor);

            services.AddScoped<ISqlConnectionFactory>(_ => new TestSqlConnectionFactory(_connectionString));
            
            // Ensure database is created for tests
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
        });
    }
}
