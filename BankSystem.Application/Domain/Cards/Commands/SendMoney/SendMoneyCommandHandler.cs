using BankSystem.Core.Common;
using BankSystem.Core.Domain.Cards.Common;
using BankSystem.Core.Domain.Cards.Data;
using BankSystem.Core.Domain.Cards.Models;
using MediatR;

namespace BankSystem.Application.Domain.Cards.Commands.SendMoney;

internal class SendMoneyCommandHandler(
    ICardRepository cardRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<SendMoneyCommand>
{
    public async Task Handle(SendMoneyCommand command, CancellationToken cancellationToken)
    {
        var cardSender = await cardRepository.FindAsync(command.CardSenderId, cancellationToken);
        var cardReceiver = await cardRepository.FindAsync(command.CardReceiverId, cancellationToken);

        var data = new SendMoneyData(cardSender, cardReceiver, command.TotalMoneyToSend);

        Card.SendMoney(data);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
