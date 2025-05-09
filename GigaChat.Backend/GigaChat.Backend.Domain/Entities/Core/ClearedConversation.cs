namespace GigaChat.Backend.Domain.Entities.Core;

public class ClearedConversation
{
    public Guid ConversationId { get; set; }
    public string UserId { get; set; }
    public DateTime ClearedAt { get; set; }
}