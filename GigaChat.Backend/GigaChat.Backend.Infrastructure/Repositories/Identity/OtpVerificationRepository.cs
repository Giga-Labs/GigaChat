using GigaChat.Backend.Application.Repositories.Identity;
using GigaChat.Backend.Domain.Entities.Identity;
using GigaChat.Backend.Domain.Enums.Identity;
using GigaChat.Backend.Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace GigaChat.Backend.Infrastructure.Repositories.Identity;

public class OtpVerificationRepository(ApplicationUserDbContext applicationUserDbContext) : IOtpVerificationRepository
{
    public async Task<OtpVerification?> GetByIdAsync(Guid otpId, CancellationToken cancellationToken = default)
    {
        return await applicationUserDbContext.OtpVerifications.FirstOrDefaultAsync(o =>
            o.Id == otpId && o.DeletedAt == null, cancellationToken);
    }

    public async Task<IReadOnlyList<OtpVerification>> GetExpiredOtpsAsync(DateTime olderThanUtc, CancellationToken cancellationToken = default)
    {
        return await applicationUserDbContext.OtpVerifications
            .Where(o => o.ExpiresAt < olderThanUtc && o.DeletedAt == null)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<OtpVerification>> GetAllActiveOtpsForUserAsync(string userId, OtpPurpose purpose, CancellationToken cancellationToken = default)
    {
        return await applicationUserDbContext.OtpVerifications
            .Where(o =>
                o.UserId == userId &&
                o.Purpose == purpose &&
                o.DeletedAt == null &&
                o.IsUsed == false &&
                o.RevokedAt == null &&
                o.ExpiresAt > DateTime.UtcNow)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(OtpVerification otp, CancellationToken cancellationToken = default)
    {
        await applicationUserDbContext.OtpVerifications.AddAsync(otp, cancellationToken);
        await applicationUserDbContext.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<OtpVerification?> GetLatestActiveOtpAsync(string userId, OtpPurpose purpose, CancellationToken cancellationToken = default)
    {
        return await applicationUserDbContext.OtpVerifications
            .Where(o =>
                o.UserId == userId &&
                o.Purpose == purpose &&
                o.DeletedAt == null &&
                o.IsUsed == false &&
                o.RevokedAt == null &&
                o.ExpiresAt > DateTime.UtcNow)
            .OrderByDescending(o => o.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<int> CountRecentOtpsAsync(string userId, OtpPurpose purpose, TimeSpan window, CancellationToken cancellationToken = default)
    { 
        var cutoff = DateTime.UtcNow - window;

        return await applicationUserDbContext.OtpVerifications
            .CountAsync(o =>
                    o.UserId == userId &&
                    o.Purpose == purpose &&
                    o.CreatedAt >= cutoff &&
                    o.DeletedAt == null,
                cancellationToken);
    }

    public async Task UpdateAsync(OtpVerification otp, CancellationToken cancellationToken = default)
    {
        applicationUserDbContext.OtpVerifications.Update(otp);
        await applicationUserDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateRangeAsync(IEnumerable<OtpVerification> otps, CancellationToken cancellationToken = default)
    {
        applicationUserDbContext.OtpVerifications.UpdateRange(otps);
        await applicationUserDbContext.SaveChangesAsync(cancellationToken);
    }
}