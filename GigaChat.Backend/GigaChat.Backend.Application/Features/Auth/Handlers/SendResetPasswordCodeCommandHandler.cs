using GigaChat.Backend.Application.Auth;
using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Auth.Commands;
using GigaChat.Backend.Application.Repositories.Identity;
using GigaChat.Backend.Application.Services.Email;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GigaChat.Backend.Application.Features.Auth.Handlers;

public class SendResetPasswordCodeCommandHandler(IUserRepository userRepository
    , IOtpProvider otpProvider
    , IEmailService emailService
    , ILogger<SendResetPasswordCodeCommandHandler> logger)
    : IRequestHandler<SendResetPasswordCodeCommand, Result>
{
    public async Task<Result> Handle(SendResetPasswordCodeCommand request, CancellationToken cancellationToken)
    {
        // Check if user exists
        if (await userRepository.FindByEmailAsync(request.Email) is not { } applicationUser)
        {
            logger.LogWarning("No user found with email: {Email}", request.Email);
            return Result.Success();
        }

        // Check rate limit
        var userExceededLimit = await otpProvider.ExceededLimit(request.Email, cancellationToken);
        if (userExceededLimit)
            return Result.Failure(OtpErrors.ExceedLimit);

        // Generate and store new OTP

        var otp =  otpProvider.GenerateOtp() ;
        await otpProvider.StoreOtpAsync(request.Email, otp, cancellationToken);

        await emailService.SendPasswordResetEmailAsync(applicationUser.Email, otp);

        logger.LogInformation("OTP : {otp}", otp);

        return Result.Success();
    }
}