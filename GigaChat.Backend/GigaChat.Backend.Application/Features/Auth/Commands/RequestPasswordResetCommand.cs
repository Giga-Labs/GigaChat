using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Auth.Commands;

public record RequestPasswordResetCommand(string Email) : IRequest<Result>;