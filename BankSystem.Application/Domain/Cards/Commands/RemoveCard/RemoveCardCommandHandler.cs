using BankSystem.Core.Common;
using BankSystem.Core.Domain.Cards.Common;
using MediatR;

namespace BankSystem.Application.Domain.Cards.Commands.RemoveCard;

public class RemoveCardCommandHandler(
    ICardRepository cardRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<RemoveCardCommand>
{
    public async Task Handle(RemoveCardCommand command, CancellationToken cancellationToken)
    {
        await cardRepository.Delete(command.Id, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
