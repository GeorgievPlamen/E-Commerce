using Microsoft.Extensions.DependencyInjection;
using Users.Core.ServiceContracts;
using Users.Core.Services;

namespace Users.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddTransient<IUsersService, UsersService>();

        return services;
    }
}