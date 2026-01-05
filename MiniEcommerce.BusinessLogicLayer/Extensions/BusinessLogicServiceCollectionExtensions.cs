using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniEcommerce.BusinessLogicLayer.Interfaces;
using MiniEcommerce.BusinessLogicLayer.Services;
using MiniEcommerce.DataAccessLayer.Extensions;

namespace MiniEcommerce.BusinessLogicLayer.Extensions;

public static class BusinessLogicServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLogicLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDataAccessLayer(configuration);

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();

        services.AddMapping();

        return services;
    }
}
