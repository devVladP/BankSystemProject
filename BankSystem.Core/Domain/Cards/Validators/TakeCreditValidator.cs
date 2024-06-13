using BankSystem.Core.Domain.Cards.Common;
using BankSystem.Core.Domain.Cards.Data;
using BankSystem.Core.Domain.Cards.Models;
using BankSystem.Core.Domain.Cards.Rules;
using FluentValidation;

namespace BankSystem.Core.Domain.Cards.Validators;

public class TakeCreditValidator : AbstractValidator<TakeCreditData>
{
    public TakeCreditValidator(ICardMustExistChecker cardMustExistChecker)
    {
        RuleFor(x => x.Amount)
            .NotEmpty()
            .GreaterThanOrEqualTo(1)
            .LessThanOrEqualTo(100_000);

        RuleFor(x => x.CardId)
            .NotEmpty()
            .WithMessage("CardId is required");

        RuleFor(x => x.CardId)
            .CustomAsync(async (cardId, context, cancellationToken) =>
            {
                var ruleResult = await new CardMustExistRule(cardId, cardMustExistChecker).CheckAsync(cancellationToken);
                if (ruleResult.IsSuccess) return;
                foreach (var error in ruleResult.Errors) context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(Card), error));
            });
    }
}
