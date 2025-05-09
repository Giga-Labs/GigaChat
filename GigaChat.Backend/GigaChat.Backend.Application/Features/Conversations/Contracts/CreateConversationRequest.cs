namespace GigaChat.Backend.Application.Features.Conversations.Contracts;

public record CreateConversationRequest
(
    string? Name,
    List<string> MembersList
);