using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Profile.Commands;
using GigaChat.Backend.Application.Repositories.Identity;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Profile.Handlers;

public class ChangePasswordCommandHandler(IUserRepository userRepository) : IRequestHandler<ChangePasswordCommand, Result>
{
    public async Task<Result> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByIdAsync(request.RequesterId);
        if (user is null)
            return Result.Failure(UserErrors.UserNotFound);

        if (!await userRepository.CheckPasswordAsync(user, request.CurrentPassword))
            return Result.Failure(UserErrors.InvalidCredentials);

        var success = await userRepository.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
        if (!success)
            return Result.Failure(UserErrors.PasswordChangeFailed);

        return Result.Success();
    }
}