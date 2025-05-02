using System.ComponentModel.DataAnnotations;

namespace GigaChat.Backend.Application.Settings;

public class OtpRateSettings
{
    [Required]
    public static string SectionName { get; } = "OtpRateSettings";
    [Required]
    public int MaxAttempts { get; set; }
    [Required]
    public int RateLimitWindowByHours { get; set; }
}