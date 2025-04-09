using Products.BLL.DTO;
using Products.BLL.ServiceContracts;

namespace Products.API.ApiEndpoints;

public static class ProductApiEndpoints
{
    public static void MapProducts(this IEndpointRouteBuilder builder)
    {
        var group = builder.MapGroup("api/products");

        group.MapGet("/", async (IProductsService productsService) =>
        {
            var products = await productsService.GetProducts();

            return Results.Ok(products);
        });

        group.MapGet("/search/product-id/{productId}", async (Guid productId, IProductsService productsService) =>
        {
            await Task.Delay(100);
            throw new NotImplementedException();

            var product = await productsService.GetProductByCondition(x => x.ProductID == productId);

            if (product is null)
                return Results.NotFound();

            return Results.Ok(product);
        });

        group.MapGet("/search/{searchString}", async (string searchString, IProductsService productsService) =>
        {
            var product = await productsService.GetProductsByCondition(x => x.ProductName!.Contains(searchString));
            var category = await productsService.GetProductsByCondition(x => x.Category!.Contains(searchString));

            var result = product.Union(category);

            return Results.Ok(result);
        });

        group.MapPost("/", async (ProductAddRequest request, IProductsService productsService) =>
        {
            var product = await productsService.AddProduct(request);

            return Results.Ok(product);
        });

        group.MapPut("/", async (ProductUpdateRequest request, IProductsService productsService) =>
        {
            var product = await productsService.UpdateProduct(request);

            return Results.Ok(product);
        });

        group.MapDelete("/{productId}", async (Guid productId, IProductsService productsService) =>
        {
            var result = await productsService.DeleteProduct(productId);

            return Results.Ok(result);
        });
    }
}