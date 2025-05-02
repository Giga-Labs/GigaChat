using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Auth.Commands;

public record VerifyOtpCommand
(
    string Otp,
    string Email
):IRequest<Result>;