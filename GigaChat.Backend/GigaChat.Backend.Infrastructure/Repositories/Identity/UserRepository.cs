using GigaChat.Backend.Application.Repositories.Identity;
using GigaChat.Backend.Domain.Interfaces.Identity;
using GigaChat.Backend.Infrastructure.Persistence.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GigaChat.Backend.Infrastructure.Repositories.Identity;

public class UserRepository(UserManager<ApplicationUser> userManager) : IUserRepository
{
    public async Task<IApplicationUser?> FindByEmailAsync(string email)
    {
        return await userManager.FindByEmailAsync(email);
    }

    public async Task<IApplicationUser?> FindByUserNameAsync(string userName)
    {
        return await userManager.FindByNameAsync(userName);
    }

    public async Task<IApplicationUser?> FindByIdAsync(string userId)
    {
        return await userManager.FindByIdAsync(userId);
    }

    public async Task<bool> CheckPasswordAsync(IApplicationUser user, string password)
    {
        if (user is not ApplicationUser appUser)
            return false;

        return await userManager.CheckPasswordAsync(appUser, password);
    }

    public async Task UpdateAsync(IApplicationUser user)
    {
        if (user is ApplicationUser applicationUser)
        {
            await userManager.UpdateAsync(applicationUser);
        }
    }

    public async Task<bool> CreateUserAsync(IApplicationUser user, string password)
    {
        var applicationUser =
            new
                ApplicationUser // this works while mapping using mapster doesn't because doing this creates a new ApplicationUser and ef automatically generates a new id for it meanwhile if we used mapster it won't create an id 
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                };
        var result = await userManager.CreateAsync(applicationUser, password);
        return result.Succeeded;
    }

    public async Task<string> GenerateEmailConfirmationTokenAsync(IApplicationUser user)
    {
        if (user is ApplicationUser applicationUser)
        {
            var result = await userManager.GenerateEmailConfirmationTokenAsync(applicationUser);

            return result;
        }

        throw new Exception("Can't use GenerateEmailConfirmationTokenAsync with non ApplicationUser type objects");
    }

    public async Task<bool> ConfirmEmailAsync(IApplicationUser user, string code)
    {
        if (user is ApplicationUser applicationUser)
        {
            var result = await userManager.ConfirmEmailAsync(applicationUser, code);

            return result.Succeeded;
        }

        throw new Exception("Can't use ConfirmEmailAsync with non ApplicationUser type objects");
    }
    
    public async Task<IReadOnlyList<IApplicationUser>> GetUsersByIdsAsync(IReadOnlyList<string> userIds, CancellationToken cancellationToken = default)
    {
        return await userManager.Users
            .Where(u => userIds.Contains(u.Id))
            .Cast<IApplicationUser>()
            .ToListAsync(cancellationToken);
    }
    
    public async Task<IReadOnlyList<IApplicationUser>> GetUsersByEmailsAsync(IReadOnlyList<string> emails, CancellationToken cancellationToken = default)
    {
        return await userManager.Users
            .Where(u => emails.Contains(u.Email!))
            .Cast<IApplicationUser>()
            .ToListAsync(cancellationToken);
    }
    public async Task<bool> ChangePasswordAsync(IApplicationUser user, string currentPassword, string newPassword)
    {
        if (user is ApplicationUser applicationUser)
        {
            var result = await userManager.ChangePasswordAsync(applicationUser, currentPassword, newPassword);

            applicationUser.UpdatedAt = DateTime.UtcNow;
            await userManager.UpdateAsync(applicationUser);

            return result.Succeeded;
        }

        throw new Exception("Can't use ChangePasswordAsync with non ApplicationUser type objects");

    }
    public async Task<string> GeneratePasswordResetTokenAsync(IApplicationUser user)
    {
        if (user is ApplicationUser applicationUser)
        {
            var result = await userManager.GeneratePasswordResetTokenAsync(applicationUser);

            return result;
        }

        throw new Exception("Can't use GeneratePasswordResetTokenAsync with non ApplicationUser type objects");

    }
    public async Task<bool> ResetPasswordAsync(IApplicationUser user, string token, string newPassword)
    {
        if (user is ApplicationUser applicationUser)
        {
            var result = await userManager.ResetPasswordAsync(applicationUser, token, newPassword);
            
            applicationUser.UpdatedAt = DateTime.UtcNow;
            await userManager.UpdateAsync(applicationUser);

            return result.Succeeded;
        }

        throw new Exception("Can't use ResetPasswordAsync with non ApplicationUser type objects");

    }

}