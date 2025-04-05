using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Orders.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStringTemplate = configuration.GetConnectionString("MongoDB");

        var connectionString = connectionStringTemplate
            .Replace("$MONGO_HOST", Environment.GetEnvironmentVariable("MONGO_HOST"))
            .Replace("$MONGO_PORT", Environment.GetEnvironmentVariable("MONGO_PORT"));

        services.AddSingleton<IMongoClient>(new MongoClient(connectionString));

        services.AddScoped(provider =>
        {
            var client = provider.GetService<IMongoClient>();

            return client!.GetDatabase("OrdersDatabase");
        });

        return services;
    }
}