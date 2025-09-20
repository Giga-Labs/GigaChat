using GigaChat.Backend.Domain.Entities.Core;

namespace GigaChat.Backend.Application.Repositories.Core;

public interface IDeletedMessageRepository
{
    Task AddAsync(DeletedMessage entry, CancellationToken cancellationToken = default);
    Task<bool> IsMessageDeletedForUserAsync(Guid messageId, string userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Guid>> GetDeletedMessageIdsForUserAsync(string userId, Guid conversationId, CancellationToken cancellationToken = default);
}