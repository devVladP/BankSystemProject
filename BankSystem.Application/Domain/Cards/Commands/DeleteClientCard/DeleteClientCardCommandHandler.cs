using BankSystem.Core.Common;
using BankSystem.Core.Domain.Cards.Common;
using MediatR;


namespace BankSystem.Application.Domain.Cards.Commands.DeleteClientCard;

internal class DeleteClientCardCommandHandler(ICardClientRepository cardClientRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteClientCardCommand>
{
    public async Task Handle(DeleteClientCardCommand request, CancellationToken cancellationToken)
    {
        await cardClientRepository.DeleteAsync(request.cardId, request.clientId);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
