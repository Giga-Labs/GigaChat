using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Domain.Entities.Core;
using GigaChat.Backend.Infrastructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace GigaChat.Backend.Infrastructure.Repositories.Core;

public class MessageRepository(CoreDbContext coreDbContext) : IMessageRepository
{
    public async Task<Message?> FindByIdAsync(Guid messageId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.Messages
            .Include(m => m.Reactions)
            .Include(m => m.Receipts)
            .Include(m => m.EditHistory)
            .FirstOrDefaultAsync(m => m.Id == messageId, cancellationToken);
    }

    public async Task<IReadOnlyList<Message>> GetMessagesForConversationAsync(Guid conversationId, int take = 50, int skip = 0,
        CancellationToken cancellationToken = default)
    {
        return await coreDbContext.Messages
            .Include(m => m.Reactions)
            .Include(m => m.Receipts)
            .Include(m => m.EditHistory)
            .Where(m => m.ConversationId == conversationId)
            .OrderByDescending(m => m.CreatedAt)
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);

    }

    public async Task<IReadOnlyList<Message>> GetVisibleMessagesForUserAsync(string userId, Guid conversationId, int take = 50, int skip = 0,
        CancellationToken cancellationToken = default)
    {
        // get when the user last cleared this convo
        var clearedAt = await coreDbContext.ClearedConversations
            .Where(c => c.UserId == userId && c.ConversationId == conversationId)
            .Select(c => (DateTime?)c.ClearedAt)
            .FirstOrDefaultAsync(cancellationToken);

        // get list of message IDs the user deleted
        var deletedMessageIds = await coreDbContext.DeletedMessages
            .Where(dm => dm.UserId == userId)
            .Select(dm => dm.MessageId)
            .ToListAsync(cancellationToken);

        // build the base query
        var query = coreDbContext.Messages
            .Include(m => m.Reactions)
            .Include(m => m.Receipts)
            .Where(m => m.ConversationId == conversationId);

        if (clearedAt is not null)
        {
            query = query.Where(m => m.CreatedAt > clearedAt);
        }

        if (deletedMessageIds.Any())
        {
            query = query.Where(m => !deletedMessageIds.Contains(m.Id));
        }

        return await query
            .OrderByDescending(m => m.CreatedAt)
            .Skip(skip)
            .Take(take)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Message message, CancellationToken cancellationToken = default)
    {
        await coreDbContext.Messages.AddAsync(message, cancellationToken);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Message message, CancellationToken cancellationToken = default)
    {
        coreDbContext.Messages.Update(message);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(Message message, CancellationToken cancellationToken = default)
    {
        coreDbContext.Messages.Remove(message);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }
}