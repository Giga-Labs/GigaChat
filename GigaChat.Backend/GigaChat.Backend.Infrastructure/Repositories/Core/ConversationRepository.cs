using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Domain.Entities.Core;
using GigaChat.Backend.Infrastructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace GigaChat.Backend.Infrastructure.Repositories.Core;

public class ConversationRepository(CoreDbContext coreDbContext) : IConversationRepository
{
    public async Task<Conversation?> FindByIdAsync(Guid conversationId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.Conversations
            .Include(c => c.Members)
            .FirstOrDefaultAsync(c => c.Id == conversationId, cancellationToken);
    }

    public async Task<IReadOnlyList<Conversation>> GetAllForUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.Conversations
            .Where(c => c.Members.Any(m => m.UserId == userId))
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Conversation conversation, CancellationToken cancellationToken = default)
    {
        await coreDbContext.Conversations.AddAsync(conversation, cancellationToken);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Conversation conversation, CancellationToken cancellationToken = default)
    {
        coreDbContext.Conversations.Update(conversation);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(Conversation conversation, CancellationToken cancellationToken = default)
    {
        coreDbContext.Conversations.Remove(conversation);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid conversationId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.Conversations
            .AnyAsync(c => c.Id == conversationId, cancellationToken);
    }

    public async Task<Conversation?> FindOneToOneAsync(string userId1, string userId2, CancellationToken cancellationToken = default)
    {
        var userIds = new[] { userId1, userId2 };

        var candidateConversations = await coreDbContext.Conversations
            .Where(c => !c.IsGroup)
            .Join(coreDbContext.ConversationMembers,
                c => c.Id,
                cm => cm.ConversationId,
                (c, cm) => new { c, cm.UserId })
            .GroupBy(x => x.c)
            .Where(g => g.Count() == 2 && userIds.All(id => g.Any(x => x.UserId == id)))
            .Select(g => g.Key)
            .ToListAsync(cancellationToken);

        return candidateConversations.FirstOrDefault();
    }
}