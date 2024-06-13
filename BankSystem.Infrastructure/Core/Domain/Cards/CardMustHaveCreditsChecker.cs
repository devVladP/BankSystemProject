using BankSystem.Core.Domain.Cards.Common;
using BankSystem.Core.Domain.Cards.Models;
using BankSystem.Core.Exceptions;
using BankSystem.Persistence;

namespace BankSystem.Infrastructure.Core.Domain.Cards;

public class CardMustHaveCreditsChecker(BankSystemDbContext dbContext) : ICardMustHaveCreditsChecker
{
    public async Task<bool> CheckCardHaveCurrentCredit(Guid cardId, Guid creditId, CancellationToken cancellationToken)
    {
        var card = await dbContext.Cards.FindAsync(cardId, cancellationToken)
            ?? throw new NotFoundException($"There is no {typeof(Card)} with id {cardId}");
        return card.Credits.Any(x => x.Id == creditId);
    }
}
