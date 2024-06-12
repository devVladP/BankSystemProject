using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Core.Exceptions;

internal class RuleValidationException(IEnumerable<string> errors) : DomainException("Validation is failed.")
{
    public IReadOnlyCollection<string> Errors { get; } = errors.ToList().AsReadOnly();
}
