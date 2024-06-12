using MediatR;

namespace BankSystem.Application.Domain.Cards.Commands.AddClientCard;

public record CreateClientCardCommand(Guid CardId, Guid ClientId) : IRequest;
