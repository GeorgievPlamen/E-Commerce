using System.Net.Http.Json;
using Orders.BLL.DTO;

namespace Orders.BLL.HttpClients;

public class ProductsMicroserviceClient(HttpClient httpClient)
{
    public async Task<ProductDTO?> GetProductByProductID(Guid productID)
    {
        var result = await httpClient.GetAsync($"/api/products/search/product-id/{productID}");

        if (!result.IsSuccessStatusCode)
        {
            return null;
        }

        var product = await result.Content.ReadFromJsonAsync<ProductDTO>();

        return product;
    }
}