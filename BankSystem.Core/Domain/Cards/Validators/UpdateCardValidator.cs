using BankSystem.Core.Domain.Cards.Data;
using FluentValidation;

namespace BankSystem.Core.Domain.Cards.Validators;

public class UpdateCardValidator : AbstractValidator<UpdateCardData>
{
    public UpdateCardValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty()
            .Length(16);

        RuleFor(x => x.CVV2)
            .NotEmpty()
            .Length(3);

        RuleFor(x => x.IssueDate)
                .NotEmpty()
                .LessThan(x => x.ExpirationDate);

        RuleFor(x => x.ExpirationDate)
            .NotEmpty()
            .GreaterThan(x => x.IssueDate);
            
        RuleFor(x => x.Balance)
            .NotEmpty()
            .GreaterThanOrEqualTo(0);
    }
}