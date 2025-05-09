using GigaChat.Backend.Application.Models;

namespace GigaChat.Backend.Application.Features.Conversations.Contracts;

public record ConversationResponse
(
    Guid Id,
    string Name,
    bool IsGroup,
    string? AdminId,
    IReadOnlyList<ReceiverModel> MembersList
);