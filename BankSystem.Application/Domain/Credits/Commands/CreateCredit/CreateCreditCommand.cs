using MediatR;

namespace BankSystem.Application.Domain.Credits.Commands.CreateCredit;

public record CreateCreditCommand(decimal InitialSum, DateOnly CreditIssueDate, byte PercentPerMonth, Guid CardId) : IRequest<Guid>;
