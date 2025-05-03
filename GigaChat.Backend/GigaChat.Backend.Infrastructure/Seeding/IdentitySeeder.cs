using GigaChat.Backend.Infrastructure.Persistence.Identity.Entities;
using GigaChat.Backend.Infrastructure.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GigaChat.Backend.Infrastructure.Seeding;

public static class IdentitySeeder
{
    public static async Task SeedAdminUserAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var options = serviceProvider.GetRequiredService<IOptions<AdminSettings>>();
        var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("IdentitySeeder");

        var adminSettings = options.Value;

        if (string.IsNullOrWhiteSpace(adminSettings.Email) || string.IsNullOrWhiteSpace(adminSettings.Password))
            throw new InvalidOperationException("Admin email or password is not configured properly.");


        var adminUser = await userManager.FindByEmailAsync(adminSettings.Email);
        if (adminUser is null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminSettings.UserName,
                Email = adminSettings.Email,
                EmailConfirmed = true,
                CreatedAt = DateTime.UtcNow,
                FirstName = adminSettings.FirstName,
                LastName = adminSettings.LastName,
            };

            var result = await userManager.CreateAsync(adminUser, adminSettings.Password);
            if (!result.Succeeded)
                throw new Exception("Failed to create admin user: " +
                                    string.Join(", ", result.Errors.Select(e => e.Description)));
        }
        else
        {
            logger.LogInformation("Admin user already seeded.");
        }

        logger.LogInformation("Admin user was seeded successfully.");
    }
}