using GigaChat.Backend.Domain.Entities.Core;

namespace GigaChat.Backend.Application.Repositories.Core;

public interface IMessageEditHistoryRepository
{
    Task AddAsync(MessageEditHistory editHistory, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<MessageEditHistory>> GetEditsForMessageAsync(Guid messageId, CancellationToken cancellationToken = default);
}