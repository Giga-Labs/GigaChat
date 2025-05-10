using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Profile.Commands;
using GigaChat.Backend.Application.Features.Profile.Contracts;
using GigaChat.Backend.Application.Repositories.Identity;
using GigaChat.Backend.Application.Services.Storage;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Profile.Handlers;

public class UpdateProfileCommandHandler(IUserRepository userRepository, IFileStorageService fileStorageService) : IRequestHandler<UpdateProfileCommand, Result<UserProfileResponse>>
{
    public async Task<Result<UserProfileResponse>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(request.RequesterId);
        if (user is null)
            return Result.Failure<UserProfileResponse>(UserErrors.UserNotFound);

        string profilePictureUrl;

        if (request.ProfilePicture is not null)
        {
            profilePictureUrl = await fileStorageService.UploadProfilePictureAsync(request.RequesterId, request.ProfilePicture);
        }
        else
        {
            profilePictureUrl = $"https://api.dicebear.com/6.x/bottts/svg?seed={Uri.EscapeDataString(request.Username)}";
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;
        user.UserName = request.Username;
        user.AllowGroupInvites = request.AllowGroupInvites;
        user.TwoFactorEnabled = request.TwoFactorEnabled;
        user.ProfilePictureUrl = profilePictureUrl;

        await userRepository.UpdateAsync(user);

        var response = new UserProfileResponse(
            user.Id,
            user.UserName,
            user.Email,
            user.FirstName,
            user.LastName,
            user.ProfilePictureUrl,
            user.AllowGroupInvites,
            user.TwoFactorEnabled
        );

        return Result.Success(response);
    }
}