using GigaChat.Backend.Application.Features.Conversations.Contracts;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Conversations.Commands;

public record AddConversationMembersCommand(
    string RequesterId,
    Guid ConversationId,
    IReadOnlyList<string> MembersList
) : IRequest<Result<ConversationResponse>>;