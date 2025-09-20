using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Auth.Commands;

public record ResetPasswordCommand
(
    string Token,
    string NewPassword
) : IRequest<Result>;