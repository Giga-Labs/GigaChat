using GigaChat.Backend.Domain.Entities.Identity;
using GigaChat.Backend.Domain.Interfaces.Identity;
using Microsoft.AspNetCore.Identity;

namespace GigaChat.Backend.Infrastructure.Persistence.Identity.Entities;

public class ApplicationUser : IdentityUser, IApplicationUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? PasswordChangedAt { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public List<RefreshToken> RefreshTokens { get; set; } = [];
    public string? ProfilePictureUrl { get; set; }
    public bool AllowGroupInvites { get; set; }
}