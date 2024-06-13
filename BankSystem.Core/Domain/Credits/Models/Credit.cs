using BankSystem.Core.Common;
using BankSystem.Core.Domain.Cards.Models;
using BankSystem.Core.Domain.Credits.Data;
using BankSystem.Core.Domain.Credits.Validators;

namespace BankSystem.Core.Domain.Credits.Models;

public class Credit : Entity
{
    public Guid Id { get; private set; }

    public decimal InitialSum { get; private set; }

    public DateOnly CreditIssueDate { get; private set; }

    public byte PercentPerMonth { get; private set; }

    public Guid CardId { get; private set; }

    public Card Card { get; private set; }

    public static Credit CreateCredit(CreateCreditData data)
    {
        // todo validation
        Validate(new CreateCreditValidator(), data);

        return new Credit
        {
            Id = Guid.NewGuid(),
            InitialSum = data.InitialSum,
            CreditIssueDate = data.CreditIssueDate,
            PercentPerMonth = data.PercentPerMonth,
            CardId = data.CardId,
        };
    }

    public decimal CountCurrentCredit()
    {
        var currentDate = DateOnly.FromDateTime(DateTime.UtcNow);
        var yearDiff = currentDate.Year - CreditIssueDate.Year;
        var monthDiff = currentDate.Month - CreditIssueDate.Month;
        var sumPerMonth = InitialSum * PercentPerMonth / 100;

        return InitialSum + ((yearDiff * 12 + monthDiff) * sumPerMonth);
    }
}
