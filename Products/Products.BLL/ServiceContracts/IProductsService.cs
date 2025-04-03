using System.Linq.Expressions;
using Products.BLL.DTO;

namespace Products.BLL.ServiceContracts;

public interface IProductsService
{
    Task<List<ProductResponse?>> GetProducts();
    Task<List<ProductResponse?>> GetProductsByCondition(Expression<Func<ProductResponse, bool>> expression);
    Task<ProductResponse?> GetProductByCondition(Expression<Func<ProductResponse, bool>> expression);
    Task<ProductResponse?> AddProduct(ProductAddRequest request);
    Task<ProductResponse?> UpdateProduct(ProductUpdateRequest request);
    Task<bool> DeleteProduct(Guid productId);
}