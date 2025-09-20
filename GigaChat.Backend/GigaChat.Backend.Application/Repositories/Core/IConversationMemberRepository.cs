using GigaChat.Backend.Domain.Entities.Core;

namespace GigaChat.Backend.Application.Repositories.Core;

public interface IConversationMemberRepository
{
    Task<bool> IsMemberAsync(string userId, Guid conversationId, CancellationToken cancellationToken = default);
    Task<bool> IsAdminAsync(string userId, Guid conversationId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ConversationMember>> GetMembersAsync(Guid conversationId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Guid>> GetConversationIdsForUserAsync(string userId, CancellationToken cancellationToken = default);
    Task<ConversationMember?> FindAsync(string userId, Guid conversationId, CancellationToken cancellationToken = default);
    Task AddAsync(ConversationMember member, CancellationToken cancellationToken = default);
    Task AddRangeAsync(IEnumerable<ConversationMember> members, CancellationToken cancellationToken = default);
    Task UpdateAsync(ConversationMember member, CancellationToken cancellationToken = default);
    Task RemoveAsync(ConversationMember member, CancellationToken cancellationToken = default);
}