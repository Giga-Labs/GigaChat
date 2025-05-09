using GigaChat.Backend.Domain.Entities.Core;

namespace GigaChat.Backend.Application.Repositories.Core;

public interface IConversationInviteLogRepository
{
    Task<ConversationInviteLog?> FindInviteByIdAsync(Guid conversationId, string inviteeId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ConversationInviteLog>> GetInvitesForUserAsync(string userId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<ConversationInviteLog>> GetInvitesSentByUserAsync(string inviterId, CancellationToken cancellationToken = default);

    Task AddAsync(ConversationInviteLog inviteLog, CancellationToken cancellationToken = default);   

    Task UpdateAsync(ConversationInviteLog inviteLog, CancellationToken cancellationToken = default);
}