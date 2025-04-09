using Polly;

namespace Orders.BLL.PollyPolicies;

public interface IUsersMicroservicePolicies
{
    IAsyncPolicy<HttpResponseMessage> GetRetryPolicy();
}