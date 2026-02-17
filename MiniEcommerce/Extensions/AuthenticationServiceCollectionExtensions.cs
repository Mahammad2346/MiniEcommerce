using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using MiniEcommerce.Authorization;

namespace MiniEcommerce.Extensions;

public static class AuthenticationServiceCollectionExtensions
{
	public static IServiceCollection AddAuth0Authentication(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
			.AddJwtBearer(options =>
			{
				options.Authority = "https://dev-ax8s6bs03qdw4zy0.us.auth0.com/";
				options.Audience = "https://miniecommerce-api";
			});

		services.AddAuthorizationBuilder().AddPolicy(AuthorizationPolicies.ReadCategories, policy => policy.Requirements.Add(new HasScopeRequirement(AuthorizationPolicies.ReadCategories)))
			    .AddPolicy(AuthorizationPolicies.WriteCategories, policy => policy.Requirements.Add(new HasScopeRequirement(AuthorizationPolicies.WriteCategories)));

		services.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
		return services;
	}
}
