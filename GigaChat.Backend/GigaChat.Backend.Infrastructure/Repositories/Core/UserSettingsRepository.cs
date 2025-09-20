using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Domain.Entities.Core;
using GigaChat.Backend.Infrastructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace GigaChat.Backend.Infrastructure.Repositories.Core;

public class UserSettingsRepository(CoreDbContext coreDbContext) : IUserSettingsRepository
{
    public async Task<UserSettings?> FindByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.UserSettings
            .FirstOrDefaultAsync(s => s.UserId == userId, cancellationToken);
    }

    public async Task AddAsync(UserSettings settings, CancellationToken cancellationToken = default)
    {
        await coreDbContext.UserSettings.AddAsync(settings, cancellationToken);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(UserSettings settings, CancellationToken cancellationToken = default)
    {
        coreDbContext.UserSettings.Update(settings);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }
}