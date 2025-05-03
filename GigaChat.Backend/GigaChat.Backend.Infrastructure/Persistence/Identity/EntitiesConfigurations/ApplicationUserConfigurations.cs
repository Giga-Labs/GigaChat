using GigaChat.Backend.Infrastructure.Persistence.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigaChat.Backend.Infrastructure.Persistence.Identity.EntitiesConfigurations;

public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(user => user.FirstName).HasMaxLength(100);
        builder.Property(user => user.LastName).HasMaxLength(100);

        builder.OwnsMany(user => user.RefreshTokens)
            .ToTable("RefreshTokens")
            .WithOwner()
            .HasForeignKey("UserId");
        
        builder.HasIndex(u => u.Email)
            .HasFilter("[DeletedAt] IS NULL");
        
        builder.HasIndex(u => u.UserName)
            .HasFilter("[DeletedAt] IS NULL");
        
        builder.HasIndex(u => u.DeletedAt);
    }
}