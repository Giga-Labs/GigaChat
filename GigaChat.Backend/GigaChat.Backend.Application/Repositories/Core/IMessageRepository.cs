using GigaChat.Backend.Domain.Entities.Core;

namespace GigaChat.Backend.Application.Repositories.Core;

public interface IMessageRepository
{
    Task<Message?> FindByIdAsync(Guid messageId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Message>> GetMessagesForConversationAsync(Guid conversationId, int take = 50, int skip = 0, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Message>> GetVisibleMessagesForUserAsync(string userId, Guid conversationId, int take = 50, int skip = 0, CancellationToken cancellationToken = default);
    Task AddAsync(Message message, CancellationToken cancellationToken = default);
    Task UpdateAsync(Message message, CancellationToken cancellationToken = default);
    Task RemoveAsync(Message message, CancellationToken cancellationToken = default);
}