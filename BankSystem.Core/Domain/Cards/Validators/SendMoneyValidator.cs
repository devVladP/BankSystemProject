using BankSystem.Core.Domain.Cards.Data;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Core.Domain.Cards.Validators;

internal class SendMoneyValidator : AbstractValidator<SendMoneyData>
{
    public SendMoneyValidator()
    {
        RuleFor(x => x.CardSender)
            .NotEmpty()
            .NotEqual(x => x.CardReceiver);

        RuleFor(x => x.CardReceiver)
            .NotEmpty();

        RuleFor(x => x.TotalMoney)
            .NotEmpty()
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(x => x.CardSender.Balance);
    }
}
