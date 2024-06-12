using MediatR;

namespace BankSystem.Application.Domain.Clients.Commands.RemoveClient
{
    public record RemoveClientCommand(Guid Id) : IRequest;
}
