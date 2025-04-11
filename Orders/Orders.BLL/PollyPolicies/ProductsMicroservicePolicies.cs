using System.Text.Json;
using Microsoft.Extensions.Logging;
using Orders.BLL.DTO;
using Polly;
using Polly.Bulkhead;

namespace Orders.BLL.PollyPolicies;

public class ProductsMicroservicePolicies(ILogger<ProductsMicroservicePolicies> logger) : IProductsMicroservicePolicies
{
    public IAsyncPolicy<HttpResponseMessage> GetBulkheadIsolationPolicy()
    => Policy.BulkheadAsync<HttpResponseMessage>(
        maxParallelization: 2,
        maxQueuingActions: 40,
        onBulkheadRejectedAsync: (context) =>
        {
            logger.LogInformation("Bulkhead isolation triggered. Can't send any more request, queue is full.");

            throw new BulkheadRejectedException("Bulkhead queue is full!");
        });

    public IAsyncPolicy<HttpResponseMessage> GetFallbackPolicy()
    => Policy.HandleResult<HttpResponseMessage>(response => !response.IsSuccessStatusCode)
        .FallbackAsync((context) =>
        {
            logger.LogInformation("Executing fallback policy");

            var product = new ProductDTO(Guid.Empty, "Unavailable", "Unavailable", 0, 0);

            var response = new HttpResponseMessage(System.Net.HttpStatusCode.ServiceUnavailable)
            {
                Content = new StringContent(JsonSerializer.Serialize(product))
            };

            return Task.FromResult(response);
        });
}
