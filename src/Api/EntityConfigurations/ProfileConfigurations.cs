using Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.EntityConfigurations;

public class ProfileConfigurations : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.ToTable("Profiles");
        builder.HasKey(e=>e.Id);
        builder.HasOne(e=>e.ProfilePhoto).WithOne().HasForeignKey<ProfilePhoto>(e=>e.Id).OnDelete(DeleteBehavior.Cascade);
        builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}