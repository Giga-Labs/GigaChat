using GigaChat.Backend.Domain.Entities.Core;

namespace GigaChat.Backend.Application.Repositories.Core;

public interface IConversationRepository
{
    Task<Conversation?> FindByIdAsync(Guid conversationId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Conversation>> GetAllForUserAsync(string userId, CancellationToken cancellationToken = default);
    Task AddAsync(Conversation conversation, CancellationToken cancellationToken = default);
    Task UpdateAsync(Conversation conversation, CancellationToken cancellationToken = default);
    Task RemoveAsync(Conversation conversation, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid conversationId, CancellationToken cancellationToken = default);
    Task<Conversation?> FindOneToOneAsync(string userId1, string userId2, CancellationToken cancellationToken = default);
}