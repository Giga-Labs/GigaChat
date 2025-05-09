using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Domain.Entities.Core;
using GigaChat.Backend.Infrastructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace GigaChat.Backend.Infrastructure.Repositories.Core;

public class DeletedMessageRepository(CoreDbContext coreDbContext) : IDeletedMessageRepository
{
    public async Task AddAsync(DeletedMessage entry, CancellationToken cancellationToken = default)
    {
        await coreDbContext.DeletedMessages.AddAsync(entry, cancellationToken);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsMessageDeletedForUserAsync(Guid messageId, string userId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.DeletedMessages
            .AnyAsync(dm => dm.MessageId == messageId && dm.UserId == userId, cancellationToken);
    }

    public async Task<IReadOnlyList<Guid>> GetDeletedMessageIdsForUserAsync(string userId, Guid conversationId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.DeletedMessages
            .Where(dm => dm.UserId == userId)
            .Where(dm => coreDbContext.Messages.Any(m => m.Id == dm.MessageId && m.ConversationId == conversationId))
            .Select(dm => dm.MessageId)
            .ToListAsync(cancellationToken);
    }
}