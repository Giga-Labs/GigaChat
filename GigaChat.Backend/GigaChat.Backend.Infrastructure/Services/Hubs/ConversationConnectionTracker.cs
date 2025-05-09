using System.Collections.Concurrent;
using GigaChat.Backend.Application.Services.Hubs;

namespace GigaChat.Backend.Infrastructure.Services.Hubs;

public class ConversationConnectionTracker : IConversationConnectionTracker
{
    private readonly ConcurrentDictionary<string, HashSet<Guid>> _connections = new();

    public void Add(string connectionId, Guid conversationId)
    {
        _connections.AddOrUpdate(
            connectionId,
            _ => new HashSet<Guid> { conversationId },
            (_, existing) => { existing.Add(conversationId); return existing; });
    }

    public List<Guid> GetConversations(string connectionId)
        => _connections.TryGetValue(connectionId, out var convos) ? convos.ToList() : [];

    public void Remove(string connectionId, Guid conversationId)
    {
        if (_connections.TryGetValue(connectionId, out var convos))
        {
            convos.Remove(conversationId);
            if (convos.Count == 0) _connections.TryRemove(connectionId, out _);
        }
    }

    public void Clear(string connectionId)
    {
        _connections.TryRemove(connectionId, out _);
    }
}