using BankSystem.Application.Domain.Clients.Queries.GetClientDetails;
using BankSystem.Core.Exceptions;
using BankSystem.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Infrastructure.Application.Domain.Clients.Queries.GetClientDetails;

public class GetClientDetailsQueryHandler(BankSystemDbContext dbContext) : IRequestHandler<GetClientDetailsQuery, ClientDetailsDto>
{
    public async Task<ClientDetailsDto> Handle(GetClientDetailsQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Clients
            .AsNoTracking()
            .Where(a => a.Id == request.id)
            .Include(a => a.ClientsCards)
            .ThenInclude(cc => cc.Card)
            .Select(a => new ClientDetailsDto
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                MiddleName = a.MiddleName,
                Email = a.Email,
                Cards = a.ClientsCards.Select(c => new CardInformationDto
                {
                    Id = c.CardId,
                    Number = c.Card.Number,
                    Balance = c.Card.Balance,
                    Credit = c.Card.Credit,
                }).ToArray(),
                TotalCredit = a.ClientsCards.Select(x => x.Card.Credit).Sum(),
                TotalBalance = a.ClientsCards.Select(x => x.Card.Balance).Sum(),
            }).SingleOrDefaultAsync(cancellationToken)
        ?? throw new NotFoundException($"There is no client with Id: {request.id}");
    }
}
