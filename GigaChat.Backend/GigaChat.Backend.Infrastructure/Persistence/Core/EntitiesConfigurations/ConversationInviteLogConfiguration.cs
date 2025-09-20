using GigaChat.Backend.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigaChat.Backend.Infrastructure.Persistence.Core.EntitiesConfigurations;

public class ConversationInviteLogConfiguration : IEntityTypeConfiguration<ConversationInviteLog>
{
    public void Configure(EntityTypeBuilder<ConversationInviteLog> builder)
    {
        builder.ToTable("ConversationInviteLogs");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
            .IsRequired();

        builder.Property(i => i.ConversationId)
            .IsRequired();

        builder.Property(i => i.InviterId)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(i => i.InviteeId)
            .IsRequired()
            .HasMaxLength(64);

        builder.HasOne(i => i.Conversation)
            .WithMany()
            .HasForeignKey(i => i.ConversationId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(i => i.InviteeId);
        builder.HasIndex(i => i.InviterId);
        builder.HasIndex(i => i.ConversationId);
    }
}