using GigaChat.Backend.Domain.Entities.Shared;

namespace GigaChat.Backend.Domain.Entities.Core;

public class ConversationInviteLog : AuditableEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid ConversationId { get; set; }
    public Conversation Conversation { get; set; } = default!;
    public string InviterId { get; set; } = default!;
    public string InviteeId { get; set; } = default!;
    public bool IsAccepted { get; set; }
    public bool IsReported { get; set; }
}