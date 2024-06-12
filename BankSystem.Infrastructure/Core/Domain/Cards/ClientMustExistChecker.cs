using BankSystem.Core.Domain.Cards.Common;
using BankSystem.Persistence;

namespace BankSystem.Infrastructure.Core.Domain.Cards;

internal class ClientMustExistChecker(BankSystemDbContext dbContext) : IClientMustExistChecker
{
    public async Task<bool> CheckClientMustExistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var client = await dbContext.Clients.FindAsync(id);
        return client is not null;
    }
}
