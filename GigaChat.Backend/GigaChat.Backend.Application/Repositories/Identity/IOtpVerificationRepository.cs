using GigaChat.Backend.Domain.Entities.Identity;

namespace GigaChat.Backend.Application.Repositories.Identity;

public interface IOtpVerificationRepository
{
    Task AddAsync(OtpVerification otp, CancellationToken cancellationToken);
    Task<OtpVerification?> FindByEmailAndOtpAsync(string email, string otp, CancellationToken cancellationToken);
    Task UpdateAsync(OtpVerification otp, CancellationToken cancellationToken);
    Task<IQueryable<OtpVerification>> GetRecentOtpsAsync(string email, TimeSpan timeWindow, CancellationToken cancellationToken);
}