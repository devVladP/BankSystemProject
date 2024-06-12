using Microsoft.AspNetCore.Authorization;

namespace BankSystem.Api.Auth0;

public class HasRoleRequirement : IAuthorizationRequirement
{
    public string Role { get; }

    public HasRoleRequirement(string role)
    {
        Role = role ?? throw new ArgumentNullException(nameof(role));
    }
}
