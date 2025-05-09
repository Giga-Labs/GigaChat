using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Domain.Entities.Core;
using GigaChat.Backend.Infrastructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace GigaChat.Backend.Infrastructure.Repositories.Core;

public class MessageReactionRepository(CoreDbContext coreDbContext) : IMessageReactionRepository
{
    public async Task AddAsync(MessageReaction reaction, CancellationToken cancellationToken = default)
    {
        await coreDbContext.MessageReactions.AddAsync(reaction, cancellationToken);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(MessageReaction reaction, CancellationToken cancellationToken = default)
    {
        coreDbContext.MessageReactions.Update(reaction);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(MessageReaction reaction, CancellationToken cancellationToken = default)
    {
        coreDbContext.MessageReactions.Remove(reaction);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<MessageReaction>> GetReactionsForMessageAsync(Guid messageId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.MessageReactions
            .Where(r => r.MessageId == messageId)
            .ToListAsync(cancellationToken);
    }
}