using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Users.Core.ServiceContracts;
using Users.Core.Services;
using Users.Core.Validators;

namespace Users.Core;

public static class DependencyInjection
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddTransient<IUsersService, UsersService>();
        services.AddValidatorsFromAssemblyContaining<LoginRequestValidator>();

        return services;
    }
}