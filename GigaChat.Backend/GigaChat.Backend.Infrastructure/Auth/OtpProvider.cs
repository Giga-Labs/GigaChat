using GigaChat.Backend.Application.Auth;
using GigaChat.Backend.Application.Repositories.Identity;
using GigaChat.Backend.Application.Services.Otp;
using GigaChat.Backend.Domain.Entities.Identity;
using GigaChat.Backend.Domain.Enums.Identity;
using GigaChat.Backend.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace GigaChat.Backend.Infrastructure.Auth;

public class OtpProvider(IOtpVerificationRepository otpVerificationRepository, IOtpHashingService otpHashingService, IOtpGenerator otpGenerator, IOptions<OtpSettings> otpOptions) : IOtpProvider
{
    
    public async Task<string> GenerateAsync(string userId, OtpPurpose purpose, CancellationToken cancellationToken = default)
    {
        var config = purpose switch
        {
            OtpPurpose.PasswordReset => otpOptions.Value.PasswordReset,
            OtpPurpose.EmailVerification => otpOptions.Value.Verification,
            OtpPurpose.TwoFactorAuth => otpOptions.Value.TwoFactor,
            _ => throw new InvalidOperationException("Unhandled OTP purpose")
        };

        // check rate limit
        var recentCount = await otpVerificationRepository.CountRecentOtpsAsync(userId, purpose, TimeSpan.FromMinutes(config.WindowMinutes), cancellationToken);

        if (recentCount >= config.MaxAttempts)
            throw new InvalidOperationException("Too many OTP requests. Please wait before trying again.");

        // revoke old OTPs
        await RevokeAllButLatestAsync(userId, purpose, cancellationToken);

        var rawOtp = otpGenerator.GenerateCode();
        var hashedOtp = otpHashingService.Hash(rawOtp);

        var otp = new OtpVerification
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Purpose = purpose,
            HashedOtpCode = hashedOtp,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddMinutes(config.ExpiryMinutes)
        };

        await otpVerificationRepository.AddAsync(otp, cancellationToken);

        return rawOtp;
    }

    public async Task RevokeAllButLatestAsync(string userId, OtpPurpose purpose, CancellationToken cancellationToken = default)
    {
        var activeOtps = await otpVerificationRepository
            .GetAllActiveOtpsForUserAsync(userId, purpose, cancellationToken);

        if (!activeOtps.Any())
            return;

        var latest = activeOtps
            .OrderByDescending(x => x.CreatedAt)
            .First();

        var toRevoke = activeOtps
            .Where(x => x.Id != latest.Id)
            .ToList();

        if (!toRevoke.Any())
            return;

        foreach (var otp in toRevoke)
        {
            otp.RevokedAt = DateTime.UtcNow;
            otp.UpdatedAt = DateTime.UtcNow;
        }

        await otpVerificationRepository.UpdateRangeAsync(toRevoke, cancellationToken);
    }
    
    public async Task<bool> IsValidOtpAsync(string userId, string otpCode, OtpPurpose purpose, CancellationToken cancellationToken = default)
    {
        var otp = await otpVerificationRepository
            .GetLatestActiveOtpAsync(userId, purpose, cancellationToken);

        if (otp is null)
            return false;

        if (otp.ExpiresAt < DateTime.UtcNow)
            return false;

        if (otp.IsUsed || otp.RevokedAt != null)
            return false;

        return otpHashingService.Verify(otpCode, otp.HashedOtpCode);;
    }

    public async Task<bool> VerifyAndConsumeAsync(string userId, string otpCode, OtpPurpose purpose, CancellationToken cancellationToken = default)
    {
        var otp = await otpVerificationRepository
            .GetLatestActiveOtpAsync(userId, purpose, cancellationToken);

        if (otp is null)
            return false;

        if (otp.ExpiresAt < DateTime.UtcNow)
            return false;

        if (otp.IsUsed || otp.RevokedAt != null)
            return false;
        
        var result = otpHashingService.Verify(otpCode, otp.HashedOtpCode);
        if (!result)
            return false;

        await MarkAsUsedAsync(otp.Id, cancellationToken);

        return true;
    }

    public async Task MarkAsUsedAsync(Guid otpId, CancellationToken cancellationToken = default)
    {
        var otp = await otpVerificationRepository.GetByIdAsync(otpId, cancellationToken);
    
        if (otp is null || otp.IsUsed || otp.DeletedAt != null)
            return;

        otp.IsUsed = true;
        otp.UpdatedAt = DateTime.UtcNow;

        await otpVerificationRepository.UpdateAsync(otp, cancellationToken);
    }
}