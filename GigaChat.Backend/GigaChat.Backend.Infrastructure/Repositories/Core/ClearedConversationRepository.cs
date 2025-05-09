using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Domain.Entities.Core;
using GigaChat.Backend.Infrastructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace GigaChat.Backend.Infrastructure.Repositories.Core;

public class ClearedConversationRepository(CoreDbContext coreDbContext) : IClearedConversationRepository
{
    public async Task<ClearedConversation?> FindByUserAndConversation(string userId, Guid conversationId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.ClearedConversations
            .FirstOrDefaultAsync(c => c.UserId == userId && c.ConversationId == conversationId, cancellationToken);
    }

    public async Task<IReadOnlyList<Guid>> GetClearedConversationIdsForUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.ClearedConversations
            .Where(c => c.UserId == userId)
            .Select(c => c.ConversationId)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> IsClearedAsync(string userId, Guid conversationId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.ClearedConversations
            .AnyAsync(c => c.UserId == userId && c.ConversationId == conversationId, cancellationToken);
    }

    public async Task AddAsync(ClearedConversation entry, CancellationToken cancellationToken = default)
    {
        await coreDbContext.ClearedConversations.AddAsync(entry, cancellationToken);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(ClearedConversation entry, CancellationToken cancellationToken = default)
    {
        coreDbContext.ClearedConversations.Remove(entry);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }
}