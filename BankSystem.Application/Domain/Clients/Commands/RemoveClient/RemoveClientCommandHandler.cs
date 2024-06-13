using BankSystem.Core.Common;
using BankSystem.Core.Domain.Clients.Common;
using MediatR;

namespace BankSystem.Application.Domain.Clients.Commands.RemoveClient;

public class RemoveClientCommandHandler(
    IClientRepository clientRepository,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<RemoveClientCommand>
{
    public async Task Handle(RemoveClientCommand command, CancellationToken cancellationToken)
    {
        await clientRepository.DeleteAsync(command.Id, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
