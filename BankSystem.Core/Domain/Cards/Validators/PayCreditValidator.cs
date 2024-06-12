using BankSystem.Core.Domain.Cards.Data;
using FluentValidation;

namespace BankSystem.Core.Domain.Cards.Validators;

internal class PayCreditValidator : AbstractValidator<PayCreditData>
{
    public PayCreditValidator(decimal Balance)
    {
        RuleFor(x => x.Amount)
            .NotEmpty()
            .LessThanOrEqualTo(Balance);
    }
}
