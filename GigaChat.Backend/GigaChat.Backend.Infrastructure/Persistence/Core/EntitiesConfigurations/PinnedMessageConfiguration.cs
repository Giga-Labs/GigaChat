using GigaChat.Backend.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigaChat.Backend.Infrastructure.Persistence.Core.EntitiesConfigurations;

public class PinnedMessageConfiguration : IEntityTypeConfiguration<PinnedMessage>
{
    public void Configure(EntityTypeBuilder<PinnedMessage> builder)
    {
        builder.ToTable("PinnedMessages");

        builder.HasKey(p => p.MessageId);

        builder.Property(p => p.PinnedById)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(p => p.PinnedAt)
            .IsRequired();

        builder.HasOne(p => p.Message)
            .WithOne()
            .HasForeignKey<PinnedMessage>(p => p.MessageId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(p => p.PinnedById);
    }
}