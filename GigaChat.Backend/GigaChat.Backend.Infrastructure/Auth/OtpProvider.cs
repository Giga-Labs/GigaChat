using GigaChat.Backend.Application.Auth;
using GigaChat.Backend.Application.Repositories.Identity;
using GigaChat.Backend.Application.Settings;
using GigaChat.Backend.Domain.Entities.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GigaChat.Backend.Infrastructure.Auth;

public class OtpProvider(IOtpVerificationRepository otpRepository, IOptions<OtpRateSettings> otpRateSettings , ILogger<OtpProvider> logger) : IOtpProvider
{
    public string GenerateOtp()
    {
        // Generate a 6-digit OTP
        var random = new Random();
        return random.Next(100000, 999999).ToString();
    }

    public async Task StoreOtpAsync(string email, string otp, CancellationToken cancellationToken)
    {
        var otpVerification = new OtpVerification
        {
            Id = Guid.NewGuid(),
            Email = email,
            OtpCode = otp,
            CreatedOn = DateTime.UtcNow,
            ExpiresOn = DateTime.UtcNow.AddMinutes(10), // OTP valid for 10 minutes
            IsUsed = false
        };

        await otpRepository.AddAsync(otpVerification, cancellationToken);
    }

    public async Task<bool> VerifyOtpAsync(string email, string otp, CancellationToken cancellationToken)
    {
        var otpVerification = await otpRepository.FindByEmailAndOtpAsync(email, otp, cancellationToken);
        if (otpVerification == null || otpVerification.IsUsed || otpVerification.ExpiresOn < DateTime.UtcNow)
            return false;

        
        return true;
    }

    public async Task<bool> ExceededLimit(string email, CancellationToken cancellationToken)
    {
        var RateLimitWindow = TimeSpan.FromHours(otpRateSettings.Value.RateLimitWindowByHours);
        var recentOtps = await otpRepository.GetRecentOtpsAsync(email, RateLimitWindow,cancellationToken);

        var MaxAttempts = otpRateSettings.Value.MaxAttempts;


        if (recentOtps.Count() >= MaxAttempts)
        {
            logger.LogWarning("Rate limit exceeded for email: {Email}. Max attempts: {MaxAttempts} within {Window}",
                email, MaxAttempts, RateLimitWindow);
            return true;
        }

        return false;
    }
    public async Task EndVarification(string email, string otp, CancellationToken cancellationToken)
    {
        var otpVerification = await otpRepository.FindByEmailAndOtpAsync(email, otp, cancellationToken);
       
        otpVerification!.IsUsed = true;
        await otpRepository.UpdateAsync(otpVerification, cancellationToken);
        
    }
}