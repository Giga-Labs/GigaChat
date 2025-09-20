using System.ComponentModel.DataAnnotations;

namespace GigaChat.Backend.Infrastructure.Config.Otp;

public class PerPurposeOtpConfig
{
    [Required]
    public int MaxAttempts { get; set; }

    [Required]
    public int WindowMinutes { get; set; }
    [Required]
    public int ExpiryMinutes { get; set; }
}