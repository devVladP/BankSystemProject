using BankSystem.Core.Domain.Cards.Data;
using FluentValidation;

namespace BankSystem.Core.Domain.Cards.Validators;

internal class RemoveCardValidator : AbstractValidator<RemoveCardData>
{
    public RemoveCardValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
