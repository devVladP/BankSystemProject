namespace BankSystem.Core.Common;

/// <summary>
/// See https://enlabsoftware.com/development/how-to-implement-repository-unit-of-work-design-patterns-in-dot-net-core-practical-examples-part-one.html
/// https://dotnettutorials.net/lesson/unit-of-work-csharp-mvc/
/// https://www.programmingwithwolfgang.com/repository-and-unit-of-work-pattern/
/// </summary>
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
