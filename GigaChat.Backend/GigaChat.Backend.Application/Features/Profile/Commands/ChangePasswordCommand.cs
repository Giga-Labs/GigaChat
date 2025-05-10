using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Profile.Commands;

public record ChangePasswordCommand(
    string RequesterId,
    string CurrentPassword,
    string NewPassword
) : IRequest<Result>;