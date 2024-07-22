using BankSystem.Application.Domain.Clients.Queries.GetClientDetails;
using MediatR;

namespace BankSystem.Application.Domain.Clients.Queries.GetClientDetailsByAuth0;

public record GetClientDetailsByAuth0Query(string Auth0Id) : IRequest<ClientDetailsDto>;
