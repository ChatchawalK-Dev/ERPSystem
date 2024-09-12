using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace ERPSystem.Security
{
    public class CustomAuthorizationHandler : AuthorizationHandler<CustomRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CustomRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "CustomClaimType"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }

    public class CustomRequirement : IAuthorizationRequirement
    {
        
    }
}
