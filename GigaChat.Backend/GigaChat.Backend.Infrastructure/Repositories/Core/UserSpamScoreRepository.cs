using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Domain.Entities.Core;
using GigaChat.Backend.Infrastructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace GigaChat.Backend.Infrastructure.Repositories.Core;

public class UserSpamScoreRepository(CoreDbContext coreDbContext) : IUserSpamScoreRepository
{
    public async Task<UserSpamScore?> FindByUserIdAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.UserSpamScores.FirstOrDefaultAsync(s => s.UserId == userId, cancellationToken);
    }

    public async Task AddAsync(UserSpamScore score, CancellationToken cancellationToken = default)
    {
        await coreDbContext.UserSpamScores.AddAsync(score, cancellationToken);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(UserSpamScore score, CancellationToken cancellationToken = default)
    {
        coreDbContext.UserSpamScores.Update(score);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsUserSpammerAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.UserSpamScores
            .AnyAsync(s => s.UserId == userId && s.IsMarkedAsSpammer, cancellationToken);
    }
}