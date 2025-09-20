using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Domain.Entities.Core;
using GigaChat.Backend.Infrastructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace GigaChat.Backend.Infrastructure.Repositories.Core;

public class MessageEditHistoryRepository(CoreDbContext coreDbContext) : IMessageEditHistoryRepository
{
    public async Task AddAsync(MessageEditHistory editHistory, CancellationToken cancellationToken = default)
    {
        await coreDbContext.MessageEditHistories.AddAsync(editHistory, cancellationToken);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<MessageEditHistory>> GetEditsForMessageAsync(Guid messageId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.MessageEditHistories
            .Where(e => e.MessageId == messageId)
            .OrderByDescending(e => e.EditedAt)
            .ToListAsync(cancellationToken);
    }
}