using GigaChat.Backend.Domain.Enums.Identity;
using GigaChat.Backend.Domain.Interfaces.Identity;

namespace GigaChat.Backend.Application.Auth;

public interface IJwtProvider
{
    (string token, int expiresIn) GenerateToken(IApplicationUser user);
    string? ValidateToken(string token);
    string GenerateEmailConfirmationJwtToken(string userId, string emailConfirmationCode, string email);
    (string?, string?, string?) ValidateEmailConfirmationJwtToken(string token);
    public string GenerateOtpJwtToken(string userId, string email, OtpPurpose purpose);
    public (string? userId, string? email, OtpPurpose? purpose) ValidateOtpJwtToken(string token);
    string GeneratePasswordResetJwt(string userId, string email, string identityToken);
    (string? userId, string? email, string? identityToken) ValidatePasswordResetJwt(string token);
}