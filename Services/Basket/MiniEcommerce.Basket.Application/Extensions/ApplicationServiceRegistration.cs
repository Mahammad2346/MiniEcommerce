using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MiniEcommerce.Basket.Application.Extensions
{
	public static class ApplicationServiceRegistration
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			var config = TypeAdapterConfig.GlobalSettings;
			config.Scan(Assembly.GetExecutingAssembly());

			services.AddSingleton(config);

			services.AddScoped<IMapper, ServiceMapper>();

			return services;
		}
	}
}
