using GigaChat.Backend.Domain.Entities.Identity;
using GigaChat.Backend.Infrastructure.Persistence.Identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigaChat.Backend.Infrastructure.Persistence.Identity.EntitiesConfigurations;

public class OtpVerificationConfigurations : IEntityTypeConfiguration<OtpVerification>
{
    public void Configure(EntityTypeBuilder<OtpVerification> builder)
    {
        {
            builder.ToTable("OtpVerifications");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.HashedOtpCode)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(o => o.Purpose)
                .IsRequired()
                .HasConversion<string>()
                .HasMaxLength(50);

            builder.Property(o => o.CreatedAt)
                .IsRequired();

            builder.Property(o => o.ExpiresAt)
                .IsRequired();

            builder.Property(o => o.RevokedAt)
                .IsRequired(false);

            builder.Property(o => o.Metadata)
                .HasMaxLength(512);

            builder.Property(o => o.UserId)
                .IsRequired();

            builder.HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .HasConstraintName("FK_Otp_User")
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(x => x.DeletedById)
                .HasConstraintName("FK_Otp_DeletedBy")
                .OnDelete(DeleteBehavior.SetNull);
            
            builder.HasIndex(o => new { o.UserId, o.Purpose })
                .HasFilter("[DeletedAt] IS NULL");
            
            builder.HasIndex(o => o.ExpiresAt);
        }
    }
}