using GigaChat.Backend.Application.Auth;
using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Auth.Commands;
using GigaChat.Backend.Application.Repositories.Identity;
using GigaChat.Backend.Domain.Abstractions;
using GigaChat.Backend.Domain.Enums.Identity;
using MediatR;

namespace GigaChat.Backend.Application.Features.Auth.Handlers;

public class VerifyPasswordResetOtpCommandHandler(IUserRepository userRepository, IOtpProvider otpProvider, IJwtProvider jwtProvider) : IRequestHandler<VerifyPasswordResetOtpCommand, Result<string>>
{
    public async Task<Result<string>> Handle(VerifyPasswordResetOtpCommand request, CancellationToken cancellationToken)
    {
        // find user by email
        var user = await userRepository.FindByEmailAsync(request.Email);
        if (user is null)
            return Result.Failure<string>(UserErrors.InvalidPasswordResetOtp);

        // check if the otp is valid and mark it as used 
        var isValid =
            await otpProvider.VerifyAndConsumeAsync(user.Id, request.OtpCode, OtpPurpose.PasswordReset, cancellationToken);
        if (!isValid)
            return Result.Failure<string>(UserErrors.InvalidPasswordResetOtp);
        
        // generate identity token
        var identityToken = await userRepository.GeneratePasswordResetTokenAsync(user);

        // wrap it in your beautiful, handcrafted artisan JWT
        var jwt = jwtProvider.GeneratePasswordResetJwt(user.Id, user.Email, identityToken);
        
        return Result.Success(jwt);
    }
}