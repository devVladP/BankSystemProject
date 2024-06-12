using BankSystem.Application.Domain.Clients.Queries.GetClients;
using BankSystem.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PagesResponses;

namespace BankSystem.Infrastructure.Application.Domain.Clients.Queries.GetClients;

internal class GetClientsQueryHandler(BankSystemDbContext dbContext) : IRequestHandler<GetClientsQuery, PageResponse<ClientDto[]>>
{
    public async Task<PageResponse<ClientDto[]>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.Clients.AsNoTracking();

        var skipCount = (request.Page - 1) * request.PageSize;

        var authors = await query
            .Skip(skipCount)
            .Take(request.PageSize)
            .Select(c => new ClientDto
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                MiddleName = c.MiddleName,
                Email = c.Email
            })
            .ToArrayAsync(cancellationToken);

        var count = await query.CountAsync(cancellationToken);

        return new PageResponse<ClientDto[]>(count, authors);
    }
}
