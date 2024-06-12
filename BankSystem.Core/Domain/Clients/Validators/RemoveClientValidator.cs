using BankSystem.Core.Domain.Clients.Data;
using FluentValidation;

namespace BankSystem.Core.Domain.Clients.Validators;

internal class RemoveClientValidator : AbstractValidator<RemoveClientData>
{
    public RemoveClientValidator() {
        RuleFor(x => x.Id)
            .NotEmpty();
    }
}
