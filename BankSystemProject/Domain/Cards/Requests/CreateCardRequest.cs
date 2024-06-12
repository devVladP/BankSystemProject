namespace BankSystem.Api.Domain.Cards.Requests;

public record CreateCardRequest(string Number, DateOnly IssueDate, DateOnly ExpirationDate, decimal Balance, string PaymentSystem);
