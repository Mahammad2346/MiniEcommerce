using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniEcommerce.DataAccessLayer.Context;
using MiniEcommerce.DataAccessLayer.Repositories;
using MiniEcommerce.DataAccessLayer.Repositories.Interfaces;

namespace MiniEcommerce.DataAccessLayer.Extensions
{
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
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }
    }
}
