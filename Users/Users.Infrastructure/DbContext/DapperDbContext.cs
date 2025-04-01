using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Users.Infrastructure.DbContext;

public class DapperDbContext
{
    public IDbConnection DbConnection { get; private set; }
    public DapperDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresConnection");
        DbConnection = new NpgsqlConnection(connectionString);
    }
}