using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Auth.Commands;

public record ConfirmEmailCommand
(
    string Token
) : IRequest<Result>;