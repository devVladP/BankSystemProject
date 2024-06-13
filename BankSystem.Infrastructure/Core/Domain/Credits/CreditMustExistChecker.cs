using BankSystem.Core.Domain.Credits.Common;
using BankSystem.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Infrastructure.Core.Domain.Credits;

public class CreditMustExistChecker(BankSystemDbContext dbContext) : ICreditMustExistChecker
{
    public async Task<bool> CheckCreditMustExistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var credit = await dbContext.Credits.FindAsync(id);
        return credit is not null;
    }
}
