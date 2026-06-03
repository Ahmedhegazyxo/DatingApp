using Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.EntityConfigurations;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProfileConfigurations());
        modelBuilder.ApplyConfiguration(new UserConfigurations());
        modelBuilder.ApplyConfiguration(new ProfileLikeConfigurations());
        modelBuilder.ApplyConfiguration(new ProfileMatchConfigurations());
        modelBuilder.ApplyConfiguration(new ProfilePhotoConfigurations());
        modelBuilder.ApplyConfiguration(new AttachmentConfigurations());
        modelBuilder.ApplyConfiguration(new MatchChatMessageConfigurations());
        base.OnModelCreating(modelBuilder);
    }
}