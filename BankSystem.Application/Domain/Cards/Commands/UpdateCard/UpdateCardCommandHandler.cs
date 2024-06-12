using BankSystem.Core.Common;
using BankSystem.Core.Domain.Cards.Common;
using BankSystem.Core.Domain.Cards.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Domain.Cards.Commands.UpdateCard;

internal class UpdateCardCommandHandler(
    ICardRepository cardRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateCardCommand>
{
    public async Task Handle(UpdateCardCommand command, CancellationToken cancellationToken)
    {
        var card = await cardRepository.FindAsync(command.Id, cancellationToken);

        var data = new UpdateCardData(
            command.Number,
            command.CVV2,
            command.IssueDate,
            command.ExpirationDate,
            command.Balance,
            command.Credit);

        card.Update(data);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
