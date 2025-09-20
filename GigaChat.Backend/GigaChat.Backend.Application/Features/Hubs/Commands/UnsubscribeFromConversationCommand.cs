using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Hubs.Commands;

public record UnsubscribeFromConversationCommand(string ConnectionId, Guid ConversationId) : IRequest<Result>;