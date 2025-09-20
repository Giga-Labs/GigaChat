using GigaChat.Backend.Domain.Entities.Core;

namespace GigaChat.Backend.Application.Repositories.Core;

public interface IUserSpamScoreRepository
{
    Task<UserSpamScore?> FindByUserIdAsync(string userId, CancellationToken cancellationToken = default);
    Task AddAsync(UserSpamScore score, CancellationToken cancellationToken = default);
    Task UpdateAsync(UserSpamScore score, CancellationToken cancellationToken = default);
    Task<bool> IsUserSpammerAsync(string userId, CancellationToken cancellationToken = default);
}