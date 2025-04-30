using GigaChat.Backend.Domain.Entities.Identity;

namespace GigaChat.Backend.Domain.Interfaces.Identity;

public interface IApplicationUser
{ 
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PhoneNumber { get; set; }
    
    // Security Features
    public DateTime? PasswordChangedAt { get; set; }
    public bool EmailConfirmed { get; set; }

    // Activity Tracking
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    
    public List<RefreshToken> RefreshTokens { get; set; }
}