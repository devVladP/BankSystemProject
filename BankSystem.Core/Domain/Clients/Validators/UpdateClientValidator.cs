using BankSystem.Core.Domain.Clients.Data;
using FluentValidation;

namespace BankSystem.Core.Domain.Clients.Validators;

internal class UpdateClientValidator : AbstractValidator<UpdateClientData>
{
    public UpdateClientValidator() 
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.MiddleName)
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email must be not empty;")
            .EmailAddress().WithMessage("Email must be valid;");
    }
}
