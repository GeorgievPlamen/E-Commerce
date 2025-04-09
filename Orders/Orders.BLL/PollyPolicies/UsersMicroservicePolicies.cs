using Microsoft.Extensions.Logging;
using Polly;

namespace Orders.BLL.PollyPolicies;

public class UsersMicroservicePolicies(ILogger<UsersMicroservicePolicies> logger) : IUsersMicroservicePolicies
{
    public IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        => Policy
            .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .WaitAndRetryAsync(
                retryCount: 5,
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(2),
                onRetry: (outcome, timespan, retryAttempt, context) =>
                {
                    logger.LogInformation("Executing retry attempt {retryAttempt}", retryAttempt);
                });
}