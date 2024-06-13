namespace BankSystem.Core.Domain.Cards.Data;

public record PayCreditData(decimal Amount, Guid CardId, Guid CreditId);