using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace BankSystem.Api.Auth0;

public class ClaimsTransformation : IClaimsTransformation
{
    private readonly IHttpContextAccessor _httpContextAccessor;


    public ClaimsTransformation(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        // Extend with custom claims.
        return await ExtendWithCustomClaims(principal);
    }

    private async Task<ClaimsPrincipal> ExtendWithCustomClaims(ClaimsPrincipal principal)
    {
        // Clone current identity
        var principleClone = principal.Clone();
        var newIdentity = (ClaimsIdentity)principleClone.Identity!;
        Console.WriteLine("Entered ClaimsTransofmation ExtendWithCustomClaims");
        try
        {
            // Add claims to cloned identity
            //if (!newIdentity.HasClaim(c => c.Type == ClaimTypes.Role && c.Value == "BankEmployee"))
            //{
            //    newIdentity.AddClaim(new Claim(ClaimTypes.Role, "BankEmployee"));
            //}

            var roleClaims = principal.FindAll(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" && newIdentity.FindAll(c => c.Type == ClaimTypes.Role) == null);
            foreach ( var roleClaim in roleClaims )
            {
                newIdentity.AddClaim(new Claim(ClaimTypes.Role, roleClaim.Value));
            }

            return principleClone;
        }
        catch (Exception e)
        {
            return principleClone;
        }
    }
}