using BankSystem.Core.Common;
using BankSystem.Core.Domain.Clients.Common;
using BankSystem.Core.Domain.Clients.Data;
using MediatR;

namespace BankSystem.Application.Domain.Clients.Commands.UpdateClient;

public class UpdateClientCommandHandler(
    IClientRepository clientRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateClientCommand>
{
    public async Task Handle(UpdateClientCommand command, CancellationToken cancellationToken)
    {
        var client = await clientRepository.FindAsync(command.Id, cancellationToken);

        var data = new UpdateClientData(
            command.Id,
            command.FirstName,
            command.LastName,
            command.Email,
            command.MiddleName);

        client.Update(data);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
