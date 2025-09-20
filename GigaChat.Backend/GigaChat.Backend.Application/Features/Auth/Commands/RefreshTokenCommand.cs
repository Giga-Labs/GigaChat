using GigaChat.Backend.Application.Features.Auth.Contracts;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Auth.Commands;

public record RefreshTokenCommand
(
    string Token,
    string RefreshToken
) : IRequest<Result<AuthResponse>>;