namespace GigaChat.Backend.Domain.Entities.Core;

public class ConversationMember
{
    public Guid ConversationId { get; set; }
    public Conversation Conversation { get; set; }

    public string UserId { get; set; } = string.Empty;

    public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    public string? InvitedById { get; set; }
    public bool IsMuted { get; set; } = false; // if the user muted the conversation 

    public bool IsAdmin { get; set; } = false;
}