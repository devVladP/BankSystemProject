using BankSystem.Api.Common;
using BankSystem.Api.Constants;
using BankSystem.Api.Domain.Clients.Requests;
using BankSystem.Application.Domain.Clients.Commands.CreateClient;
using BankSystem.Application.Domain.Clients.Commands.RemoveClient;
using BankSystem.Application.Domain.Clients.Commands.UpdateClient;
using BankSystem.Application.Domain.Clients.Queries.GetClientDetails;
using BankSystem.Application.Domain.Clients.Queries.GetClients;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagesResponses;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.Api.Domain.Clients;

[Route(Routes.Clients)]
public class ClientsController(IMediator mediator) : ApiControllerBase
{
    //[Authorize(Policy = "BankEmployeePolicy")]
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
    [HttpGet("details/{id}")]
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

    [HttpPost("create")]
    [ProducesResponseType(typeof(ClientDetailsDto), StatusCodes.Status201Created)]
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
    [HttpDelete("{id}")]
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

    [HttpGet("check")]
    public Task<IActionResult> CheckUser(
        [FromQuery] string Id,
        CancellationToken cancellationToken = default
        )
    {
        throw new NotImplementedException();
    }
}
