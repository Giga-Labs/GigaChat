using GigaChat.Backend.Domain.Interfaces.Identity;

namespace GigaChat.Backend.Application.Repositories.Identity;

public interface IUserRepository
{
    Task<IApplicationUser?> FindByEmailAsync(string email);
    Task<IApplicationUser?> FindByUserNameAsync(string userName);
    Task<IApplicationUser?> FindByIdAsync(string userId);
    Task<bool> CheckPasswordAsync(IApplicationUser user, string password);
    Task UpdateAsync(IApplicationUser user);
    Task<bool> CreateUserAsync(IApplicationUser user, string password);
    Task<string> GenerateEmailConfirmationTokenAsync(IApplicationUser user);
    Task<bool> ConfirmEmailAsync(IApplicationUser user, string code);
    Task<string> GeneratePasswordResetTokenAsync(IApplicationUser user);
    Task<bool> ResetPasswordAsync(IApplicationUser user, string token, string newPassword);
    Task<bool> ChangePasswordAsync(IApplicationUser user, string currentPassword, string newPassword);
    Task<IReadOnlyList<IApplicationUser>> GetUsersByIdsAsync(IReadOnlyList<string> userIds, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<IApplicationUser>> GetUsersByEmailsAsync(IReadOnlyList<string> emails, CancellationToken cancellationToken = default);
}