using MediatR;

namespace BankSystem.Application.Domain.Clients.Queries.GetClientDetails;

public record GetClientDetailsQuery(Guid id) : IRequest<ClientDetailsDto>;

