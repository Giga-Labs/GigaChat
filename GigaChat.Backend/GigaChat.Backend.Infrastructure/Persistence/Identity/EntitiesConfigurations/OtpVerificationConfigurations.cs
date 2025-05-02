using GigaChat.Backend.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigaChat.Backend.Infrastructure.Persistence.Identity.EntitiesConfigurations;

public class OtpVerifactionConfigurations : IEntityTypeConfiguration<OtpVerification>
{
    public void Configure(EntityTypeBuilder<OtpVerification> builder)
    {
        {

            builder.HasKey(o => o.Id); // Primary key

            builder.Property(o => o.Email)
                .IsRequired();

            builder.Property(o => o.OtpCode)
                .IsRequired()
                .HasMaxLength(6);

            builder.Property(o => o.CreatedOn)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(o => o.ExpiresOn)
                .IsRequired();

            builder.Property(o => o.IsUsed)
                .IsRequired()
                .HasDefaultValue(false);

            // Index for faster lookups
            builder.HasIndex(o => new { o.Email, o.OtpCode })
                .IsUnique(false);

            builder.HasIndex(o => new { o.Email, o.CreatedOn });

            

        }
    }
}