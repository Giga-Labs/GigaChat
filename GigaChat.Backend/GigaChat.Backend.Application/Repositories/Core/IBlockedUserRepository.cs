using GigaChat.Backend.Domain.Entities.Core;

namespace GigaChat.Backend.Application.Repositories.Core;

public interface IBlockedUserRepository
{
    Task<BlockedUser?> GetAsync(string userId, string blockedUserId, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(string userId, string blockedUserId, CancellationToken cancellationToken = default);
    Task AddAsync(BlockedUser blockedUser, CancellationToken cancellationToken = default);
    Task UpdateAsync(BlockedUser blockedUser, CancellationToken cancellationToken = default);
    Task RemoveAsync(BlockedUser blockedUser, CancellationToken cancellationToken = default);
}