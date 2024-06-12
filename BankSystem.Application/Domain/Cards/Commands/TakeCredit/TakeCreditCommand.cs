using MediatR;

namespace BankSystem.Application.Domain.Cards.Commands.TakeCredit;

public record TakeCreditCommand(Guid CardId, decimal Amount) : IRequest;
