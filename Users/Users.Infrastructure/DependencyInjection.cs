using Microsoft.Extensions.DependencyInjection;
using Users.Core.RepositoryContracts;
using Users.Infrastructure.Repositories;

namespace Users.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();

        return services;
    }
}