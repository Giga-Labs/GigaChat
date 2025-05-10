namespace GigaChat.Backend.Application.Services.Hubs;

public interface IConversationHubNotifier
{
    Task AddConnectionAsync(string connectionId, Guid conversationId);
    Task RemoveConnectionAsync(string connectionId, Guid conversationId);
    Task AddPrivateConnectionAsync(string connectionId, string userId);
    Task RemovePrivateConnectionAsync(string connectionId, string userId);
}