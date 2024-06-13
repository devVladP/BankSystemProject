namespace BankSystem.Core.Domain.Credits.Data;

public record CreateCreditData(decimal InitialSum, DateOnly CreditIssueDate, byte PercentPerMonth, Guid CardId);
