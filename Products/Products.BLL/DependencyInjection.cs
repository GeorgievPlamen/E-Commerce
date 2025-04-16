using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Products.BLL.Mappers;
using Products.BLL.RabbitMQ;
using Products.BLL.ServiceContracts;
using Products.BLL.Services;
using Products.BLL.Validators;

namespace Products.BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddBLL(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MapperProfiles));
        services.AddScoped<IProductsService, ProductsService>();
        services.AddValidatorsFromAssemblyContaining<ProductAddRequestValidator>();
        services.AddTransient<IRabbitMQPublisher, RabbitMQPublisher>();

        return services;
    }
}