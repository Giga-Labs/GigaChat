using GigaChat.Backend.Application.Auth;
using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Auth.Commands;
using GigaChat.Backend.Application.Repositories.Identity;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Auth.Handlers;

public class ResetPasswordCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider) : IRequestHandler<ResetPasswordCommand, Result>
{
    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        // decode the reset token
        var (userId, _, resetToken) = jwtProvider.ValidatePasswordResetJwt(request.Token);

        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(resetToken))
            return Result.Failure(UserErrors.InvalidPasswordResetToken);

        // get the user
        var user = await userRepository.FindByIdAsync(userId);
        if (user is null)
            return Result.Failure(UserErrors.UserNotFound);

        // reset the password
        var success = await userRepository.ResetPasswordAsync(user, resetToken, request.NewPassword);
        if (!success)
            return Result.Failure(UserErrors.ResetPasswordFailed);

        return Result.Success();
    }
}