using BankSystem.Core.Common;
using BankSystem.Core.Domain.Cards.Common;
using BankSystem.Core.Domain.Cards.Data;
using BankSystem.Core.Domain.Credits.Common;
using MediatR;

namespace BankSystem.Application.Domain.Credits.Commands.PayCredit;

internal class PayCreditCommandHandler(
    ICreditRepository creditRepository,
    ICardRepository cardRepository,
    ICardMustHaveCreditsChecker cardMustHaveCreditsChecker,
    ICreditMustExistChecker creditMustExistChecker,
    IUnitOfWork unitOfWork
    ) : IRequestHandler<PayCreditCommand>
{
    public async Task Handle(PayCreditCommand request, CancellationToken cancellationToken)
    {
        var card = await cardRepository.FindAsync(request.CardId, cancellationToken);
        var credit = await creditRepository.FindAsync(request.Id, cancellationToken);
        
        await card.PayCreditAsync(new PayCreditData(credit.CountCurrentCredit(), request.CardId, request.Id), 
            cardMustHaveCreditsChecker,
            creditMustExistChecker,
            cancellationToken);

        await creditRepository.DeleteAsync(request.Id, cancellationToken);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}

