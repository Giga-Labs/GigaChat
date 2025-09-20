using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Conversations.Commands;

public record RemoveUserFromConversationCommand(
    string RequesterId,
    Guid ConversationId,
    string TargetUserId
) : IRequest<Result>;