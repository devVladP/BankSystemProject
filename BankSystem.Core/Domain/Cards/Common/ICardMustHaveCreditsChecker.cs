namespace BankSystem.Core.Domain.Cards.Common;

public interface ICardMustHaveCreditsChecker
{
    Task<bool> CheckCardHaveCurrentCredit(Guid cardId, Guid creditId, CancellationToken cancellationToken);
}
