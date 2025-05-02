using GigaChat.Backend.Application.Auth;
using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Auth.Commands;
using GigaChat.Backend.Application.Repositories.Identity;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Auth.Handlers;

public class ResetPasswordCommandHandler(IOtpProvider otpProvider,IUserRepository userRepository) : IRequestHandler<ResetPasswordCommand, Result>
{
    public async Task<Result> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var isValid = await otpProvider.VerifyOtpAsync(request.Email, request.Otp, cancellationToken);
        if (!isValid)
            return Result.Failure(OtpErrors.InvalidOtp);

        await otpProvider.EndVarification(request.Email, request.Otp, cancellationToken); 
        var user = await userRepository.FindByEmailAsync(request.Email);
        if (user is null)
            return Result.Failure(UserErrors.UserNotFound);

        var token = await userRepository.GeneratePasswordResetTokenAsync(user);
        var succeeded = await userRepository.ResetPasswordAsync(user!, token, request.NewPassword);

        return succeeded ? Result.Success() : Result.Failure(UserErrors.ResetPasswordFailed);
    }

}