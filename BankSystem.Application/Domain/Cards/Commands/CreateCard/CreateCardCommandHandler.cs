using BankSystem.Core.Common;
using BankSystem.Core.Domain.Cards.Common;
using BankSystem.Core.Domain.Cards.Data;
using BankSystem.Core.Domain.Cards.Models;
using MediatR;

namespace BankSystem.Application.Domain.Cards.Commands.CreateCard;

internal class CreateCardCommandHandler(
    ICardRepository cardRepository,
    IUnitOfWork unitOfWork,
    ICardNumberMustBeUniqueChecker cardNumberMustBeUniqueChecker
    ) : IRequestHandler<CreateCardCommand, Guid>
{
    public async Task<Guid> Handle(CreateCardCommand command, CancellationToken cancellationToken)
    {
        var data = new CreateCardData(
            command.Number,
            command.IssueDate,
            command.ExpireDate,
            command.Balance,
            command.PaymentSystem
            );

        var card = await Card.CreateAsync(data, cardNumberMustBeUniqueChecker);

        cardRepository.Add(card);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return card.Id;
    }
}
