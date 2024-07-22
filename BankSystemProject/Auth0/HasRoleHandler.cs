using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BankSystem.Api.Auth0;

public class HasRoleHandler(ILogger<HasRoleHandler> logger) : AuthorizationHandler<HasRoleRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasRoleRequirement requirement)
    {
        logger.LogInformation("HasRoleHandler invoked");

        var roles = context.User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();

        logger.LogInformation("User roles: {Roles}", string.Join(", ", roles));

        if (roles.Contains(requirement.Role))
        {
            logger.LogInformation("user has required role");
            context.Succeed(requirement);
        }
        else
        {
            logger.LogInformation("user has not required role");
        }

        return Task.CompletedTask;
    }
}
