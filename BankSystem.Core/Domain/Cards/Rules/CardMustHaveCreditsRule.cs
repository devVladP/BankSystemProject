using BankSystem.Core.Common;
using BankSystem.Core.Domain.Cards.Common;
using BankSystem.Core.Domain.Cards.Models;
using BankSystem.Core.Domain.Credits.Models;

namespace BankSystem.Core.Domain.Cards.Rules;

internal class CardMustHaveCreditsRule(Guid cardId, 
    Guid creditId, 
    ICardMustHaveCreditsChecker cardMustHaveCreditsChecker
    ) : IBusinessRuleAsync
{
    public async Task<RuleResult> CheckAsync(CancellationToken cancellationToken = default)
    {
        var haveCredit = await cardMustHaveCreditsChecker.CheckCardHaveCurrentCredit(cardId, creditId, cancellationToken);
        return Check(haveCredit);
    }

    private RuleResult Check(bool haveCredit)
    {
        if (haveCredit) return RuleResult.Success();
        return RuleResult.Failure($"In the {nameof(Card)} with id {cardId} has not found {nameof(Credit)} with id '{creditId}'.");
    }
}
