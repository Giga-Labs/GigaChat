using GigaChat.Backend.Application.Features.Messages.Contracts;

namespace GigaChat.Backend.Application.Services.Hubs;

public interface IMessageBroadcaster
{
    Task BroadcastNewMessageAsync(Guid conversationId, MessageResponse response);
    Task BroadcastMessageDeletedAsync(Guid conversationId, Guid messageId);
    Task BroadcastMessageEditedAsync(Guid conversationId, MessageResponse response);
    Task BroadcastReactionAddedAsync(Guid conversationId, MessageResponse response);
    Task BroadcastReactionRemovedAsync(Guid conversationId, MessageResponse response);
}