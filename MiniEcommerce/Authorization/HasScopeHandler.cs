using Microsoft.AspNetCore.Authorization;

namespace MiniEcommerce.Authorization;

    public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
{
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
        {
		var permissions = context.User.FindAll("permissions").Select(p => p.Value).ToList();

		var scopeClaim = context.User.FindFirst("scope")?.Value;
		if (!string.IsNullOrEmpty(scopeClaim))
		{
			permissions.AddRange(scopeClaim.Split(' '));
		}

		if (permissions.Any(p => p == requirement.Scope))
		{
			context.Succeed(requirement);
		}

		return Task.CompletedTask;
	}
    }
