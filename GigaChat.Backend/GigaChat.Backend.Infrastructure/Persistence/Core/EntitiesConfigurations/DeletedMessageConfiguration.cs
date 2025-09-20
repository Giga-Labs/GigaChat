using GigaChat.Backend.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigaChat.Backend.Infrastructure.Persistence.Core.EntitiesConfigurations;

public class DeletedMessageConfiguration : IEntityTypeConfiguration<DeletedMessage>
{
    public void Configure(EntityTypeBuilder<DeletedMessage> builder)
    {
        builder.ToTable("DeletedMessages");

        builder.HasKey(dm => new { dm.MessageId, dm.UserId });

        builder.Property(dm => dm.MessageId)
            .IsRequired();

        builder.Property(dm => dm.UserId)
            .IsRequired()
            .HasMaxLength(64); 

        builder.Property(dm => dm.DeletedAt)
            .IsRequired();

        builder.HasIndex(dm => dm.UserId);
    }
}