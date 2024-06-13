using BankSystem.Core.Domain.Credits.Common;
using BankSystem.Core.Domain.Credits.Models;
using BankSystem.Core.Exceptions;
using BankSystem.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Infrastructure.Core.Domain.Credits;

public class CreditRepository(BankSystemDbContext dbContext) : ICreditRepository
{
    public void Add(Credit credit)
    {
        dbContext.Credits.Add(credit);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var clientToDelete = await dbContext.Credits.FindAsync(id, cancellationToken);
        dbContext.Credits.Remove(clientToDelete);
    }

    public async Task<Credit> FindAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Credits.FindAsync(id, cancellationToken)
            ?? throw new NotFoundException($"Credit with id {id} has not been found");
    }

    public async Task<IReadOnlyCollection<Credit>> FindManyAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken)
    {
        return await dbContext.Credits.Where(cr => ids.Contains(cr.Id)).ToListAsync(cancellationToken);
    }
}
