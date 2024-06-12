using BankSystem.Core.Common;
using BankSystem.Core.Domain.Cards.Common;
using BankSystem.Core.Domain.Cards.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Application.Domain.Cards.Commands.TakeCredit;

internal class TakeCreditCommandHandler(ICardRepository cardRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<TakeCreditCommand>
{
    public async Task Handle(TakeCreditCommand request, CancellationToken cancellationToken)
    {
        var card = await cardRepository.FindAsync(request.CardId, cancellationToken);
        var data = new TakeCreditData(request.Amount);

        card.TakeCredit(data);

        await unitOfWork.SaveChangesAsync();
    }
}
