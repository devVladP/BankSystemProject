namespace BankSystem.Api.Domain.Cards.Requests;

public record UpdateCardRequest(string Number, string CVV2, DateOnly IssueDate, DateOnly ExpirationDate, decimal Balance, decimal Credit);
