namespace BankSystem.Api.Domain.Credits.Requests;

public record CreateCreditRequest(decimal InitialSum, DateOnly CreditIssueDate, byte PercentPerMonth, Guid CardId);
