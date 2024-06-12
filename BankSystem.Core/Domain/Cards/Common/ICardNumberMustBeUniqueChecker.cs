namespace BankSystem.Core.Domain.Cards.Common;

public interface ICardNumberMustBeUniqueChecker
{
    Task<bool> IsUniqueAsync(string number, CancellationToken cancellationToken);
}
