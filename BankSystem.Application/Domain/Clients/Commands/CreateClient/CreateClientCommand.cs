using MediatR;

namespace BankSystem.Application.Domain.Clients.Commands.CreateClient
{
    public record CreateClientCommand(string FirstName, string LastName, string Email, string? MiddleName = "") : IRequest<Guid>;
}
