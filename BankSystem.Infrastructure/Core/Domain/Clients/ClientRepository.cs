using BankSystem.Core.Domain.Clients.Common;
using BankSystem.Core.Domain.Clients.Models;
using BankSystem.Core.Exceptions;
using BankSystem.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Infrastructure.Core.Domain.Clients;

public class ClientRepository(BankSystemDbContext dbContext) : IClientRepository
{
    public void Add(Client client)
    {
        dbContext.Clients.Add(client);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var client = await dbContext.Clients.FindAsync(id, cancellationToken);
        dbContext.Clients.Remove(client);
    }

    public async Task<Client> FindAsync(Guid id, CancellationToken cancellationToken)
    {
        var client = await dbContext.Clients.FindAsync(id, cancellationToken);
        return client ?? throw new NotFoundException($"Client with ID {id} has not been found");
    }

    public async Task<Client> FindByAuthIdAsync(string AuthId, CancellationToken cancellationToken)
    {
       var client = await dbContext.Clients.Where(a => a.Auth0Id == AuthId).SingleOrDefaultAsync(cancellationToken);
       return client ?? throw new NotFoundException($"Client with Auth0 ID {AuthId} has not been found");
    }

    public async Task<IReadOnlyCollection<Client>> FindManyAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken)
    {
        var clients = await dbContext.Clients.Where(a => ids.Contains(a.Id)).ToListAsync(cancellationToken);
        return clients;
    }
}
