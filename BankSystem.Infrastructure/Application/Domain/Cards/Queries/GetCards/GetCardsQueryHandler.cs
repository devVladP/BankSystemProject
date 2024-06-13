using BankSystem.Application.Domain.Cards.Queries.GetCards;
using BankSystem.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PagesResponses;

namespace BankSystem.Infrastructure.Application.Domain.Cards.Queries.GetCards;

public class GetCardsQueryHandler(BankSystemDbContext dbContext) : IRequestHandler<GetCardsQuery, PageResponse<CardDto[]>>
{
    public async Task<PageResponse<CardDto[]>> Handle(GetCardsQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.Cards.AsNoTracking();

        var skipCount = (request.page - 1) * request.pageSize;

        var cards = await query
            .Skip(skipCount)
            .Take(request.pageSize)
            .Select(c => new CardDto
            {
                Id = c.Id,
                Number = c.Number,
                Balance = c.Balance,
            })
            .OrderBy(x => x.Number)
            .ToArrayAsync(cancellationToken);

        var count = await query.CountAsync(cancellationToken);

        return new PageResponse<CardDto[]>(count, cards);
    }
}
