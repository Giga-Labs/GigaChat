namespace GigaChat.Backend.Domain.Entities.Core;

public class DeletedMessage
{
    public Guid MessageId { get; set; }
    public string UserId { get; set; }
    public DateTime DeletedAt { get; set; }
}