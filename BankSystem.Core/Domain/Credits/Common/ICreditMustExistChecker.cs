namespace BankSystem.Core.Domain.Credits.Common;

public interface ICreditMustExistChecker
{
    Task<bool> CheckCreditMustExistAsync(Guid id, CancellationToken cancellationToken = default);
}
