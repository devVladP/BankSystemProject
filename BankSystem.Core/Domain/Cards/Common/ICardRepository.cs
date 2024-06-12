using BankSystem.Core.Domain.Cards.Models;


namespace BankSystem.Core.Domain.Cards.Common;

public interface ICardRepository
{
    public Task<Card> FindAsync(Guid Id, CancellationToken cancellationToken);

    public Task<IReadOnlyCollection<Card>> FindManyAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken);

    public void Add(Card card);

    public Task Delete(Guid id, CancellationToken cancellationToken);
}
