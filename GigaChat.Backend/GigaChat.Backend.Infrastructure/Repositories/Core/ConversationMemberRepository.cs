using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Domain.Entities.Core;
using GigaChat.Backend.Infrastructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace GigaChat.Backend.Infrastructure.Repositories.Core;

public class ConversationMemberRepository(CoreDbContext coreDbContext) : IConversationMemberRepository
{
    public async Task<bool> IsMemberAsync(string userId, Guid conversationId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.ConversationMembers
            .AnyAsync(cm => cm.UserId == userId && cm.ConversationId == conversationId, cancellationToken);
    }

    public async Task<bool> IsAdminAsync(string userId, Guid conversationId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.ConversationMembers
            .AnyAsync(cm => cm.UserId == userId && cm.ConversationId == conversationId && cm.IsAdmin, cancellationToken);
    }

    public async Task<IReadOnlyList<ConversationMember>> GetMembersAsync(Guid conversationId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.ConversationMembers
            .Where(cm => cm.ConversationId == conversationId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Guid>> GetConversationIdsForUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.ConversationMembers
            .Where(cm => cm.UserId == userId)
            .Select(cm => cm.ConversationId)
            .ToListAsync(cancellationToken);
    }

    public async Task<ConversationMember?> FindAsync(string userId, Guid conversationId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.ConversationMembers
            .FirstOrDefaultAsync(cm => cm.UserId == userId && cm.ConversationId == conversationId, cancellationToken);
    }

    public async Task AddAsync(ConversationMember member, CancellationToken cancellationToken = default)
    {
        await coreDbContext.ConversationMembers.AddAsync(member, cancellationToken);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(ConversationMember member, CancellationToken cancellationToken = default)
    {
        coreDbContext.ConversationMembers.Update(member);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(ConversationMember member, CancellationToken cancellationToken = default)
    {
        coreDbContext.ConversationMembers.Remove(member);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }
}