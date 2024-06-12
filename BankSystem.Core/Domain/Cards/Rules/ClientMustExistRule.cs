using BankSystem.Core.Common;
using BankSystem.Core.Domain.Cards.Common;
using BankSystem.Core.Domain.Clients.Models;

namespace BankSystem.Core.Domain.Cards.Rules;

internal class ClientMustExistRule(Guid clientId, IClientMustExistChecker clientMustExistChecker) : IBusinessRuleAsync
{
    public async Task<RuleResult> CheckAsync(CancellationToken cancellationToken = default)
    {
        var exists = await clientMustExistChecker.CheckClientMustExistAsync(clientId, cancellationToken);
        return Check(exists);
    }

    private RuleResult Check(bool exists)
    {
        if (exists) return RuleResult.Success();
        return RuleResult.Failure($"{nameof(Client)} was not found. {nameof(Client.Id)}: '{clientId}'.");
    }
}
