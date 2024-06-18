using Auth0.AspNetCore.Authentication;
using BankSystem.Api.Auth0;
using BankSystem.Api.Common;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankSystem.Api.Domain.Auth;

[ApiController]
[Route("[controller]")]
public class AuthController(IConfiguration configuration) : ApiControllerBase
{
    [HttpGet("login")]
    public async Task Login(string returnUrl = "https://localhost:7219/")
    {
        var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
            .WithRedirectUri(returnUrl)
            .Build();

        await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    }

    [Authorize]
    [HttpGet("profile")]
    public async Task<IActionResult> Profile()
    {
        var accessToken = await HttpContext.GetTokenAsync("access_token");
        var idToken = await HttpContext.GetTokenAsync("id_token");
        // get roles from the token

        return Ok(new
        {
            Name = User.Identity.Name,
            EmailAddress = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value,
            ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value,
            Roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value),
            AccessToken = accessToken,
            IdToken = idToken
        });
    }

    [Authorize]
    [HttpGet("logout")]
    public async Task Logout()
    {
        var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
            // Indicate here where Auth0 should redirect the user after a logout.
            // Note that the resulting absolute Uri must be added to the
            // **Allowed Logout URLs** settings for the app.
            .WithRedirectUri("https://localhost:7219/")
            .Build();

        await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    [HttpGet("auth0")]
    public IActionResult GetAuth0Config()
    {
        var auth0Config = new Auth0Config
        {
            Domain = configuration["Auth0:Domain"],
            ClientId = configuration["Auth0:ClientId"],
            Audience = configuration["Auth0:Audience"]
        };

        return Ok(auth0Config);
    }
}
