using BankSystem.Core.Domain.Cards.Common;
using BankSystem.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Infrastructure.Core.Domain.Cards;

public class CardNumberMustBeUniqueChecker(BankSystemDbContext dbContext) : ICardNumberMustBeUniqueChecker
{
    public async Task<bool> IsUniqueAsync(string number, CancellationToken cancellationToken)
    {
        return await dbContext.Cards
            .AsNoTracking()
            .AllAsync(cards => cards.Number != number, cancellationToken);
    }
}

