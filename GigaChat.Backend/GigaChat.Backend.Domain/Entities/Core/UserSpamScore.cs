namespace GigaChat.Backend.Domain.Entities.Core;

public class UserSpamScore
{
    public string UserId { get; set; }
    public int ReportsReceived { get; set; }
    public bool IsMarkedAsSpammer { get; set; }
}