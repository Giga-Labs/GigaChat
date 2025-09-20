using Microsoft.AspNetCore.Http;

namespace GigaChat.Backend.Application.Features.Profile.Contracts;

public record UpdateProfileRequest
(
    string FirstName,
    string LastName,
    string Email,
    string Username,
    bool AllowGroupInvites,
    bool TwoFactorEnabled,
    IFormFile? ProfilePicture,
    string? ProfilePictureUrl
);