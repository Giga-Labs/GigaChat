using GigaChat.Backend.Application.Features.Profile.Contracts;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace GigaChat.Backend.Application.Features.Profile.Commands;

public record UpdateProfileCommand
(
    string RequesterId,
    string FirstName,
    string LastName,
    string Email,
    string Username,
    bool AllowGroupInvites,
    bool TwoFactorEnabled,
    IFormFile? ProfilePicture,
    string? ProfilePictureUrl
) : IRequest<Result<UserProfileResponse>>;