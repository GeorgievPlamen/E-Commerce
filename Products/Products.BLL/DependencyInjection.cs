using Microsoft.Extensions.DependencyInjection;
using Products.BLL.Mappers;

namespace Products.BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddBLL(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MapperProfiles));

        return services;
    }
}