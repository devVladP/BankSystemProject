namespace BankSystem.Core.Domain.Cards.Common;

public interface ICardMustExistChecker
{
    Task<bool> CheckCardMustExistAsync(Guid id, CancellationToken cancellationToken = default);
}
