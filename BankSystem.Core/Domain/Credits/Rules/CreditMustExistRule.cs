using BankSystem.Core.Common;
using BankSystem.Core.Domain.Cards.Common;
using BankSystem.Core.Domain.Cards.Models;
using BankSystem.Core.Domain.Credits.Common;
using BankSystem.Core.Domain.Credits.Models;

namespace BankSystem.Core.Domain.Credits.Rules;

internal class CreditMustExistRule(Guid creditId, ICreditMustExistChecker creditMustExistChecker) : IBusinessRuleAsync
{
    public async Task<RuleResult> CheckAsync(CancellationToken cancellationToken = default)
    {
        var exists = await creditMustExistChecker.CheckCreditMustExistAsync(creditId, cancellationToken);
        return Check(exists);
    }

    private RuleResult Check(bool exists)
    {
        if (exists) return RuleResult.Success();
        return RuleResult.Failure($"{nameof(Credit)} was not found. {nameof(Credit.Id)}: '{creditId}'.");
    }
}
