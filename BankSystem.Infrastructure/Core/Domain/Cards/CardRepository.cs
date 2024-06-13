using BankSystem.Core.Domain.Cards.Common;
using BankSystem.Core.Domain.Cards.Models;
using BankSystem.Core.Exceptions;
using BankSystem.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Infrastructure.Core.Domain.Cards;

public class CardRepository(BankSystemDbContext dbContext) : ICardRepository
{
    public void Add(Card card)
    {
        dbContext.Add(card);
    }

    public async Task Delete(Guid id, CancellationToken cancellationToken)
    {
        var card = await dbContext.Cards.FindAsync(id, cancellationToken);
        dbContext.Cards.Remove(card);
    }

    public async Task<Card> FindAsync(Guid id, CancellationToken cancellationToken)
    {
        var card = await dbContext.Cards.FindAsync(id, cancellationToken);
        return card ?? throw new NotFoundException($"Card with ID {id} has not been found");
    }

    public async Task<IReadOnlyCollection<Card>> FindManyAsync(IReadOnlyCollection<Guid> ids, CancellationToken cancellationToken)
    {
        return await dbContext.Cards.Where(a => ids.Contains(a.Id)).ToListAsync(cancellationToken);
    }
}
