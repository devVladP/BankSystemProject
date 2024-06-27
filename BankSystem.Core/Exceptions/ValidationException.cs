using FluentValidation.Results;

namespace BankSystem.Core.Exceptions;

public class ValidationException : DomainException
{
    public ValidationException(List<ValidationFailure> errors) : base("Validation is failed.")
    {
        Errors = errors;
    }

    public IReadOnlyCollection<ValidationFailure> Errors { get; }
}

