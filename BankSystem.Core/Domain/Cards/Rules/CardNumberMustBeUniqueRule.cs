using BankSystem.Core.Common;
using BankSystem.Core.Domain.Cards.Common;
using System.Xml.Linq;

namespace BankSystem.Core.Domain.Cards.Rules;

internal class CardNumberMustBeUniqueRule(string number, ICardNumberMustBeUniqueChecker cardNumberMustBeUniqueChecker) : IBusinessRuleAsync
{
    public async Task<RuleResult> CheckAsync(CancellationToken cancellationToken = default)
    {
        var isUnique = await cardNumberMustBeUniqueChecker.IsUniqueAsync(number, cancellationToken);
        return Check(isUnique);
    }

    private RuleResult Check(bool isUnique)
    {
        if (isUnique) return RuleResult.Success();
        return RuleResult.Failure($"Card Number: '{number}' must be unique.");
    }
}
