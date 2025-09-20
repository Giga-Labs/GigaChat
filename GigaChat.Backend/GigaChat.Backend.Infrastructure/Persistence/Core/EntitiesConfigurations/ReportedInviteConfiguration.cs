using GigaChat.Backend.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigaChat.Backend.Infrastructure.Persistence.Core.EntitiesConfigurations;

public class ReportedInviteConfiguration : IEntityTypeConfiguration<ReportedInvite>
{
    public void Configure(EntityTypeBuilder<ReportedInvite> builder)
    {
        builder.ToTable("ReportedInvites");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Id)
            .IsRequired();

        builder.Property(r => r.InviterId)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(r => r.InviteeId)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(r => r.Reason)
            .HasMaxLength(1000);

        builder.Property(r => r.ReportedAt)
            .IsRequired();

        builder.HasIndex(r => r.InviterId);
        builder.HasIndex(r => r.InviteeId);

        builder.HasIndex(r => new { r.InviterId, r.InviteeId }).IsUnique();
    }
}