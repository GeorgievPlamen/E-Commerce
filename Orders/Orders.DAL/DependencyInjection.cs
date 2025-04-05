using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Orders.DAL.Contracts;
using Orders.DAL.Repositories;

namespace Orders.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionStringTemplate = configuration.GetConnectionString("MongoDB");

        var connectionString = connectionStringTemplate!
            .Replace("$MONGO_HOST", Environment.GetEnvironmentVariable("MONGO_HOST"))
            .Replace("$MONGO_PORT", Environment.GetEnvironmentVariable("MONGO_PORT"));

        services.AddSingleton<IMongoClient>(new MongoClient(connectionString));

        services.AddScoped(provider =>
        {
            var client = provider.GetService<IMongoClient>();

            return client!.GetDatabase("OrdersDatabase");
        });

        services.AddScoped<IOrdersRepository, OrdersRepository>();
        return services;
    }
}