using GigaChat.Backend.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigaChat.Backend.Infrastructure.Persistence.Core.EntitiesConfigurations;

public class MessageReactionConfiguration : IEntityTypeConfiguration<MessageReaction>
{
    public void Configure(EntityTypeBuilder<MessageReaction> builder)
    {
        builder.ToTable("MessageReactions");

        builder.HasKey(r => new { r.MessageId, r.UserId});

        builder.Property(r => r.MessageId)
            .IsRequired();

        builder.Property(r => r.UserId)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(r => r.Emoji)
            .IsRequired()
            .HasMaxLength(16);

        builder.Property(r => r.ReactedAt)
            .IsRequired();

        builder.HasOne(r => r.Message)
            .WithMany(m => m.Reactions)
            .HasForeignKey(r => r.MessageId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(r => r.UserId);
    }
}