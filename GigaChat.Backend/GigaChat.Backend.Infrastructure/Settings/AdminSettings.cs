using System.ComponentModel.DataAnnotations;

namespace GigaChat.Backend.Infrastructure.Settings;

public class AdminSettings
{
    public static string SectionName { get; } = "AdminSettings";
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string UserName { get; set; }
    [Required]
    public string FirstName { get; set; }
    [Required]
    public string LastName { get; set; }
}