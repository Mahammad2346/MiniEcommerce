using Microsoft.AspNetCore.Authorization;

namespace MiniEcommerce.Authorization;

public class HasScopeRequirement: IAuthorizationRequirement
	{
    public string Scope { get; }

    public HasScopeRequirement(string scope)
    {
        Scope = scope;  
    }
}
