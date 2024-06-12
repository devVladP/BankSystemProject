using Auth0.AspNetCore.Authentication;
using BankSystem.Api.Common;
using BankSystem.Api.Constants;
using BankSystem.Api.Domain.Clients.Requests;
using BankSystem.Application.Domain.Clients.Commands.CreateClient;
using BankSystem.Application.Domain.Clients.Commands.RemoveClient;
using BankSystem.Application.Domain.Clients.Commands.UpdateClient;
using BankSystem.Application.Domain.Clients.Queries.GetClientDetails;
using BankSystem.Application.Domain.Clients.Queries.GetClients;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagesResponses;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace BankSystem.Api.Domain.Clients;

[Route(Routes.Clients)]
public class ClientsController(IMediator mediator) : ApiControllerBase
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

    [Authorize(Policy = "BankEmployeePolicy")]
    [HttpGet]
    [ProducesResponseType(typeof(PageResponse<ClientDto[]>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetClientsAsync(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetClientsQuery(page, pageSize);
        var clients = await mediator.Send(query, cancellationToken);
        return Ok(clients);
    }

    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetClientDetailsAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var query = new GetClientDetailsQuery(id);
        var client = await mediator.Send(query, cancellationToken);
        return Ok(client);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ClientDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateClientAsync(
        [FromBody][Required] CreateClientRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateClientCommand(request.FirstName, request.LastName, request.Email, request.MiddleName);

        var clientId = await mediator.Send(command, cancellationToken);
        return Created(clientId);
    }

    [Authorize(Policy = "BankClientPolicy")]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateClientAsync(
        [FromRoute] [Required] Guid id,
        [FromBody][Required] UpdateClientRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateClientCommand(id, request.FirstName, request.LastName, request.Email, request.MiddleName);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [Authorize(Policy = "BankClientPolicy")]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteClientAsync(
        [FromRoute][Required] Guid id,
        CancellationToken cancellationToken = default)
    {
        var command = new RemoveClientCommand(id);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }
}
