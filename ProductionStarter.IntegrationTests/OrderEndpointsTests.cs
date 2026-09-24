using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using ProductionStarter.Features.Orders.Commands;
using ProductionStarter.Features.Orders.Queries;
using ProductionStarter.Core.Entities;

namespace ProductionStarter.IntegrationTests;

public class OrderEndpointsTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly HttpClient _client;

    public OrderEndpointsTests(CustomWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task CreateOrder_ShouldReturnCreated()
    {
        // Arrange
        var command = new CreateOrderCommand("customer-123", 100m, new List<OrderItemDto>
        {
            new OrderItemDto("prod-1", 2, 50m)
        });

        // Act
        var response = await _client.PostAsJsonAsync("/api/orders", command);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        
        var location = response.Headers.Location?.ToString();
        location.Should().NotBeNull();
        
        // Follow up with a GET request to verify Dapper read side works
        var getResponse = await _client.GetAsync(location);
        getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var order = await getResponse.Content.ReadFromJsonAsync<OrderResponse>();
        order.Should().NotBeNull();
        order!.CustomerId.Should().Be("customer-123");
        order.TotalAmount.Should().Be(100m);
    }
}
