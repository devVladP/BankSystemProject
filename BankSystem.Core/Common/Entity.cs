using BankSystem.Core.Exceptions;
using FluentValidation;
using FluentValidation.Results;


namespace BankSystem.Core.Common
{
    public abstract class Entity
    {
        protected static void Validate<T> (AbstractValidator<T> validator, T entity)
        {
            var validationResult = validator.Validate(entity);
            ThrowIfNotValid(validationResult);
        }

        protected static async Task ValidateAsync<T>(AbstractValidator<T> validator, T entity, CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(entity);
            ThrowIfNotValid(validationResult);
        }

        protected static void CheckRule(IBusinessRule rule)
        {
            var ruleResult = rule.Check();
            if (ruleResult.IsFailed) throw new RuleValidationException(ruleResult.Errors);
        }

        private static void ThrowIfNotValid(ValidationResult validationResult)
        {
            if (!validationResult.IsValid) throw new Exceptions.ValidationException(validationResult.Errors);
        }
    }
}
