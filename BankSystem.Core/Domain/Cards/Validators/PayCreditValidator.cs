using BankSystem.Core.Domain.Cards.Common;
using BankSystem.Core.Domain.Cards.Data;
using BankSystem.Core.Domain.Cards.Models;
using BankSystem.Core.Domain.Cards.Rules;
using BankSystem.Core.Domain.Credits.Common;
using BankSystem.Core.Domain.Credits.Models;
using BankSystem.Core.Domain.Credits.Rules;
using FluentValidation;
using System.Security.Cryptography.X509Certificates;

namespace BankSystem.Core.Domain.Cards.Validators;

internal class PayCreditValidator : AbstractValidator<PayCreditData>
{
    public PayCreditValidator(decimal Balance, ICardMustHaveCreditsChecker cardMustHaveCreditsChecker, 
        ICreditMustExistChecker creditMustExistChecker, 
        Guid creditId)
    {
        RuleFor(x => x.Amount)
            .NotEmpty()
            .LessThanOrEqualTo(Balance)
            .WithMessage($"Not enough money! You have: {Balance}.");

        RuleFor(x => x.CardId)
           .NotEmpty()
           .WithMessage("CardId is required");

        RuleFor(x => x.CreditId)
            .NotEmpty()
            .WithMessage("CardId is required");

        RuleFor(x => x.CardId)
        .CustomAsync(async (cardId, context, cancellationToken) =>
        {
                var ruleResult = await new CardMustHaveCreditsRule(cardId, creditId, cardMustHaveCreditsChecker).CheckAsync(cancellationToken);
                if (ruleResult.IsSuccess) return;
                foreach (var error in ruleResult.Errors) context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(Card), error));
        });

        RuleFor(x => x.CreditId)
        .CustomAsync(async (creditId, context, cancellationToken) =>
        {
            var ruleResult = await new CreditMustExistRule(creditId, creditMustExistChecker).CheckAsync(cancellationToken);
            if (ruleResult.IsSuccess) return;
            foreach (var error in ruleResult.Errors) context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(Card), error));
        });
    }
}
