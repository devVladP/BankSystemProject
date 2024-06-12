using BankSystem.Core.Common;
using BankSystem.Core.Domain.Cards.Common;
using BankSystem.Core.Domain.Cards.Models;

namespace BankSystem.Core.Domain.Cards.Rules;

internal class CardMustExistRule(Guid cardId, ICardMustExistChecker clientMustExistChecker) : IBusinessRuleAsync
{
    public async Task<RuleResult> CheckAsync(CancellationToken cancellationToken = default)
    {
        var exists = await clientMustExistChecker.CheckCardMustExistAsync(cardId, cancellationToken);
        return Check(exists);
    }

    private RuleResult Check(bool exists)
    {
        if (exists) return RuleResult.Success();
        return RuleResult.Failure($"{nameof(Card)} was not found. {nameof(Card.Id)}: '{cardId}'.");
    }
}
