namespace GigaChat.Backend.Domain.Entities.Core;

public class PinnedMessage
{ 
    public Guid MessageId { get; set; }
    public Message Message { get; set; }
    public string PinnedById { get; set; }
    public DateTime PinnedAt { get; set; }
}