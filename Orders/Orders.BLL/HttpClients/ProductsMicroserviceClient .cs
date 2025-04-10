using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;
using Orders.BLL.DTO;

namespace Orders.BLL.HttpClients;

public class ProductsMicroserviceClient(
    HttpClient httpClient,
    IDistributedCache cache)
{
    public async Task<ProductDTO?> GetProductByProductID(Guid productID)
    {
        var cacheKey = $"product:{productID}";

        var cachedProduct = await cache.GetStringAsync(cacheKey);

        if (cachedProduct is not null)
        {
            var productFromCache = JsonSerializer.Deserialize<ProductDTO>(cachedProduct);

            return productFromCache;
        }

        var result = await httpClient.GetAsync($"/api/products/search/product-id/{productID}");

        if (!result.IsSuccessStatusCode)
        {
            return null;
        }

        var product = await result.Content.ReadFromJsonAsync<ProductDTO>();

        return product;
    }
}