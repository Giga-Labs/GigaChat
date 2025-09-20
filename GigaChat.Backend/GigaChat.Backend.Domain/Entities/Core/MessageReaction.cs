namespace GigaChat.Backend.Domain.Entities.Core;

public class MessageReaction
{
    public Guid MessageId { get; set; }
    public Message Message { get; set; } = default!;

    public string UserId { get; set; } = default!;

    public string Emoji { get; set; } = "👍";
    public DateTime ReactedAt { get; set; }
}