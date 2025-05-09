namespace GigaChat.Backend.Application.Services.Hubs;

public interface IConversationHubNotifier
{
    Task AddConnectionAsync(string connectionId, Guid conversationId);
    Task RemoveConnectionAsync(string connectionId, Guid conversationId);
}