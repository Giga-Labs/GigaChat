using GigaChat.Backend.Domain.Interfaces.Identity;

namespace GigaChat.Backend.Application.Auth;

public interface IJwtProvider
{
    (string token, int expiresIn) GenerateToken(IApplicationUser user);
    string? ValidateToken(string token);
    string GenerateEmailConfirmationJwtToken(string userId, string emailConfirmationCode, string email);
    (string?, string?, string?) ValidateEmailConfirmationJwtToken(string token);
}