using System.Data;
using Npgsql;
using ProductionStarter.Infrastructure.Data;

namespace ProductionStarter.IntegrationTests;

public class TestSqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString;

    public TestSqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_connectionString);
    }
}
