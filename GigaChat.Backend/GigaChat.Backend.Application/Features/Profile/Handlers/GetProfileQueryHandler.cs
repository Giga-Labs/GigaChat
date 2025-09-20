using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Profile.Commands;
using GigaChat.Backend.Application.Features.Profile.Contracts;
using GigaChat.Backend.Application.Repositories.Identity;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Profile.Handlers;

public class GetProfileQueryHandler(IUserRepository userRepository) : IRequestHandler<GetProfileQuery, Result<UserProfileResponse>>
{
    public async Task<Result<UserProfileResponse>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(request.RequesterId);
        if (user is null)
            return Result.Failure<UserProfileResponse>(UserErrors.UserNotFound);

        var profile = new UserProfileResponse(
            user.Id,
            user.UserName,
            user.Email,
            user.FirstName,
            user.LastName,
            user.ProfilePictureUrl,
            user.AllowGroupInvites,
            user.TwoFactorEnabled
        );

        return Result.Success(profile);
    }
}