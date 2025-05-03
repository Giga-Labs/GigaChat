using System.ComponentModel.DataAnnotations;
using GigaChat.Backend.Infrastructure.Config.Otp;

namespace GigaChat.Backend.Infrastructure.Settings;

public class OtpSettings
{
    public static string SectionName { get; } = "OtpSettings";

    [Required]
    public string SecretKey { get; set; } = string.Empty;

    [Required]
    public PerPurposeOtpConfig Verification { get; set; } = new();

    [Required]
    public PerPurposeOtpConfig PasswordReset { get; set; } = new();
    
    [Required]
    public PerPurposeOtpConfig TwoFactor { get; set; } = new();
}