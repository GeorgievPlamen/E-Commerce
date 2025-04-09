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
                sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (outcome, timespan, retryAttempt, context) =>
                {
                    logger.LogInformation("Executing retry attempt {retryAttempt}", retryAttempt);
                });

    public IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        => Policy
            .HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
            .CircuitBreakerAsync(
                handledEventsAllowedBeforeBreaking: 3,
                durationOfBreak: TimeSpan.FromMinutes(2),
                onBreak: (outcome, timespan) =>
                {
                    logger.LogInformation("Circuit breaker triggered.");
                },
                onReset: () =>
                {
                    logger.LogInformation("Circuit breaker reseting.");
                });

    public IAsyncPolicy<HttpResponseMessage> GetTimeoutPolicy()
        => Policy
            .TimeoutAsync<HttpResponseMessage>(TimeSpan.FromMicroseconds(1500));

    public IAsyncPolicy<HttpResponseMessage> GetCombinedPolicy()
    {
        var retry = GetRetryPolicy();
        var circuitBreaker = GetCircuitBreakerPolicy();
        var timeout = GetTimeoutPolicy();

        return Policy.WrapAsync(retry, circuitBreaker, timeout);
    }
}