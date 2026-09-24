using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Routing;
using ProductionStarter.Features.Orders.Commands;
using ProductionStarter.Features.Orders.Queries;

namespace ProductionStarter.Features;

public static class DependencyInjection
{
    public static IServiceCollection AddFeatures(this IServiceCollection services)
    {
        services.AddMediatR(cfg => 
        {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });

        return services;
    }

    public static IEndpointRouteBuilder MapFeatureEndpoints(this IEndpointRouteBuilder app)
    {
        CreateOrderEndpoint.MapEndpoint(app);
        GetOrderByIdEndpoint.MapEndpoint(app);
        
        return app;
    }
}
