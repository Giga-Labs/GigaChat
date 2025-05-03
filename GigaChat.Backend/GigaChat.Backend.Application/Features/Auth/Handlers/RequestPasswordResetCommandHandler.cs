using GigaChat.Backend.Application.Auth;
using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Auth.Commands;
using GigaChat.Backend.Application.Repositories.Identity;
using GigaChat.Backend.Application.Services.Email;
using GigaChat.Backend.Domain.Abstractions;
using GigaChat.Backend.Domain.Enums.Identity;
using MediatR;

namespace GigaChat.Backend.Application.Features.Auth.Handlers;

public class RequestPasswordResetCommandHandler(IUserRepository userRepository, IOtpProvider otpProvider, IEmailService emailService) : IRequestHandler<RequestPasswordResetCommand, Result>
{
    public async Task<Result> Handle(RequestPasswordResetCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.FindByEmailAsync(request.Email);
        if (user is null)
            return Result.Success(); // lie 
        
        if (!user.EmailConfirmed)
            return Result.Failure(UserErrors.EmailNotConfirmed);

        var rawOtp = await otpProvider.GenerateAsync(user.Id, OtpPurpose.PasswordReset, cancellationToken);

        await emailService.SendPasswordResetEmailAsync(user.Email, rawOtp);

        return Result.Success();
    }
}