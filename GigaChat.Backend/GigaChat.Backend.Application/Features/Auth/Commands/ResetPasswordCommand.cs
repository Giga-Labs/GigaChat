using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Auth.Commands;

public record ResetPasswordCommand
(
    string Email,
    string Otp,
    string NewPassword
):IRequest<Result>;