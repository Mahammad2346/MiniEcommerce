

using Microsoft.EntityFrameworkCore;
using MiniEcommerce.Product.API;
using MiniEcommerce.Product.API.Interfaces;
using MiniEcommerce.Product.API.Repositories;
using MiniEcommerce.Product.BusinessLogicLayer.Services;
using MiniEcommerce.Product.DataAccessLayer;
using MiniEcommerce.Product.DataAccessLayer.Repositories;

namespace MiniEcommerce.DataAccessLayer.Extensions;

public static class DataAccessServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccessLayer(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<MiniEcommerceDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")
            ));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }
}
