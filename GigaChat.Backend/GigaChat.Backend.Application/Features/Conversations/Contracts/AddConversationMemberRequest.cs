namespace GigaChat.Backend.Application.Features.Conversations.Contracts;

public record AddConversationMemberRequest(IReadOnlyList<string> MembersList);