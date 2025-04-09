using System.Text.Json;
using Microsoft.Extensions.Logging;
using Orders.BLL.DTO;
using Polly;

namespace Orders.BLL.PollyPolicies;

public class ProductsMicroservicePolicies(ILogger<ProductsMicroservicePolicies> logger) : IProductsMicroservicePolicies
{
    public IAsyncPolicy<HttpResponseMessage> GetFallbackPolicy()
    => Policy.HandleResult<HttpResponseMessage>(response => !response.IsSuccessStatusCode)
        .FallbackAsync((context) =>
        {
            logger.LogInformation("Executing fallback policy");

            var product = new ProductDTO(Guid.Empty, "Unavailable", "Unavailable", 0, 0);

            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(product))
            };

            return Task.FromResult(response);
        });
}
