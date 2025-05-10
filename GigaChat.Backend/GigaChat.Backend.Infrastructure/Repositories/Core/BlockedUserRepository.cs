using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Domain.Entities.Core;
using GigaChat.Backend.Infrastructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace GigaChat.Backend.Infrastructure.Repositories.Core;

public class BlockedUserRepository(CoreDbContext coreDbContext) : IBlockedUserRepository
{
    public async Task<BlockedUser?> GetAsync(string userId, string blockedUserId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.BlockedUsers
            .FirstOrDefaultAsync(b => b.UserId == userId && b.BlockedUserId == blockedUserId, cancellationToken);
    }

    public async Task<bool> ExistsAsync(string userId, string blockedUserId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.BlockedUsers
            .AnyAsync(b => b.UserId == userId && b.BlockedUserId == blockedUserId, cancellationToken);
    }
    
    public async Task AddAsync(BlockedUser blockedUser, CancellationToken cancellationToken = default)
    {
        await coreDbContext.BlockedUsers.AddAsync(blockedUser, cancellationToken);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task UpdateAsync(BlockedUser blockedUser, CancellationToken cancellationToken = default)
    {
        coreDbContext.BlockedUsers.Update(blockedUser);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(BlockedUser blockedUser, CancellationToken cancellationToken = default)
    {
        coreDbContext.BlockedUsers.Remove(blockedUser);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<BlockedUser>> GetBlockedUsersAsync(string userId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.BlockedUsers
            .Where(b => b.UserId == userId)
            .OrderByDescending(b => b.BlockedAt)
            .ToListAsync(cancellationToken);
    }
}