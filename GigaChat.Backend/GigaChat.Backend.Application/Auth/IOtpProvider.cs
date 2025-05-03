using GigaChat.Backend.Domain.Entities.Identity;
using GigaChat.Backend.Domain.Enums.Identity;

namespace GigaChat.Backend.Application.Auth;

public interface IOtpProvider
{
    Task<string> GenerateAsync(string userId, OtpPurpose purpose, CancellationToken cancellationToken = default);

    Task RevokeAllButLatestAsync(string userId, OtpPurpose purpose, CancellationToken cancellationToken = default);

    Task<bool> VerifyAsync(string userId, string otpCode, OtpPurpose purpose, CancellationToken cancellationToken = default);

    Task MarkAsUsedAsync(Guid otpId, CancellationToken cancellationToken = default);
}