using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Domain.Entities.Core;
using GigaChat.Backend.Infrastructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace GigaChat.Backend.Infrastructure.Repositories.Core;

public class ConversationInviteLogRepository(CoreDbContext coreDbContext) : IConversationInviteLogRepository
{
    public async Task<ConversationInviteLog?> FindInviteByIdAsync(Guid conversationId, string inviteeId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.ConversationInviteLogs
            .FirstOrDefaultAsync(x => x.ConversationId == conversationId && x.InviteeId == inviteeId, cancellationToken);
    }

    public async Task<IReadOnlyList<ConversationInviteLog>> GetInvitesForUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.ConversationInviteLogs
            .Where(x => x.InviteeId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ConversationInviteLog>> GetInvitesSentByUserAsync(string inviterId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.ConversationInviteLogs
            .Where(x => x.InviterId == inviterId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(ConversationInviteLog inviteLog, CancellationToken cancellationToken = default)
    {
        await coreDbContext.ConversationInviteLogs.AddAsync(inviteLog, cancellationToken);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task AddRangeAsync(IEnumerable<ConversationInviteLog> logs, CancellationToken cancellationToken = default)
    {
        await coreDbContext.ConversationInviteLogs.AddRangeAsync(logs, cancellationToken);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ConversationInviteLog inviteLog, CancellationToken cancellationToken = default)
    {
        coreDbContext.Update(inviteLog);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task RemoveAsync(ConversationInviteLog inviteLog, CancellationToken cancellationToken = default)
    {
        coreDbContext.Remove(inviteLog);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<ConversationInviteLog?> GetByUserAndConversationAsync(string userId, Guid conversationId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.ConversationInviteLogs
            .FirstOrDefaultAsync(invite =>
                    invite.ConversationId == conversationId &&
                    invite.InviteeId == userId,
                cancellationToken);
    }
}