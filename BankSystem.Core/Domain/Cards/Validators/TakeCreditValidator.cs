using BankSystem.Core.Domain.Cards.Data;
using FluentValidation;

namespace BankSystem.Core.Domain.Cards.Validators;

public class TakeCreditValidator : AbstractValidator<TakeCreditData>
{
    public TakeCreditValidator()
    {
        RuleFor(x => x.Amount)
            .NotEmpty()
            .GreaterThanOrEqualTo(100)
            .LessThanOrEqualTo(100_000);

    }
}
