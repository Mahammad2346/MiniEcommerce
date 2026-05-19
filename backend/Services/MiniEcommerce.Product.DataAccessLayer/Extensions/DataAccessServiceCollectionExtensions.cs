using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniEcommerce.Product.DataAccessLayer.Constants;
using MiniEcommerce.Product.DataAccessLayer.Interfaces;
using MiniEcommerce.Product.DataAccessLayer.Repositories;

namespace MiniEcommerce.Product.DataAccessLayer.Extensions;

public static class DataAccessServiceCollectionExtensions
{
	public static IServiceCollection AddDataAccessLayer(
		this IServiceCollection services,
		IConfiguration configuration)
	{
		services.AddDbContext<MiniEcommerceDbContext>(options =>
			options.UseSqlServer(
				configuration.GetConnectionString(DatabaseConstants.DefaultConnection)
			));

		services.AddScoped<IUnitOfWork, UnitOfWork>();
		services.AddScoped<ICategoryRepository, CategoryRepository>();
		services.AddScoped<IProductRepository, ProductRepository>();

		return services;
	}
}
