using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniEcommerce.Product.API;
using MiniEcommerce.Product.API.Interfaces;
using MiniEcommerce.Product.DataAccessLayer;

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
        return services;
    }
}
