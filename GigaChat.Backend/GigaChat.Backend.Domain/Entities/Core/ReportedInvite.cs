namespace GigaChat.Backend.Domain.Entities.Core;

public class ReportedInvite
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string InviteeId { get; set; } = default!;
    public string InviterId { get; set; } = default!;
    public string? Reason { get; set; }
    public DateTime ReportedAt { get; set; }
}