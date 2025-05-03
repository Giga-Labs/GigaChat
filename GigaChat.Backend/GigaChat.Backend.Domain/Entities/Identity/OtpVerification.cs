using GigaChat.Backend.Domain.Enums.Identity;

namespace GigaChat.Backend.Domain.Entities.Identity;

public class OtpVerification
{
    public Guid Id { get; set; }
    public string HashedOtpCode { get; set; } = string.Empty;
    public OtpPurpose Purpose { get; set; } = OtpPurpose.PasswordReset;
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsUsed { get; set; }

    public DateTime? RevokedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedById { get; set; }
    public string? Metadata { get; set; }
    
    public string UserId { get; set; } = string.Empty;
}
