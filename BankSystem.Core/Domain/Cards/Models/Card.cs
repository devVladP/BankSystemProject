using BankSystem.Core.Common;
using BankSystem.Core.Domain.Cards.Common;
using BankSystem.Core.Domain.Cards.Data;
using BankSystem.Core.Domain.Cards.Validators;
using BankSystem.Core.Domain.Credits.Common;
using BankSystem.Core.Domain.Credits.Models;

namespace BankSystem.Core.Domain.Cards.Models;

public class Card : Entity
{
    private readonly List<ClientsCards> _clientsCards = [];
    private readonly List<Credit> _credits = [];


    public Guid Id { get; private set; }

    public string Number { get; private set; }

    public string CVV2 { get; set; }

    public DateOnly IssueDate { get; set; }

    public DateOnly ExpirationDate { get; set; }

    public decimal Balance { get; private set; }

    public string PaymentSystem { get; set; }

    public IReadOnlyCollection<ClientsCards> ClientsCards => _clientsCards;

    public IReadOnlyCollection<Credit> Credits => _credits;

    public static async Task<Card> CreateAsync(CreateCardData data, 
        ICardNumberMustBeUniqueChecker cardNumberMustBeUniqueChecker, CancellationToken cancellationToken = default)
    {
        await ValidateAsync(new CreateCardValidator(cardNumberMustBeUniqueChecker), data, cancellationToken);
        var rndm = new Random();
        var CVV2Number = rndm.Next(1, 1000);

        return new Card
        {
            Id = Guid.NewGuid(),
            Number = data.Number,
            CVV2 = CVV2Number.ToString("000"),
            IssueDate = data.IssueDate,
            ExpirationDate = data.ExpirationDate,
            Balance = data.Balance,
            PaymentSystem = data.PaymentSystem
        };
    }

    public static Guid Delete(RemoveCardData data)
    {
        Validate(new RemoveCardValidator(), data);

        return data.Id;
    }

    public void Update(UpdateCardData data)
    {
        Validate(new UpdateCardValidator(), data);

        Number = data.Number;
        CVV2 = data.CVV2;
        IssueDate = data.IssueDate;
        ExpirationDate = data.ExpirationDate;
        Balance = data.Balance;
    }

    public static void SendMoney(SendMoneyData data)
    {
        Validate(new SendMoneyValidator(), data);

        data.CardSender.Balance -= data.TotalMoney;
        data.CardReceiver.Balance += data.TotalMoney;
    }

    public async Task TakeCreditAsync(TakeCreditData data,
        ICardMustExistChecker cardMustExistChecker,
        CancellationToken cancellationToken = default)
    {
        await ValidateAsync(new TakeCreditValidator(cardMustExistChecker), data, cancellationToken);

        Balance += data.Amount;
    }

    public async Task PayCreditAsync(PayCreditData data, 
        ICardMustHaveCreditsChecker cardMustHaveCreditsChecker,
        ICreditMustExistChecker creditMustExistChecker,
        CancellationToken cancellationToken = default)
    {
        await ValidateAsync(new PayCreditValidator(Balance, cardMustHaveCreditsChecker, creditMustExistChecker, data.CreditId), data, cancellationToken);

        Balance -= data.Amount;
    }
}
