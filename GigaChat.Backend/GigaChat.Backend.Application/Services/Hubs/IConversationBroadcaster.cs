using GigaChat.Backend.Application.Features.Conversations.Contracts;

namespace GigaChat.Backend.Application.Services.Hubs;

public interface IConversationBroadcaster
{
    Task BroadcastNewConversationAsync(string userId, ConversationResponse response);
    Task BroadcastConversationDeletedAsync(string userId, Guid conversationId);
    Task BroadcastConversationUpdatedAsync(string userId, ConversationResponse response);
}