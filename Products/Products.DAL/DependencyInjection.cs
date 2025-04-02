using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Products.DAL.Context;
using Products.DAL.Repositories;
using Products.DAL.RepositoryContracts;

namespace Products.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDAL(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<AppDbContext>(opt =>
        {
            opt.UseMySQL(config.GetConnectionString("DefaultConnection")!);
        });

        services.AddScoped<IProductsRepository, ProductsRepository>();

        return services;
    }
}