using BankSystem.Core.Common;
using BankSystem.Core.Domain.Cards.Common;
using BankSystem.Core.Domain.Cards.Data;
using MediatR;

namespace BankSystem.Application.Domain.Cards.Commands.PayCredit;

internal class PayCreditCommandHandler(ICardRepository cardRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<PayCreditCommand>
{
    public async Task Handle(PayCreditCommand request, CancellationToken cancellationToken)
    {
        var card = await cardRepository.FindAsync(request.cardId, cancellationToken);
        var data = new PayCreditData(request.Amount);

        card.PayCredit(data);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
