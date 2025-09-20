using GigaChat.Backend.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigaChat.Backend.Infrastructure.Persistence.Core.EntitiesConfigurations;

public class BlockedUserConfiguration : IEntityTypeConfiguration<BlockedUser>
{
    public void Configure(EntityTypeBuilder<BlockedUser> builder)
    {
        builder.ToTable("BlockedUsers");
        
        builder.HasKey(b => new { b.UserId, b.BlockedUserId });

        builder.Property(b => b.UserId)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(b => b.BlockedUserId)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(b => b.BlockedAt)
            .IsRequired();

        builder.HasIndex(b => b.UserId);
        builder.HasIndex(b => b.BlockedUserId);
    }
}