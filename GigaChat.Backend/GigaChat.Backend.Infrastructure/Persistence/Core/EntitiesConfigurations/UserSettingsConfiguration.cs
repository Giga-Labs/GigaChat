using GigaChat.Backend.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigaChat.Backend.Infrastructure.Persistence.Core.EntitiesConfigurations;

public class UserSettingsConfiguration : IEntityTypeConfiguration<UserSettings>
{
    public void Configure(EntityTypeBuilder<UserSettings> builder)
    {
        builder.ToTable("UserSettings");

        builder.HasKey(s => s.UserId);

        builder.Property(s => s.UserId)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(s => s.AllowGroupInvites)
            .IsRequired();

        builder.HasIndex(s => s.UserId);
    }
}