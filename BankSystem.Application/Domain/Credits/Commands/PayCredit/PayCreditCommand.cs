using MediatR;

namespace BankSystem.Application.Domain.Credits.Commands.PayCredit;

public record PayCreditCommand(Guid Id, Guid CardId) : IRequest;
