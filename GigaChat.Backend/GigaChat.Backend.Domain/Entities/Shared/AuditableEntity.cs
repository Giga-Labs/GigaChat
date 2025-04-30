namespace GigaChat.Backend.Domain.Entities.Shared;

public class AuditableEntity
{
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedOn { get; set; }
    public DateTime? DeletedOn { get; set; }

    public string CreatedById { get; set; } = default!;
    public string? UpdatedById { get; set; }
    public string? DeletedById { get; set; }
}