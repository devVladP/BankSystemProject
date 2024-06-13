using BankSystem.Application.Domain.Clients.Queries.GetClientDetails;
using BankSystem.Application.Domain.Credits.Queries.GetCreditDetails;
using BankSystem.Core.Exceptions;
using BankSystem.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Infrastructure.Application.Domain.Credits.Queries.GetCreditDetails
{
    internal class GetCreditDetailsQueryHandler(BankSystemDbContext dbContext)
        : IRequestHandler<GetCreditDetailsQuery, CreditDetailsDto>
    {
        public async Task<CreditDetailsDto> Handle(GetCreditDetailsQuery request, CancellationToken cancellationToken)
        {
            return await dbContext.Credits
                .AsNoTracking()
                .Where(cr => cr.Id == request.Id)
                .Include(cr => cr.Card)
                .Select(x => new CreditDetailsDto
                {
                    Id = x.Id,
                    InitialSum = x.InitialSum,
                    CreditIssueDate = x.CreditIssueDate,
                    FinalSum = x.CountCurrentCredit(),
                    PercentPerMonth = x.PercentPerMonth,
                    Card = new CardInformationDto
                    {
                        Id = x.Card.Id,
                        Number = x.Card.Number,
                        Balance = x.Card.Balance,
                    }
                }).SingleOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException($"There is no client with Id: {request.Id}");
        }
    }
}
