using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigaChat.Backend.Infrastructure.Persistence.Core.EntitiesConfigurations;

public class FileUploadMetadata : IEntityTypeConfiguration<Domain.Entities.Core.FileUploadMetadata>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Core.FileUploadMetadata> builder)
    {
        builder.ToTable("FileUploadMetadata");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.Id)
            .IsRequired();

        builder.Property(f => f.UploadedById)
            .IsRequired()
            .HasMaxLength(64);

        builder.Property(f => f.Url)
            .IsRequired()
            .HasMaxLength(512);

        builder.Property(f => f.FileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(f => f.MimeType)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(f => f.SizeInBytes)
            .IsRequired();

        builder.Property(f => f.IsVoice)
            .IsRequired();

        builder.Property(f => f.UploadedAt)
            .IsRequired();

        builder.HasIndex(f => f.UploadedById);
    }
}