using MediatR;

namespace BankSystem.Application.Domain.Cards.Commands.DeleteClientCard;

public record DeleteClientCardCommand(Guid cardId, Guid clientId) : IRequest;
