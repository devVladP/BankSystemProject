using BankSystem.Core.Common;
using BankSystem.Core.Domain.Cards.Common;
using BankSystem.Core.Domain.Cards.Data;
using BankSystem.Core.Domain.Cards.Validators;

namespace BankSystem.Core.Domain.Cards.Models;

public class Card : Entity
{
    private readonly List<ClientsCards> _clientsCards = new();

    public Guid Id { get; private set; }

    public string Number { get; private set; }

    public string CVV2 { get; set; }

    public DateOnly IssueDate { get; set; }

    public DateOnly ExpirationDate { get; set; }

    public decimal Balance { get; private set; }

    public string PaymentSystem { get; set; }

    public decimal Credit { get; private set; }

    public IReadOnlyCollection<ClientsCards> ClientsCards => _clientsCards;

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

    public void TakeCredit(TakeCreditData data)
    {
        Validate(new TakeCreditValidator(), data);

        Credit += data.Amount;
        Balance += data.Amount;
    }

    public void PayCredit(PayCreditData data)
    {
        Validate(new PayCreditValidator(Balance), data);

        Credit -= data.Amount;
        Balance -= data.Amount;
    }
}
