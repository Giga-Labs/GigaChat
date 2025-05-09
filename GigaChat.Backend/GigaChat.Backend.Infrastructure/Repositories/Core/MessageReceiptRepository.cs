using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Domain.Entities.Core;
using GigaChat.Backend.Infrastructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace GigaChat.Backend.Infrastructure.Repositories.Core;

public class MessageReceiptRepository(CoreDbContext coreDbContext) : IMessageReceiptRepository
{
    public async Task AddAsync(MessageReceipt receipt, CancellationToken cancellationToken = default)
    {
        await coreDbContext.MessageReceipts.AddAsync(receipt, cancellationToken);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<MessageReceipt?> FindByMessageAndUserAsync(Guid messageId, string userId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.MessageReceipts
            .FirstOrDefaultAsync(r => r.MessageId == messageId && r.UserId == userId, cancellationToken);
    }

    public async Task UpdateAsync(MessageReceipt receipt, CancellationToken cancellationToken = default)
    {
        coreDbContext.MessageReceipts.Update(receipt);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<MessageReceipt>> GetReceiptsForMessageAsync(Guid messageId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.MessageReceipts
            .Where(r => r.MessageId == messageId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<MessageReceipt>> GetReceiptsForUserAsync(string userId, Guid conversationId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.MessageReceipts
            .Where(r => r.UserId == userId && coreDbContext.Messages.Any(m => m.Id == r.MessageId && m.ConversationId == conversationId))
            .ToListAsync(cancellationToken);
    }
}