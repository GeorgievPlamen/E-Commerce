using Polly;

namespace Orders.BLL.PollyPolicies;

public interface IProductsMicroservicePolicies
{
    IAsyncPolicy<HttpResponseMessage> GetFallbackPolicy();
}