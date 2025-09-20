using GigaChat.Backend.Application.Features.Auth.Contracts;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Auth.Queries;

public record AuthQuery
(
    string Email,
    string Password
) : IRequest<Result<AuthResponse>>;