using GigaChat.Backend.Application.Auth;
using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Auth.Commands;
using GigaChat.Backend.Application.Models;
using GigaChat.Backend.Application.Repositories.Identity;
using GigaChat.Backend.Application.Services.Email;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace GigaChat.Backend.Application.Features.Auth.Handlers;

public class RegisterCommandHandler(IUserRepository userRepository, ILogger<RegisterCommandHandler> logger, IEmailService emailService, IJwtProvider jwtProvider) : IRequestHandler<RegisterCommand, Result>
{
    public async Task<Result> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await userRepository.FindByEmailAsync(request.Email) is not null)
            return Result.Failure(UserErrors.DuplicateEmail);
        
        if (await userRepository.FindByUserNameAsync(request.UserName) is not null)
            return Result.Failure(UserErrors.DuplicateUserName);

        var user = new ApplicationUserModel
        {
            Email = request.Email,
            UserName = request.UserName,
            FirstName = request.FirstName,
            LastName = request.LastName,
        };

        var success = await userRepository.CreateUserAsync(user, request.Password);

        if (!success)
            return Result.Failure(UserErrors.RegistrationFailed);

        var applicationUser = await userRepository.FindByEmailAsync(user.Email);
        if (applicationUser is null)
            return Result.Failure(UserErrors.UserNotFound);

        var emailConfirmationToken = await userRepository.GenerateEmailConfirmationTokenAsync(applicationUser);

        var token = jwtProvider.GenerateEmailConfirmationJwtToken(applicationUser.Id, emailConfirmationToken, applicationUser.Email);
        
        await emailService.SendConfirmationEmail(applicationUser.Email, token);
        
        logger.LogInformation("Confirmation Token: {token}", token);
        
        return Result.Success();
    }
}