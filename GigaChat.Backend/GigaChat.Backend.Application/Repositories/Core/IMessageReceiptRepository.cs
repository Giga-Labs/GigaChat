using GigaChat.Backend.Domain.Entities.Core;

namespace GigaChat.Backend.Application.Repositories.Core;

public interface IMessageReceiptRepository
{
    Task AddAsync(MessageReceipt receipt, CancellationToken cancellationToken = default);
    Task<MessageReceipt?> FindByMessageAndUserAsync(Guid messageId, string userId, CancellationToken cancellationToken = default);
    Task UpdateAsync(MessageReceipt receipt, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MessageReceipt>> GetReceiptsForMessageAsync(Guid messageId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MessageReceipt>> GetReceiptsForUserAsync(string userId, Guid conversationId, CancellationToken cancellationToken = default);
}