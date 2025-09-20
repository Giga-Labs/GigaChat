using GigaChat.Backend.Api.Hubs;
using GigaChat.Backend.Application.Features.Messages.Contracts;
using GigaChat.Backend.Application.Services.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace GigaChat.Backend.Api.Services.Hubs;

public class MessageBroadcaster(IHubContext<MessageHub> hubContext) : IMessageBroadcaster
{
    public Task BroadcastNewMessageAsync(Guid conversationId, MessageResponse response)
    {
        return hubContext.Clients
            .Group($"conversation-{conversationId}")
            .SendAsync("ReceiveMessage", response);
    }

    public Task BroadcastMessageDeletedAsync(Guid conversationId, Guid messageId)
    {
        return hubContext.Clients
            .Group($"conversation-{conversationId}")
            .SendAsync("MessageDeleted", messageId);
    }

    public Task BroadcastMessageEditedAsync(Guid conversationId, MessageResponse response)
    {
        return hubContext.Clients
            .Group($"conversation-{conversationId}")
            .SendAsync("MessageEdited", response);
    }

    public Task BroadcastReactionAddedAsync(Guid conversationId, MessageResponse response)
    {
        return hubContext.Clients
            .Group($"conversation-{conversationId}")
            .SendAsync("ReactionAdded", response);
    }
    
    public Task BroadcastReactionRemovedAsync(Guid conversationId, MessageResponse response)
    {
        return hubContext.Clients
            .Group($"conversation-{conversationId}")
            .SendAsync("ReactionRemoved", response);
    }
}