using GigaChat.Backend.Api.Hubs;
using GigaChat.Backend.Application.Services.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace GigaChat.Backend.Api.Services.Hubs;

public class MessageBroadcaster(IHubContext<MessageHub> hubContext) : IMessageBroadcaster
{
    public Task BroadcastNewMessageAsync(Guid conversationId, object messageDto)
    {
        return hubContext.Clients
            .Group($"conversation-{conversationId}")
            .SendAsync("ReceiveMessage", messageDto);
    }

    public Task BroadcastMessageDeletedAsync(Guid conversationId, Guid messageId)
    {
        return hubContext.Clients
            .Group($"conversation-{conversationId}")
            .SendAsync("MessageDeleted", messageId);
    }

    public Task BroadcastMessageEditedAsync(Guid conversationId, object editedMessageDto)
    {
        return hubContext.Clients
            .Group($"conversation-{conversationId}")
            .SendAsync("MessageEdited", editedMessageDto);
    }

    public Task BroadcastReactionAddedAsync(Guid conversationId, object reactionDto)
    {
        return hubContext.Clients
            .Group($"conversation-{conversationId}")
            .SendAsync("ReactionAdded", reactionDto);
    }
    
    public Task BroadcastReactionRemovedAsync(Guid conversationId, object reactionDto)
    {
        return hubContext.Clients
            .Group($"conversation-{conversationId}")
            .SendAsync("ReactionRemoved", reactionDto);
    }
}