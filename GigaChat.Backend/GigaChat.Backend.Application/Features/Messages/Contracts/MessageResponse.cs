namespace GigaChat.Backend.Application.Features.Messages.Contracts;

public record MessageResponse(
    Guid Id,
    Guid ConversationId,
    string SenderId,
    string Content,
    DateTime SentAt
);