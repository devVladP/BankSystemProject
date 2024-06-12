namespace BankSystem.Core.Domain.Cards.Common;

public interface IClientMustExistChecker
{
    Task<bool> CheckClientMustExistAsync(Guid id, CancellationToken cancellationToken = default);
}
