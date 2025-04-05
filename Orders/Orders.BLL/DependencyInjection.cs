using Microsoft.Extensions.DependencyInjection;
using Orders.BLL.Mappers;
using Orders.BLL.ServiceContracts;
using Orders.BLL.Services;

namespace Orders.BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddBLL(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(OrderAddRequestToOrderMappingProfile));
        services.AddScoped<IOrdersService, OrdersService>();

        return services;
    }
}