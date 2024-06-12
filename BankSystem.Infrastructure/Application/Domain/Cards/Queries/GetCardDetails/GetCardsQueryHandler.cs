using BankSystem.Application.Domain.Cards.Queries.GetCardDetails;
using BankSystem.Application.Domain.Clients.Queries.GetClientDetails;
using BankSystem.Core.Exceptions;
using BankSystem.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BankSystem.Infrastructure.Application.Domain.Cards.Queries.GetCardDetails;

public class GetCardDetailsQueryHandler(BankSystemDbContext dbContext) : IRequestHandler<GetCardDetailsQuery, CardDetailsDto>
{
    public async Task<CardDetailsDto> Handle(GetCardDetailsQuery request, CancellationToken cancellationToken)
    {
        return await dbContext.Cards
            .AsNoTracking()
            .Where(x => x.Id == request.Id)
            .Include(x => x.ClientsCards)
            .ThenInclude(x => x.Client)
            .Select(x => new CardDetailsDto
            {
                Id = x.Id,
                Number = x.Number,
                CVV2 = x.CVV2,
                IssueDate = x.IssueDate,
                ExpirationDate = x.ExpirationDate,
                Balance = x.Balance,
                
                Owners = x.ClientsCards.Select(cc => new ClientInformationDto
                {
                    Id = cc.ClientId,
                    Name = cc.Client.MiddleName == null ?
                    $"{cc.Client.FirstName} {cc.Client.LastName}"
                    : $"{cc.Client.FirstName} {cc.Client.LastName} {cc.Client.MiddleName}"
                }).ToArray()
            }).SingleOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException($"There is no card with id {request.Id}");
    }
}
