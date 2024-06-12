using BankSystem.Core.Common;
using BankSystem.Core.Domain.Clients.Common;
using BankSystem.Core.Domain.Clients.Models;
using BankSystem.Core.Domain.Clients.Data;
using MediatR;

namespace BankSystem.Application.Domain.Clients.Commands.CreateClient;

public class RemoveClientCommandHandler(
    IClientRepository clientRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateClientCommand, Guid>
{
    public async Task<Guid> Handle(CreateClientCommand command, CancellationToken cancellationToken)
    {
        var data = new CreateClientData(
            command.FirstName,
            command.LastName,
            command.Email,
            command.MiddleName);

        var client = Client.Create(data);

        clientRepository.Add(client);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return client.Id;
    }
}
