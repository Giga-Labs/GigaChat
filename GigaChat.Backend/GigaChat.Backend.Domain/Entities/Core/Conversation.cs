using GigaChat.Backend.Domain.Entities.Shared;

namespace GigaChat.Backend.Domain.Entities.Core;

public class Conversation : AuditableEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string? Name { get; set; } // null for private chats
    public string? Description { get; set; }

    public bool IsGroup { get; set; } // false = private chat

    public string? AdminId { get; set; } // Only applies to group chats

    public ICollection<ConversationMember> Members { get; set; } = new List<ConversationMember>();
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}