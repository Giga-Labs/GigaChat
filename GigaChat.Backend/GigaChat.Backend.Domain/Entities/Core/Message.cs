using GigaChat.Backend.Domain.Entities.Shared;
using GigaChat.Backend.Domain.Enums.Core;

namespace GigaChat.Backend.Domain.Entities.Core;

public class Message : AuditableEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string SenderId { get; set; } = string.Empty;

    public Guid ConversationId { get; set; }
    public Conversation Conversation { get; set; } = null!;

    public string Content { get; set; } = string.Empty;

    public MessageType Type { get; set; } = MessageType.Text;
    public string? PayloadUrl { get; set; }
    public string? MimeType { get; set; }
    public bool IsVoice { get; set; }

    public ICollection<MessageReceipt> Receipts { get; set; } = new List<MessageReceipt>();
    public ICollection<MessageReaction> Reactions { get; set; } = new List<MessageReaction>();
    public ICollection<MessageEditHistory> EditHistory { get; set; } = new List<MessageEditHistory>();
}