using GigaChat.Backend.Application.Auth;
using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Auth.Commands;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Auth.Handlers;

public class VerifyOtpCommandHandler(IOtpProvider otpProvider) : IRequestHandler<VerifyOtpCommand, Result>
{
    public async Task<Result> Handle(VerifyOtpCommand request, CancellationToken cancellationToken)
    {
        // Verify the OTP using the email and OTP provided
        var isValid = await otpProvider.VerifyOtpAsync(request.Email, request.Otp, cancellationToken);
        if (!isValid)
            return Result.Failure(OtpErrors.InvalidOtp);
     

        return Result.Success();
    }

}