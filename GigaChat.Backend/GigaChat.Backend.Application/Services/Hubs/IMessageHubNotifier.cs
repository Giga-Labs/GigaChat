namespace GigaChat.Backend.Application.Services.Hubs;

public interface IMessageHubNotifier
{
    Task AddConnectionAsync(string connectionId, Guid conversationId);
    Task RemoveConnectionAsync(string connectionId, Guid conversationId);
}