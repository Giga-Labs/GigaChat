namespace GigaChat.Backend.Domain.Entities.Core;

public class UserSettings
{
    public string UserId { get; set; }
    public bool AllowGroupInvites { get; set; } = true;
}