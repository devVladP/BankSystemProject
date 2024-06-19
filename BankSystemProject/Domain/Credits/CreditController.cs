using BankSystem.Api.Common;
using BankSystem.Core.Domain.Credits.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using BankSystem.Api.Domain.Credits.Requests;
using BankSystem.Application.Domain.Credits.Commands.CreateCredit;
using MediatR;
using BankSystem.Application.Domain.Credits.Queries.GetCredits;
using PagesResponses;
using BankSystem.Application.Domain.Credits.Queries.GetCreditDetails;
using BankSystem.Api.Constants;
using BankSystem.Application.Domain.Clients.Commands.RemoveClient;
using BankSystem.Application.Domain.Credits.Commands.PayCredit;
using Microsoft.AspNetCore.Authorization;

namespace BankSystem.Api.Domain.Credits;

[Route(Routes.Credits)]
public class CreditController(IMediator mediator) : ApiControllerBase
{
    [Authorize(Policy = "BankClient")]
    [HttpPost]
    [ProducesResponseType(typeof(Credit), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCreditAsync(
        [FromBody][Required] CreateCreditRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateCreditCommand(request.InitialSum, request.CreditIssueDate, request.PercentPerMonth, request.CardId);

        var clientId = await mediator.Send(command, cancellationToken);
        return Created(clientId);
    }

    [Authorize(Policy = "BankClient")]
    [HttpDelete("{cardId}/{creditId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> PayCreditAsync(
        [FromRoute][Required] Guid creditId,
        [FromRoute][Required] Guid cardId,

        CancellationToken cancellationToken = default)
    {
        var command = new PayCreditCommand(creditId, cardId);
        await mediator.Send(command, cancellationToken);

        return Ok();
    }

    [Authorize(Policy = "BankEmployee")]
    [HttpGet]
    [ProducesResponseType(typeof(PageResponse<CreditDto[]>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCreditsAsync(
        [FromRoute] int page = 1,
        [FromRoute] int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var query = new GetCreditsQuery(page, pageSize);
        var credits = await mediator.Send(query, cancellationToken);

        return Ok(credits);
    }

    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CreditDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCreditDetailsAsync(
        [FromRoute][Required] Guid id,
        CancellationToken cancellationToken = default
        )
    {
        var query = new GetCreditDetailsQuery(id);
        var credit = await mediator.Send(query, cancellationToken);

        return Ok(credit);
    }
}
