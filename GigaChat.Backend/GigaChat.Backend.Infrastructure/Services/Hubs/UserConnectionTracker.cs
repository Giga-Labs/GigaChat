using System.Collections.Concurrent;
using GigaChat.Backend.Application.Services.Hubs;

namespace GigaChat.Backend.Infrastructure.Services.Hubs;

public class UserConnectionTracker : IUserConnectionTracker
{
    private readonly ConcurrentDictionary<string, string> _connectionToUser = new();
    private readonly ConcurrentDictionary<string, HashSet<string>> _userToConnections = new();

    public void Add(string connectionId, string userId)
    {
        _connectionToUser[connectionId] = userId;
        _userToConnections.AddOrUpdate(
            userId,
            _ => new HashSet<string> { connectionId },
            (_, set) => { set.Add(connectionId); return set; });
    }

    public void Remove(string connectionId)
    {
        if (_connectionToUser.TryRemove(connectionId, out var userId))
        {
            if (_userToConnections.TryGetValue(userId, out var set))
            {
                set.Remove(connectionId);
                if (set.Count == 0)
                    _userToConnections.TryRemove(userId, out _);
            }
        }
    }

    public List<string> GetConnections(string userId) =>
        _userToConnections.TryGetValue(userId, out var set) ? set.ToList() : new();
}