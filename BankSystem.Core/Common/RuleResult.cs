namespace BankSystem.Core.Common;

public class RuleResult
{
    public RuleResult(bool isSuccess)
    {
        IsSuccess = isSuccess;
        Errors = new List<string>().AsReadOnly();
    }

    public RuleResult(bool isSuccess, IEnumerable<string> errors)
    {
        IsSuccess = isSuccess;
        Errors = errors.ToList().AsReadOnly();
    }

    public bool IsSuccess { get; }

    public bool IsFailed => !IsSuccess;

    public IReadOnlyCollection<string> Errors { get; }

    public static RuleResult Success()
    {
        return new RuleResult(true);
    }

    public static RuleResult Failure(params string[] errors)
    {
        return new RuleResult(false, errors);
    }

    public static RuleResult Failure(List<string> errors)
    {
        return new RuleResult(false, errors);
    }

    public static RuleResult Determine(List<string> errors)
    {
        var isSuccess = errors.All(string.IsNullOrEmpty);
        errors = isSuccess ? new List<string>() : errors;
        return new RuleResult(isSuccess, errors);
    }

    public static RuleResult Determine(params string[] errors)
    {
        var isSuccess = errors.All(string.IsNullOrEmpty);
        errors = isSuccess ? Array.Empty<string>() : errors;
        return new RuleResult(isSuccess, errors);
    }
}