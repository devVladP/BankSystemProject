using BankSystem.Application.Domain.Credits.Queries.GetCredits;
using BankSystem.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PagesResponses;

namespace BankSystem.Infrastructure.Application.Domain.Credits.Queries.GetCredits
{
    internal class GetCreditsQueryHandler(BankSystemDbContext dbContext) : IRequestHandler<GetCreditsQuery, PageResponse<CreditDto[]>>
    {
        public async Task<PageResponse<CreditDto[]>> Handle(GetCreditsQuery request, CancellationToken cancellationToken)
        {
            var query = dbContext.Credits.AsNoTracking();

            var skipCount = (request.Page - 1) * request.PageSize;

            var allCredits = await query
                .Skip(skipCount)
                .Take(request.PageSize)
                .Select(x => new CreditDto
                {
                    Id = x.Id,
                    InitialSum = x.InitialSum,
                    CardId = x.CardId,
                })
                .ToArrayAsync(cancellationToken);

            var count = await query.CountAsync(cancellationToken);

            return new PageResponse<CreditDto[]>(count, allCredits);
        }
    }
}
