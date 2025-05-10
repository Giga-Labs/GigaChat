namespace GigaChat.Backend.Application.Features.Conversations.Contracts;

public record ToggleAdminStatusResponse(string UserId, bool IsAdmin);