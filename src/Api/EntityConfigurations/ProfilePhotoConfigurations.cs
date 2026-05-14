using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.EntityConfigurations;

public class ProfilePhotoConfigurations : IEntityTypeConfiguration<ProfilePhoto>
{
    public void Configure(EntityTypeBuilder<ProfilePhoto> builder)
    {
        builder.ToTable("ProfilePhotos");
        builder.HasIndex(e => e.AttachmentId).IsUnique();
        builder.HasOne(e => e.Attachment).WithMany().HasForeignKey(e => e.AttachmentId).OnDelete(DeleteBehavior.Cascade);
    }
}