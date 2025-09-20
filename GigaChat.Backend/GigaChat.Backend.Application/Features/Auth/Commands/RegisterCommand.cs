using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Auth.Commands;

public record RegisterCommand
(
    string UserName,
    string Email,
    string Password,
    string FirstName,
    string LastName
) : IRequest<Result>;