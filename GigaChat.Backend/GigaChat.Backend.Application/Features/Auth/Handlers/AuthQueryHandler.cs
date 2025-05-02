using System.Security.Cryptography;
using GigaChat.Backend.Application.Auth;
using GigaChat.Backend.Application.Errors;
using GigaChat.Backend.Application.Features.Auth.Contracts;
using GigaChat.Backend.Application.Features.Auth.Queries;
using GigaChat.Backend.Application.Repositories.Identity;
using GigaChat.Backend.Application.Settings;
using GigaChat.Backend.Domain.Abstractions;
using GigaChat.Backend.Domain.Entities.Identity;
using MediatR;
using Microsoft.Extensions.Options;

namespace GigaChat.Backend.Application.Features.Auth.Handlers;

public class AuthQueryHandler(IUserRepository userRepository, IJwtProvider jwtProvider, IOptions<RefreshTokenSettings> refreshTokenSettings) : IRequestHandler<AuthQuery, Result<AuthResponse>>
{
    public async Task<Result<AuthResponse>> Handle(AuthQuery request, CancellationToken cancellationToken)
    {
        if (await userRepository.FindByEmailAsync(request.Email) is not { } user)
            return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

        if (!await userRepository.CheckPasswordAsync(user, request.Password))
            return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

        if (!user.EmailConfirmed)
            return Result.Failure<AuthResponse>(UserErrors.EmailNotConfirmed);
        
        
        var (token, expiresIn) = jwtProvider.GenerateToken(user);
        
        var refreshToken = GenerateRefreshToken();
        var refreshTokenExpiration = DateTime.UtcNow.AddDays(refreshTokenSettings.Value.RefreshTokenExpiryDays);
        
        user.RefreshTokens.Add(new RefreshToken
        {
            Token = refreshToken,
            ExpiresOn = refreshTokenExpiration
        });
        
        await userRepository.UpdateAsync(user);
        
        var authResponse = new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, token, expiresIn,
            refreshToken, refreshTokenExpiration);

        return Result.Success(authResponse);
    }
    
    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(128));
    }
}