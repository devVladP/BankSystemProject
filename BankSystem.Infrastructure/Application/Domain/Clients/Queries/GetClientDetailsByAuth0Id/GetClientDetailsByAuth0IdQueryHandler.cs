using BankSystem.Application.Domain.Clients.Queries.GetClientDetails;
using BankSystem.Application.Domain.Clients.Queries.GetClientDetailsByAuth0;
using BankSystem.Core.Exceptions;
using BankSystem.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Infrastructure.Application.Domain.Clients.Queries.GetClientDetailsByAuth0Id;

internal class GetClientDetailsByAuth0IdQueryHandler(BankSystemDbContext dbContext) : IRequestHandler<GetClientDetailsByAuth0Query, ClientDetailsDto>
{
    public async Task<ClientDetailsDto> Handle(GetClientDetailsByAuth0Query request, CancellationToken cancellationToken)
    {
        return await dbContext.Clients
            .AsNoTracking()
            .Where(a => a.Auth0Id.ToLower() == request.Auth0Id.ToLower())
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
                Auth0Id = a.Auth0Id,
                Cards = a.ClientsCards.Select(cc => new CardInformationDto
                {
                    Id = cc.CardId,
                    Number = cc.Card.Number,
                    Balance = cc.Card.Balance,
                    CreditSum = cc.Card.Credits.Sum(cr => cr.InitialSum)
                }).ToArray(),
                TotalBalance = a.ClientsCards.Select(x => x.Card.Balance).Sum(),
            }).SingleOrDefaultAsync(cancellationToken)
        ?? throw new NotFoundException($"There is no client with Auth0Id: {request.Auth0Id}");
    }
}
