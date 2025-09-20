using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Domain.Entities.Core;
using GigaChat.Backend.Infrastructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace GigaChat.Backend.Infrastructure.Repositories.Core;

public class PinnedMessageRepository(CoreDbContext coreDbContext) : IPinnedMessageRepository
{
    public async Task AddAsync(PinnedMessage pinned, CancellationToken cancellationToken = default)
    {
        await coreDbContext.PinnedMessages.AddAsync(pinned, cancellationToken);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<PinnedMessage?> FindByMessageIdAsync(Guid messageId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.PinnedMessages
            .Include(p => p.Message)
            .FirstOrDefaultAsync(p => p.MessageId == messageId, cancellationToken);
    }

    public async Task<IReadOnlyList<PinnedMessage>> GetPinnedMessagesForConversationAsync(Guid conversationId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.PinnedMessages
            .Include(p => p.Message)
            .Where(p => p.Message.ConversationId == conversationId)
            .OrderByDescending(p => p.PinnedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task RemoveAsync(PinnedMessage message, CancellationToken cancellationToken = default)
    {
        coreDbContext.PinnedMessages.Remove(message);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }
}