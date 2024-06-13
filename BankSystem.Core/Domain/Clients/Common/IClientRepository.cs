using BankSystem.Core.Domain.Clients.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Core.Domain.Clients.Common;

public interface IClientRepository
{
    public Task<Client> FindAsync(Guid id, CancellationToken cancellationToken);

    public Task<IReadOnlyCollection<Client>> FindManyAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken);

    public void Add(Client client);

    public Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
