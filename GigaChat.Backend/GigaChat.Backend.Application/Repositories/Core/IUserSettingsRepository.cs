using GigaChat.Backend.Domain.Entities.Core;

namespace GigaChat.Backend.Application.Repositories.Core;

public interface IUserSettingsRepository
{
    Task<UserSettings?> FindByUserIdAsync(string userId, CancellationToken cancellationToken = default);
    Task AddAsync(UserSettings settings, CancellationToken cancellationToken = default);
    Task UpdateAsync(UserSettings settings, CancellationToken cancellationToken = default);
}