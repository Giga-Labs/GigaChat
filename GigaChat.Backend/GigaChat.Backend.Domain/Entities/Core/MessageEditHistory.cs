namespace GigaChat.Backend.Domain.Entities.Core;

public class MessageEditHistory
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid MessageId { get; set; }
    public Message Message { get; set; }
    public string? OldContent { get; set; }
    public DateTime EditedAt { get; set; }
    public string EditedById { get; set; }
}