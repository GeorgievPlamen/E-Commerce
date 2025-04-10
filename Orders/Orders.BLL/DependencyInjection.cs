using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Orders.BLL.Mappers;
using Orders.BLL.ServiceContracts;
using Orders.BLL.Services;

namespace Orders.BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddBLL(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(OrderAddRequestToOrderMappingProfile));
        services.AddScoped<IOrdersService, OrdersService>();
        services.AddStackExchangeRedisCache(opt =>
        {
            opt.Configuration = $"{configuration["REDIS_HOST"]}:{configuration["REDIS_PORT"]}";
        });

        return services;
    }
}