using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Users.Infrastructure.DbContext;

public class DapperDbContext
{
    public IDbConnection DbConnection { get; private set; }
    public DapperDbContext(IConfiguration configuration)
    {
        var connectionStringTemplate = configuration.GetConnectionString("PostgresConnection");

        var connectionString = connectionStringTemplate
            .Replace("$POSTGRES_HOST", Environment.GetEnvironmentVariable("POSTGRES_HOST"))
            .Replace("$POSTGRES_PASSWORD", Environment.GetEnvironmentVariable("POSTGRES_PASSWORD"));

        DbConnection = new NpgsqlConnection(connectionString);
    }
}