using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.EntityConfigurations;

public class AttachmentConfigurations : IEntityTypeConfiguration<Attachment>
{
    public void Configure(EntityTypeBuilder<Attachment> builder)
    {
        builder.ToTable("Attachments");
        builder.HasKey(e => e.Id);
        builder.Property(e => e.FileExtension).HasMaxLength(16);
        builder.Property(e => e.FileName).HasMaxLength(256);
        builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        
    }
}