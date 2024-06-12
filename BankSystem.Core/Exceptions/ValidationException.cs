using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Core.Exceptions;

public class ValidationException : DomainException
{
    public ValidationException(List<ValidationFailure> errors) : base("Validation is failed.")
    {
        Errors = errors;
    }

    public IReadOnlyCollection<ValidationFailure> Errors { get; }
}

