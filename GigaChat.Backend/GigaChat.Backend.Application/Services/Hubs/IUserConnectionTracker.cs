namespace GigaChat.Backend.Application.Services.Hubs;

public interface IUserConnectionTracker
{
    void Add(string connectionId, string userId);
    void Remove(string connectionId);
    List<string> GetConnections(string userId);
}