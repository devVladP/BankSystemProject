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
            .ThenInclude(cr => cr.Credits)
            .Select(a => new ClientDetailsDto
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                MiddleName = a.MiddleName,
                Email = a.Email,
                Cards = a.ClientsCards.Select(cc => new CardInformationDto
                {
                    Id = cc.CardId,
                    Number = cc.Card.Number,
                    Balance = cc.Card.Balance,
                    CreditSum = cc.Card.Credits.Sum(cr => cr.InitialSum) 
                }).ToArray(),
                TotalBalance = a.ClientsCards.Select(x => x.Card.Balance).Sum(),
            }).SingleOrDefaultAsync(cancellationToken)
        ?? throw new NotFoundException($"There is no client with Id: {request.id}");
    }
}
