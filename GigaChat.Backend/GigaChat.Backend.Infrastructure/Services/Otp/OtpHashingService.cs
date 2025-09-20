using System.Security.Cryptography;
using System.Text;
using GigaChat.Backend.Application.Services.Otp;
using GigaChat.Backend.Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace GigaChat.Backend.Infrastructure.Services.Otp;

public class OtpHashingService(IOptions<OtpSettings> otpOptions) : IOtpHashingService
{
    public string Hash(string otp)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(otpOptions.Value.SecretKey));
        var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(otp));
        return Convert.ToBase64String(hashBytes);
    }

    public bool Verify(string rawOtp, string hashedOtp)
    {
        var computedHash = Hash(rawOtp);

        return CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(computedHash),
            Encoding.UTF8.GetBytes(hashedOtp));
    }
}