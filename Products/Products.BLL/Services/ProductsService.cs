using System.Linq.Expressions;
using AutoMapper;
using FluentValidation;
using Products.BLL.DTO;
using Products.BLL.RabbitMQ;
using Products.BLL.ServiceContracts;
using Products.DAL.Entities;
using Products.DAL.RepositoryContracts;

namespace Products.BLL.Services;

public class ProductsService(
    IProductsRepository productsRepository,
    IMapper mapper,
    IValidator<ProductAddRequest> addValidator,
    IValidator<ProductUpdateRequest> updateValidator,
    IRabbitMQPublisher rabbitMQPublisher) : IProductsService
{
    public async Task<ProductResponse?> AddProduct(ProductAddRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var validationResult = await addValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));

            throw new ArgumentException(errors);
        }

        var product = mapper.Map<Product>(request);

        var result = await productsRepository.AddProduct(product);

        var response = mapper.Map<ProductResponse>(result);

        return response;
    }

    public async Task<bool> DeleteProduct(Guid productId)
    {
        var product = await productsRepository.GetProductByCondition(x => x.ProductID == productId);

        if (product is null)
            return false;

        var result = await productsRepository.DeleteProduct(product.ProductID);

        if (result)
        {
            var routingKey = "product.delete";
            var message = new ProductDeletionMessage(product.ProductID, product.ProductName);

            rabbitMQPublisher.Publish(routingKey, message);
        }

        return result;
    }

    public async Task<ProductResponse?> GetProductByCondition(Expression<Func<Product, bool>> expression)
    {
        var result = await productsRepository.GetProductByCondition(expression);

        var response = mapper.Map<ProductResponse>(result);

        return response;
    }

    public async Task<List<ProductResponse?>> GetProducts()
    {
        var result = await productsRepository.GetProducts();

        var response = mapper.Map<List<ProductResponse?>>(result);

        return response;
    }

    public async Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<Product, bool>> expression)
    {
        var result = await productsRepository.GetProductsByCondition(expression);

        var response = mapper.Map<List<ProductResponse?>>(result);

        return response;
    }

    public async Task<ProductResponse?> UpdateProduct(ProductUpdateRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var validationResult = await updateValidator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = string.Join(",", validationResult.Errors.Select(x => x.ErrorMessage));

            throw new ArgumentException(errors);
        }

        var product = mapper.Map<Product>(request);

        var routingKey = "product.update.name";
        var message = new ProductNameUpdateMessage(product.ProductID, product.ProductName);

        rabbitMQPublisher.Publish(routingKey, message);

        var result = await productsRepository.UpdateProduct(product);

        var response = mapper.Map<ProductResponse>(result);

        return response;
    }
}
