using MediatR;
using PagesResponses;


namespace BankSystem.Application.Domain.Clients.Queries.GetClients;

public record GetClientsQuery(int Page, int PageSize) : IRequest<PageResponse<ClientDto[]>>;