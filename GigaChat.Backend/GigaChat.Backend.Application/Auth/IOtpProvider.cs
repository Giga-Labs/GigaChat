namespace GigaChat.Backend.Application.Auth;

public interface IOtpProvider
{
    string GenerateOtp();
    Task StoreOtpAsync(string email, string otp, CancellationToken cancellationToken);
    Task<bool> VerifyOtpAsync(string email, string otp, CancellationToken cancellationToken);
    Task<bool> ExceededLimit(string email, CancellationToken cancellationToken);
    Task EndVarification(string email, string otp, CancellationToken cancellationToken);
}