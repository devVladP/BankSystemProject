using BankSystem.Core.Domain.Cards.Common;
using BankSystem.Core.Domain.Cards.Models;
using BankSystem.Core.Exceptions;
using BankSystem.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Infrastructure.Core.Domain.Cards;

public class CardClientRepository(BankSystemDbContext dbContext) : ICardClientRepository
{
    public void Add(ClientsCards clientCard)
    {
        dbContext.ClientsCards.Add(clientCard);
    }

    public async Task DeleteAsync(Guid cardId, Guid clientId)
    {
        var clientCard = await dbContext.ClientsCards.FindAsync(cardId, clientId);
        dbContext.ClientsCards.Remove(clientCard);
    }

    public async Task<ClientsCards> FindAsync(Guid cardId, Guid clientId, CancellationToken cancellationToken)
    {
        var clientCard = await dbContext.ClientsCards
            .SingleOrDefaultAsync(x => x.CardId == cardId && x.ClientId == clientId, cancellationToken);
        return clientCard ?? throw new NotFoundException($"There is no {nameof(ClientsCards)} relation");
    }
}
