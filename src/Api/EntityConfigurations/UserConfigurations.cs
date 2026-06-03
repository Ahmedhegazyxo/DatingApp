using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.EntityConfigurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
        builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.PhoneNumber).IsUnique();
        builder.HasIndex(u => u.Username).IsUnique();
        builder.Property(u => u.PhoneNumber).HasMaxLength(50);
        builder.Property(u => u.Username).HasMaxLength(128);
        builder.HasOne(e => e.Profile).WithOne(e => e.User).HasForeignKey<User>(e => e.Id).OnDelete(DeleteBehavior.Cascade);
    }
}