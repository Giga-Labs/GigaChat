using GigaChat.Backend.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigaChat.Backend.Infrastructure.Persistence.Core.EntitiesConfigurations;

public class MessageEditHistoryConfiguration : IEntityTypeConfiguration<MessageEditHistory>
{
    public void Configure(EntityTypeBuilder<MessageEditHistory> builder)
    {
        builder.ToTable("MessageEditHistories");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Id)
            .IsRequired();

        builder.Property(e => e.MessageId)
            .IsRequired();

        builder.Property(e => e.OldContent)
            .HasMaxLength(5000);

        builder.Property(e => e.EditedAt)
            .IsRequired();

        builder.Property(e => e.EditedById)
            .IsRequired()
            .HasMaxLength(64);

        builder.HasOne(e => e.Message)
            .WithMany(m => m.EditHistory)
            .HasForeignKey(e => e.MessageId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(e => e.MessageId);
        builder.HasIndex(e => e.EditedById);
    }
}