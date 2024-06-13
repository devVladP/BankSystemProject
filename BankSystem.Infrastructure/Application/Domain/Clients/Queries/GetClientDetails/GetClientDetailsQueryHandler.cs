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
            .ThenInclude(cc => cc.Credits)
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
                }).ToArray(),
                TotalCredit = a.ClientsCards.Select(cc => cc.Card.Credits.Select(cr => cr.CountCurrentCredit()).Sum()).Sum(),
                TotalBalance = a.ClientsCards.Select(x => x.Card.Balance).Sum(),
            }).SingleOrDefaultAsync(cancellationToken)
        ?? throw new NotFoundException($"There is no client with Id: {request.id}");
    }
}
