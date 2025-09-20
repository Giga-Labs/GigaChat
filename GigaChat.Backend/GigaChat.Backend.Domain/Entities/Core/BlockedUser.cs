namespace GigaChat.Backend.Domain.Entities.Core;

public class BlockedUser
{
    public string UserId { get; set; }
    public string BlockedUserId { get; set; }
    public DateTime BlockedAt { get; set; }
}