namespace BankSystem.Core.Domain.Cards.Data;

public record UpdateCardData(
    string Number,
    string CVV2,
    DateOnly IssueDate,
    DateOnly ExpirationDate,
    decimal Balance,
    decimal Credit);
