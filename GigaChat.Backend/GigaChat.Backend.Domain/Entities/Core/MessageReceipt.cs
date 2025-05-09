namespace GigaChat.Backend.Domain.Entities.Core;

public class MessageReceipt
{
    public Guid MessageId { get; set; }
    public Message Message { get; set; } = null!;

    public string UserId { get; set; } = string.Empty;

    public DateTime? SeenAt { get; set; }
    public DateTime? DeliveredAt { get; set; }
}