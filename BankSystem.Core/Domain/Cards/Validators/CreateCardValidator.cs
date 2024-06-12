using BankSystem.Core.Domain.Cards.Common;
using BankSystem.Core.Domain.Cards.Data;
using BankSystem.Core.Domain.Cards.Models;
using BankSystem.Core.Domain.Cards.Rules;
using FluentValidation;
using FluentValidation.Results;

namespace BankSystem.Core.Domain.Cards.Validators;

public class CreateCardValidator : AbstractValidator<CreateCardData>
{
    public CreateCardValidator(ICardNumberMustBeUniqueChecker cardNumberMustBeUniqueChecker) 
    {
        RuleFor(x => x.Number)
            .NotEmpty()
            .Length(16);

        RuleFor(x => x.Number)
            .CustomAsync(async (cardNumber, context, cancellationToken) =>
            {
                var ruleResult = await new CardNumberMustBeUniqueRule(cardNumber, cardNumberMustBeUniqueChecker).CheckAsync(cancellationToken);
                if (ruleResult.IsSuccess) return;
                foreach (var error in ruleResult.Errors) context.AddFailure(new ValidationFailure(nameof(Card.Number), error));
            });

        RuleFor(x => x.IssueDate)
            .NotEmpty()
            .LessThan(x => x.ExpirationDate);

        RuleFor(x => x.ExpirationDate)
            .NotEmpty()
            .GreaterThan(x => x.IssueDate);

        RuleFor(x => x.Balance)
            .GreaterThanOrEqualTo(0);

    }
}
