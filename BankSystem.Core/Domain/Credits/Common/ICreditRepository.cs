using BankSystem.Core.Domain.Credits.Models;

namespace BankSystem.Core.Domain.Credits.Common;

public interface ICreditRepository
{
    public Task<Credit> FindAsync(Guid id, CancellationToken cancellationToken);

    public Task<IReadOnlyCollection<Credit>> FindManyAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken);

    public void Add(Credit credit);

    public Task DeleteAsync(Guid id, CancellationToken cancelToken);
}
