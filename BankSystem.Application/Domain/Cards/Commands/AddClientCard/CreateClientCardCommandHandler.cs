using BankSystem.Core.Common;
using BankSystem.Core.Domain.Cards.Common;
using BankSystem.Core.Domain.Cards.Data;
using BankSystem.Core.Domain.Cards.Models;
using MediatR;

namespace BankSystem.Application.Domain.Cards.Commands.AddClientCard;

internal class CreateClientCardCommandHandler(ICardClientRepository cardClientRepository,
    IClientMustExistChecker clientMustExistChecker,
    ICardMustExistChecker cardMustExistChecker,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateClientCardCommand>
{
    public async Task Handle(CreateClientCardCommand request, CancellationToken cancellationToken)
    {
        var data = new CreateClientCardData(request.CardId, request.ClientId);

        var clientCard = await ClientsCards.Create(clientMustExistChecker, cardMustExistChecker, data, cancellationToken);
        cardClientRepository.Add(clientCard);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
