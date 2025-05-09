using GigaChat.Backend.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigaChat.Backend.Infrastructure.Persistence.Core.EntitiesConfigurations;

public class ConversationMemberConfiguration : IEntityTypeConfiguration<ConversationMember>
{
    public void Configure(EntityTypeBuilder<ConversationMember> builder)
    {
        builder.ToTable("ConversationMembers");

        builder.HasKey(m => new { m.ConversationId, m.UserId });

        builder.Property(m => m.ConversationId)
            .IsRequired();

        builder.Property(m => m.UserId)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(m => m.JoinedAt)
            .IsRequired();

        builder.Property(m => m.InvitedById)
            .HasMaxLength(64);

        builder.Property(m => m.IsMuted)
            .IsRequired();

        builder.Property(m => m.IsAdmin)
            .IsRequired();

        builder.HasOne(m => m.Conversation)
            .WithMany(c => c.Members)
            .HasForeignKey(m => m.ConversationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(m => m.UserId);
        builder.HasIndex(m => m.IsAdmin);
    }
}