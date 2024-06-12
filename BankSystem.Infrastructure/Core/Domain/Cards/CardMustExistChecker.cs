using BankSystem.Core.Domain.Cards.Common;
using BankSystem.Persistence;

namespace BankSystem.Infrastructure.Core.Domain.Cards;

public class CardMustExistChecker(BankSystemDbContext dbContext) : ICardMustExistChecker
{
    public async Task<bool> CheckCardMustExistAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var card = await dbContext.Cards.FindAsync(id);
        return card is not null;
    }
}
