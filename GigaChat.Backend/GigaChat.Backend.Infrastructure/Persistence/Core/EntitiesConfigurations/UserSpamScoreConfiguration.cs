using GigaChat.Backend.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigaChat.Backend.Infrastructure.Persistence.Core.EntitiesConfigurations;

public class UserSpamScoreConfiguration : IEntityTypeConfiguration<UserSpamScore>
{
    public void Configure(EntityTypeBuilder<UserSpamScore> builder)
    {
        builder.ToTable("UserSpamScores");

        builder.HasKey(s => s.UserId);

        builder.Property(s => s.UserId)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(s => s.ReportsReceived)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(s => s.IsMarkedAsSpammer)
            .IsRequired()
            .HasDefaultValue(false);

        builder.HasIndex(s => s.IsMarkedAsSpammer);
    }
}