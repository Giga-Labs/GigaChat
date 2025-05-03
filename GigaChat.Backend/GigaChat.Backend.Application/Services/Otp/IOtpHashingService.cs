namespace GigaChat.Backend.Application.Services.Otp;

public interface IOtpHashingService
{
    string Hash(string otp);
    bool Verify(string rawOtp, string hashedOtp);
}