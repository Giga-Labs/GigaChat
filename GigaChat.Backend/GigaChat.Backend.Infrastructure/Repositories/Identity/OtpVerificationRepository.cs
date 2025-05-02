using GigaChat.Backend.Application.Repositories.Identity;
using GigaChat.Backend.Domain.Entities.Identity;
using GigaChat.Backend.Infrastructure.Persistence.Identity;
using Microsoft.EntityFrameworkCore;

namespace GigaChat.Backend.Infrastructure.Repositories.Identity;

public class OtpVerificationRepository(ApplicationUserDbContext applicationUserDbContext): IOtpVerificationRepository
{
    public async Task AddAsync(OtpVerification otp, CancellationToken cancellationToken)
    {
        await applicationUserDbContext.OtpVerifications.AddAsync(otp, cancellationToken);
        await applicationUserDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<OtpVerification?> FindByEmailAndOtpAsync(string email, string otp, CancellationToken cancellationToken)
    {
        return await applicationUserDbContext.OtpVerifications
            .FirstOrDefaultAsync(o => o.Email == email && o.OtpCode == otp, cancellationToken);
    }
    public async Task<IQueryable<OtpVerification>> GetRecentOtpsAsync(string email, TimeSpan timeWindow, CancellationToken cancellationToken)
    {
        var cutoffTime = DateTime.UtcNow - timeWindow;
        return await Task.FromResult(applicationUserDbContext.OtpVerifications
            .Where(o => o.Email == email && o.CreatedOn >= cutoffTime)
            .AsQueryable());
    }

    public async Task UpdateAsync(OtpVerification otp, CancellationToken cancellationToken)
    {
        applicationUserDbContext.OtpVerifications.Update(otp);
        await applicationUserDbContext.SaveChangesAsync(cancellationToken);
    }
}