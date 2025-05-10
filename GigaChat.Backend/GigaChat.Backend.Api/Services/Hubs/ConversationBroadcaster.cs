using GigaChat.Backend.Api.Hubs;
using GigaChat.Backend.Application.Features.Conversations.Contracts;
using GigaChat.Backend.Application.Services.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace GigaChat.Backend.Api.Services.Hubs;

public class ConversationBroadcaster(IHubContext<ConversationHub> hubContext) : IConversationBroadcaster
{
    public Task BroadcastNewConversationAsync(string userId, ConversationResponse response)
    {
        return hubContext.Clients
            .Group($"user-{userId}")
            .SendAsync("NewConversation", response);
    }

    public Task BroadcastConversationRemovedAsync(string userId, Guid conversationId)
    {
        return hubContext.Clients
            .Group($"user-{userId}")
            .SendAsync("ConversationRemoved", conversationId);
    }

    public Task BroadcastConversationUpdatedAsync(string userId, ConversationResponse response)
    {
        return hubContext.Clients
            .Group($"user-{userId}")
            .SendAsync("ConversationUpdated", response);
    }
}