using Microsoft.Extensions.DependencyInjection;
using MiniEcommerce.Product.BusinessLogicLayer.Interfaces;
using MiniEcommerce.Product.BusinessLogicLayer.Services;

namespace MiniEcommerce.Product.BusinessLogicLayer.Extensions;

public static class BusinessLogicServiceCollectionExtensions
{
	public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
	{
		services.AddScoped<IProductService, ProductService>();
		services.AddScoped<ICategoryService, CategoryService>();

		return services;
	}
}
