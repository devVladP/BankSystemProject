using BankSystem.Core.Common;
using BankSystem.Core.Domain.Cards.Common;
using BankSystem.Core.Domain.Cards.Data;
using BankSystem.Core.Domain.Credits.Common;
using BankSystem.Core.Domain.Credits.Data;
using BankSystem.Core.Domain.Credits.Models;
using MediatR;

namespace BankSystem.Application.Domain.Credits.Commands.CreateCredit;

internal class CreateCreditCommandHandler(
    IUnitOfWork unitOfWork,
    ICreditRepository creditRepository,
    ICardRepository cardRepository,
    ICardMustExistChecker cardMustExistChecker
    ) : IRequestHandler<CreateCreditCommand, Guid>
{
    public async Task<Guid> Handle(CreateCreditCommand request, CancellationToken cancellationToken)
    {
        var dataCredit = new CreateCreditData(request.InitialSum, request.CreditIssueDate, request.PercentPerMonth, request.CardId);

        var card = await cardRepository.FindAsync(request.CardId, cancellationToken);
        var credit = Credit.CreateCredit(dataCredit);

        await card.TakeCreditAsync(new TakeCreditData(request.InitialSum, request.CardId), 
            cardMustExistChecker, 
            cancellationToken);

        creditRepository.Add(credit);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return credit.Id;
    }
}
