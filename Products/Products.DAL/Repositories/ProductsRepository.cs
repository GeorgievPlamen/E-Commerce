using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Products.DAL.Context;
using Products.DAL.Entities;
using Products.DAL.RepositoryContracts;

namespace Products.DAL.Repositories;

public class ProductsRepository(AppDbContext dbContext) : IProductsRepository
{
    public async Task<Product?> AddProduct(Product product)
    {
        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();
        return product;
    }

    public async Task<bool> DeleteProduct(Guid productId)
    {
        var existing = await dbContext.Products.FirstOrDefaultAsync(x => x.ProductID == productId);

        if (existing is null)
            return false;

        dbContext.Products.Remove(existing);

        return await dbContext.SaveChangesAsync() > 0;
    }

    public async Task<Product?> GetProductByCondition(Expression<Func<Product, bool>> condition)
    {
        return await dbContext.Products.FirstOrDefaultAsync(condition);
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await dbContext.Products.ToListAsync();

    }

    public async Task<IEnumerable<Product>> GetProductsByCondition(Expression<Func<Product, bool>> condition)
    {
        return await dbContext.Products.Where(condition).ToListAsync();
    }

    public async Task<Product?> UpdateProduct(Product product)
    {
        var existing = await dbContext.Products.FirstOrDefaultAsync(x => x.ProductID == product.ProductID);

        if (existing is null)
            return null;

        existing.ProductName = product.ProductName;
        existing.UnitPrice = product.UnitPrice;
        existing.QuantityInStock = product.QuantityInStock;
        existing.Category = product.Category;

        await dbContext.SaveChangesAsync();

        return existing;
    }
}
