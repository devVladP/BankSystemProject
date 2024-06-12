using BankSystem.Core.Common;
using BankSystem.Persistence;

namespace BankSystem.Infrastructure.Core.Common;

public class UnitOfWork(BankSystemDbContext dbContext) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}
