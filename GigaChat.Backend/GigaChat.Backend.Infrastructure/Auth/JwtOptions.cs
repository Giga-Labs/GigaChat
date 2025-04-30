using System.ComponentModel.DataAnnotations;

namespace GigaChat.Backend.Infrastructure.Auth;

public class JwtOptions
{
    [Required] 
    public static string SectionName { get; } = "Jwt";

    [Required] public string Key { get; set; } = string.Empty;
    [Required] public string ConfirmationEmailKey { get; set; } = string.Empty;
    [Required] public string Issuer { get; set; } = string.Empty;
    [Required] public string Audience { get; set; } = string.Empty;
    [Range(1, int.MaxValue)] public int ExpiryMinutes { get; set; }
    [Range(1, int.MaxValue)] public int ConfirmationEmailExpiryMinutes { get; set; }

}