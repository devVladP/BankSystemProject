using MediatR;
namespace BankSystem.Application.Domain.Clients.Commands.UpdateClient;

public record UpdateClientCommand(Guid Id, string FirstName, string LastName, string Email, string? MiddleName = default) : IRequest;
