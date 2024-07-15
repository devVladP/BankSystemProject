using MediatR;

namespace BankSystem.Application.Domain.Clients.Commands.CreateClient
{
    public record CreateClientCommand(string FirstName, string LastName, string Email, string Auth0Id, string? MiddleName = "") : IRequest<Guid>;
}
