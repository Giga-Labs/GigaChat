using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Auth.Commands;

public record VerifyPasswordResetOtpCommand
(
    string Email, 
    string OtpCode
) : IRequest<Result<string>>;