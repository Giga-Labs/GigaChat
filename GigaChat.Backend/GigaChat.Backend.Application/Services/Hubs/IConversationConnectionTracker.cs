namespace GigaChat.Backend.Application.Services.Hubs;

public interface IConversationConnectionTracker
{
    void Add(string connectionId, Guid conversationId);
    List<Guid> GetConversations(string connectionId);
    void Remove(string connectionId, Guid conversationId);
    void Clear(string connectionId);
}