using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Hubs.Commands;

public record SubscribeToConversationUpdatesCommand(string ConnectionId, string UserId) : IRequest<Result>;