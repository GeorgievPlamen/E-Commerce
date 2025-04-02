using Microsoft.Extensions.DependencyInjection;

namespace Products.BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddBLL(this IServiceCollection services)
    {
        return services;
    }
}