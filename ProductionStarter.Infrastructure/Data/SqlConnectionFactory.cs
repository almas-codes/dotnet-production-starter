using System.Data;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace ProductionStarter.Infrastructure.Data;

public class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly IConfiguration _configuration;

    public SqlConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateConnection()
    {
        var connectionString = _configuration.GetConnectionString("DefaultConnection");
        return new NpgsqlConnection(connectionString);
    }
}
