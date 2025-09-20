using GigaChat.Backend.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigaChat.Backend.Infrastructure.Persistence.Core.EntitiesConfigurations;

public class MessageReceiptConfiguration : IEntityTypeConfiguration<MessageReceipt>
{
    public void Configure(EntityTypeBuilder<MessageReceipt> builder)
    {
        builder.ToTable("MessageReceipts");

        builder.HasKey(r => new { r.MessageId, r.UserId });

        builder.Property(r => r.MessageId)
            .IsRequired();

        builder.Property(r => r.UserId)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(r => r.SeenAt)
            .IsRequired(false);

        builder.Property(r => r.DeliveredAt)
            .IsRequired(false);

        builder.HasOne(r => r.Message)
            .WithMany(m => m.Receipts)
            .HasForeignKey(r => r.MessageId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(r => r.UserId);
        builder.HasIndex(r => r.SeenAt);
        builder.HasIndex(r => r.DeliveredAt);
    }
}