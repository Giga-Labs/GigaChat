using System.Security.Cryptography;
using GigaChat.Backend.Application.Auth;
using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Auth.Commands;
using GigaChat.Backend.Application.Features.Auth.Contracts;
using GigaChat.Backend.Application.Repositories.Identity;
using GigaChat.Backend.Application.Settings;
using GigaChat.Backend.Domain.Abstractions;
using GigaChat.Backend.Domain.Entities.Identity;
using MediatR;
using Microsoft.Extensions.Options;

namespace GigaChat.Backend.Application.Features.Auth.Handlers;

public class RefreshTokenCommandHandler(IUserRepository userRepository, IJwtProvider jwtProvider, IOptions<RefreshTokenSettings> refreshTokenSettings) : IRequestHandler<RefreshTokenCommand, Result<AuthResponse>>
{
    public async Task<Result<AuthResponse>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var userId = jwtProvider.ValidateToken(request.Token);
        if (userId is null)
            return Result.Failure<AuthResponse>(TokenErrors.InvalidToken);

        var user = await userRepository.FindByIdAsync(userId);
        if (user is null)
            return Result.Failure<AuthResponse>(UserErrors.UserNotFound);

        var userRefreshToken =
            user.RefreshTokens.SingleOrDefault(token => token.Token == request.RefreshToken && token.IsActive);
        if (userRefreshToken is null)
            return Result.Failure<AuthResponse>(TokenErrors.InvalidToken);

        // revoke now since we will refresh the both the refresh token and jwt token
        userRefreshToken.RevokedOn = DateTime.UtcNow;
        
        // generate new jwt token for the user
        var (newToken, expiresIn) = jwtProvider.GenerateToken(user);

        // generate a new refresh token using the private method here (the one with the random number generator)
        var newRefreshToken = GenerateRefreshToken(); 
        var refreshTokenExpiration = DateTime.UtcNow.AddDays(refreshTokenSettings.Value.RefreshTokenExpiryDays);

        // add the new refresh token to the refresh tokens table in the database
        user.RefreshTokens.Add(new RefreshToken
        {
            Token = newRefreshToken,
            ExpiresOn = refreshTokenExpiration
        });

        await userRepository.UpdateAsync(user);
        
        var authResponse = new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, newToken, expiresIn,
            newRefreshToken, refreshTokenExpiration);

        return Result.Success(authResponse);
    }
    
    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(128));
    }
}