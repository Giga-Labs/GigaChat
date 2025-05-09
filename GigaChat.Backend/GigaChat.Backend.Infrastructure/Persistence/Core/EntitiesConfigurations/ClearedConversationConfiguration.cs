using GigaChat.Backend.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigaChat.Backend.Infrastructure.Persistence.Core.EntitiesConfigurations;

public class ClearedConversationConfiguration : IEntityTypeConfiguration<ClearedConversation>
{
    public void Configure(EntityTypeBuilder<ClearedConversation> builder)
    {
        builder.ToTable("ClearedConversations");

        builder.HasKey(c => new { c.ConversationId, c.UserId });

        builder.Property(c => c.ConversationId)
            .IsRequired();

        builder.Property(c => c.UserId)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(c => c.ClearedAt)
            .IsRequired();

        builder.HasIndex(c => c.UserId);
    }
}