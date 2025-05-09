namespace GigaChat.Backend.Domain.Entities.Shared;

public class AuditableEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public string CreatedById { get; set; } = default!;
    public string? UpdatedById { get; set; }
}