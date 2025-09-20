using GigaChat.Backend.Domain.Entities.Core;

namespace GigaChat.Backend.Application.Repositories.Core;

public interface IMessageReactionRepository
{
    Task AddAsync(MessageReaction reaction, CancellationToken cancellationToken = default);
    Task UpdateAsync(MessageReaction reaction, CancellationToken cancellationToken = default);
    Task RemoveAsync(MessageReaction reaction, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MessageReaction>> GetReactionsForMessageAsync(Guid messageId, CancellationToken cancellationToken = default);
}