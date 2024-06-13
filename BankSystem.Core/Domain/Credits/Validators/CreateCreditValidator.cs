using BankSystem.Core.Domain.Cards.Data;
using BankSystem.Core.Domain.Credits.Data;
using FluentValidation;

namespace BankSystem.Core.Domain.Credits.Validators;

internal class CreateCreditValidator : AbstractValidator<CreateCreditData>
{
    public CreateCreditValidator()
    {
        RuleFor(x => x.CreditIssueDate)
            .NotEmpty()
            .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow));

        RuleFor(x => x.PercentPerMonth)
            .NotEmpty()
            .GreaterThanOrEqualTo((byte)1)
            .LessThanOrEqualTo((byte)100);

        RuleFor(x => x.InitialSum)
            .NotEmpty();

        RuleFor(x => x.CardId)
            .NotEmpty()
            .WithMessage($"CardId is required");
    }
}
