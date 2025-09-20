using GigaChat.Backend.Api.Hubs;
using GigaChat.Backend.Application.Services.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace GigaChat.Backend.Api.Services.Hubs;

public class MessageHubNotifier(IHubContext<MessageHub> hubContext) : IMessageHubNotifier
{
    public async Task AddConnectionAsync(string connectionId, Guid conversationId)
    {
        await hubContext.Groups.AddToGroupAsync(connectionId, $"conversation-{conversationId}");
    }

    public async Task RemoveConnectionAsync(string connectionId, Guid conversationId)
    {
        await hubContext.Groups.RemoveFromGroupAsync(connectionId, $"conversation-{conversationId}");
    }
}