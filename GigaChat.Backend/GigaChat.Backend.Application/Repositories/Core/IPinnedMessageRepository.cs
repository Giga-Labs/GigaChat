using GigaChat.Backend.Domain.Entities.Core;

namespace GigaChat.Backend.Application.Repositories.Core;

public interface IPinnedMessageRepository
{
    Task AddAsync(PinnedMessage pinned, CancellationToken cancellationToken = default);
    Task<PinnedMessage?> FindByMessageIdAsync(Guid messageId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<PinnedMessage>> GetPinnedMessagesForConversationAsync(Guid conversationId, CancellationToken cancellationToken = default);
    Task RemoveAsync(PinnedMessage message, CancellationToken cancellationToken = default);
}