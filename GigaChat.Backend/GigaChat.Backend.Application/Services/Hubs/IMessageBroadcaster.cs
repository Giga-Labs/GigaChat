namespace GigaChat.Backend.Application.Services.Hubs;

public interface IMessageBroadcaster
{
    Task BroadcastNewMessageAsync(Guid conversationId, object messageDto);
    Task BroadcastMessageDeletedAsync(Guid conversationId, Guid messageId);
    Task BroadcastMessageEditedAsync(Guid conversationId, object editedMessageDto);
    Task BroadcastReactionAddedAsync(Guid conversationId, object reactionDto);
    Task BroadcastReactionRemovedAsync(Guid conversationId, object reactionDto);
}