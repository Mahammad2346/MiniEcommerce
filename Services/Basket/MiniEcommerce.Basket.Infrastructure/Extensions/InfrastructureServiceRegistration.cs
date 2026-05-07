using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MiniEcommerce.Basket.Application.Interfaces;
using MiniEcommerce.Basket.Infrastructure.Constants;
using MiniEcommerce.Basket.Infrastructure.Repositories;

namespace MiniEcommerce.Basket.Infrastructure.Extensions
{
	public static class InfrastructureServiceRegistration
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddStackExchangeRedisCache(options =>
			{
				options.Configuration = configuration.GetValue<string>(DatabaseConstants.RedisConnectionStringKey);
			});

			services.AddScoped<IBasketRepository, BasketRepository>();

			return services;
		}
	}
}
