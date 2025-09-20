namespace GigaChat.Backend.Application.Features.Profile.Contracts;

public record UserProfileResponse
(
    string Id,
    string UserName,
    string Email,
    string FirstName,
    string LastName,
    string? ProfilePictureUrl,
    bool AllowGroupInvites,
    bool TwoFactorEnabled
);
