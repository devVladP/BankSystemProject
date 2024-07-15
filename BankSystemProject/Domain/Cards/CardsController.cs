using BankSystem.Api.Common;
using BankSystem.Api.Constants;
using BankSystem.Api.Domain.Cards.Requests;
using BankSystem.Application.Domain.Cards.Commands.AddClientCard;
using BankSystem.Application.Domain.Cards.Commands.CreateCard;
using BankSystem.Application.Domain.Cards.Commands.DeleteClientCard;
using BankSystem.Application.Domain.Cards.Commands.RemoveCard;
using BankSystem.Application.Domain.Cards.Commands.SendMoney;
using BankSystem.Application.Domain.Cards.Commands.UpdateCard;
using BankSystem.Application.Domain.Cards.Queries.GetCardDetails;
using BankSystem.Application.Domain.Cards.Queries.GetCards;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PagesResponses;
using System.ComponentModel.DataAnnotations;

namespace BankSystem.Api.Domain.Cards;
[Route(Routes.Cards)]
public class CardsController(IMediator mediator) : ApiControllerBase
{
    [Authorize(Policy = "BankEmployeePolicy")]
    [HttpGet]
    [ProducesResponseType(typeof(PageResponse<CardDto[]>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCardsAsync(
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10,
    CancellationToken cancellationToken = default)
    {
        var query = new GetCardsQuery(page, pageSize);
        var cards = await mediator.Send(query, cancellationToken);
        return Ok(cards);
    }

    [Authorize]
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CardDetailsDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCardDetailsAsync(
        [FromRoute] Guid id,
        CancellationToken cancellationToken = default)
    {
        var query = new GetCardDetailsQuery(id);
        var card = await mediator.Send(query, cancellationToken);
        return Ok(card);
    }

    [Authorize(Policy = "BankClientPolicy")]
    [HttpPost]
    [ProducesResponseType(typeof(CardDetailsDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateCardAsync(
        [FromBody][Required] CreateCardRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateCardCommand(request.Number, request.IssueDate, request.ExpirationDate, request.Balance, request.PaymentSystem);
        var cardId = await mediator.Send(command, cancellationToken);
        return Created(cardId);
    }

    [Authorize(Policy = "BankClientPolicy")]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCardAsync(
        [FromRoute][Required] Guid id,
        [FromBody][Required] UpdateCardRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new UpdateCardCommand(id, request.Number, request.CVV2, request.ExpirationDate, 
            request.IssueDate, request.Balance, request.Credit);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [Authorize(Policy = "BankClientPolicy")]
    [HttpPut("{senderId}/{receiverId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SendMoneyAsync(
        [FromRoute][Required] Guid senderId,
        [FromRoute][Required] Guid receiverId,
        [FromBody][Required] SendMoneyRequest request,
        CancellationToken cancellationToken)
    {
        var command = new SendMoneyCommand(senderId, receiverId, request.Amount);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [Authorize(Policy = "BankClientPolicy")]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCardAsync(
        [FromRoute][Required] Guid id,
        CancellationToken cancellationToken = default)
    {
        var command = new RemoveCardCommand(id);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpPost("{cardId}/clients")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AddClientCardAscync(
        [FromRoute][Required] Guid cardId,
        [FromBody][Required] AddClientCardRequest request,
        CancellationToken cancellationToken = default)
    {
        var command = new CreateClientCardCommand(cardId, request.ClientId);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }

    [HttpDelete("{clientId}/{cardId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteClientCardAsync(
        [FromRoute][Required] Guid cardId,
        [FromRoute][Required] Guid clientId,
        CancellationToken cancellationToken = default)
    {
        var command = new DeleteClientCardCommand(cardId, clientId);
        await mediator.Send(command, cancellationToken);
        return Ok();
    }
}
