using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.EntityConfigurations;

public class ProfileLikeConfigurations : IEntityTypeConfiguration<ProfileLike>
{
    public void Configure(EntityTypeBuilder<ProfileLike> builder)
    {
        builder.ToTable("ProfileLikes");
        builder.HasKey(e => e.Id);
        builder.HasOne(e => e.CreatorProfile).WithMany(e => e.LikesSent).HasForeignKey(e => e.CreatorId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.ReceptorProfile).WithMany(e => e.LikesReceived).HasForeignKey(e => e.ReceptorId).OnDelete(DeleteBehavior.Restrict);
    }
}