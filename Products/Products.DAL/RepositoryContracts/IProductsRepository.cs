using System.Linq.Expressions;
using Products.DAL.Entities;

namespace Products.DAL.RepositoryContracts;

public interface IProductsRepository
{
    Task<IEnumerable<Product>> GetProducts();
    Task<IEnumerable<Product>> GetProductsByCondition(Expression<Func<Product, bool>> condition);
    Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> condition);
    Task<Product?> AddProduct(Product product);
    Task<Product?> UpdateProduct(Product product);
    Task<bool> DeleteProduct(Guid productId);
}