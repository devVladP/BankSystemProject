using BankSystem.Core.Domain.Cards.Common;
using BankSystem.Core.Domain.Cards.Data;
using BankSystem.Core.Domain.Cards.Models;
using BankSystem.Core.Domain.Cards.Rules;
using FluentValidation;

namespace BankSystem.Core.Domain.Cards.Validators;

internal class CreateClientCardValidator : AbstractValidator<CreateClientCardData>
{
    public CreateClientCardValidator(IClientMustExistChecker clientMustExistChecker,
    ICardMustExistChecker cardMustExistChecker)
    {
        RuleFor(x => x.cardId)
            .NotEmpty()
            .WithMessage("CardId is Required");

        RuleFor(x => x.clientId)
            .NotEmpty()
            .WithMessage("ClientId is Required");

        RuleFor(x => x.cardId)
            .CustomAsync(async (cardId, context, cancellationToken) =>
            {
                var ruleResult = await new CardMustExistRule(cardId, cardMustExistChecker).CheckAsync(cancellationToken);
                if (ruleResult.IsSuccess) return;
                foreach (var error in ruleResult.Errors) context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(Card), error));
            });

        RuleFor(x => x.clientId)
            .CustomAsync(async (clientId, context, cancellationToken) =>
            {
                var ruleResult = await new ClientMustExistRule(clientId, clientMustExistChecker).CheckAsync(cancellationToken);
                if (ruleResult.IsSuccess) return;
                foreach (var error in ruleResult.Errors) context.AddFailure(new FluentValidation.Results.ValidationFailure(nameof(Card), error));
            });
    }
}
