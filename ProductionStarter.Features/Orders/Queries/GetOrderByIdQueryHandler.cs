using Dapper;
using MediatR;
using ProductionStarter.Infrastructure.Data;

namespace ProductionStarter.Features.Orders.Queries;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderResponse?>
{
    private readonly ISqlConnectionFactory _connectionFactory;

    public GetOrderByIdQueryHandler(ISqlConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<OrderResponse?> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        using var connection = _connectionFactory.CreateConnection();
        
        const string sql = """
            SELECT "Id", "CustomerId", "TotalAmount", "Status"
            FROM "Orders"
            WHERE "Id" = @Id
            """;

        return await connection.QueryFirstOrDefaultAsync<OrderResponse>(sql, new { request.Id });
    }
}
