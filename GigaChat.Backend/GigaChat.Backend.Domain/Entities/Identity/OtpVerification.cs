namespace GigaChat.Backend.Domain.Entities.Identity;

public class OtpVerification
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string OtpCode { get; set; } = string.Empty;
    public DateTime CreatedOn { get; set; }
    public DateTime ExpiresOn { get; set; }
    public bool IsUsed { get; set; }
}
