using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Conversations.Commands;

public record AcceptConversationCommand
(
    string RequesterId,
    Guid ConversationId,
    bool Accept,
    string ConnectionId
) : IRequest<Result>;