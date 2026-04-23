using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.EntityConfigurations;

public class ProfileMatchConfigurations : IEntityTypeConfiguration<ProfileMatch>
{
    public void Configure(EntityTypeBuilder<ProfileMatch> builder)
    {
        builder.ToTable("ProfileMatches");
        builder.HasOne(e=>e.CreatorProfile).WithMany(e=>e.MatchesSent).HasForeignKey(e=>e.CreatorProfileId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e=>e.ReceptorProfile).WithMany(e=>e.MatchesReceived).HasForeignKey(e=>e.ReceptorProfileId).OnDelete(DeleteBehavior.Restrict);
    }

}