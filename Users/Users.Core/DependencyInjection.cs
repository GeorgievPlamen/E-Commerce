using Azure.Identity;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
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
        services.AddScoped<GraphServiceClient>(provider =>
        {
            var scopes = new[] { "https://graph.microsoft.com/.default" };

            var options = new ClientSecretCredentialOptions() { AuthorityHost = AzureAuthorityHosts.AzurePublicCloud };

            var clientSecretCredential = new ClientSecretCredential("tenant-id", "client-id", "client-secret", options);

            var graphClient = new GraphServiceClient(clientSecretCredential, scopes);

            return graphClient;
        });

        return services;
    }
}