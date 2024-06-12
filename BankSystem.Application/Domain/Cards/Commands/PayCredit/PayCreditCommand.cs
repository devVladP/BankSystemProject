using MediatR;

namespace BankSystem.Application.Domain.Cards.Commands.PayCredit;

public record PayCreditCommand(Guid cardId, decimal Amount) : IRequest;