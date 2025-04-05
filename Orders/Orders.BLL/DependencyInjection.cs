using Microsoft.Extensions.DependencyInjection;
using Orders.BLL.Mappers;

namespace Orders.BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddBLL(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(OrderAddRequestToOrderMappingProfile));

        return services;
    }
}