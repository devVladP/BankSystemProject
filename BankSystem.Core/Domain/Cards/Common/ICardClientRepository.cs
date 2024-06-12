using BankSystem.Core.Domain.Cards.Models;

namespace BankSystem.Core.Domain.Cards.Common;

public interface ICardClientRepository
{
    public void Add(ClientsCards clientCard);

    public Task<ClientsCards> FindAsync(Guid cardId, Guid clientId, CancellationToken cancellationToken);

    public Task DeleteAsync(Guid cardId, Guid clientId);
}
