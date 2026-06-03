using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.EntityConfigurations;

public class MatchChatMessageConfigurations : IEntityTypeConfiguration<ChatMessage>
{
    public void Configure(EntityTypeBuilder<ChatMessage> builder)
    {
        builder.ToTable("ChatMessages");
        builder.HasKey(e => e.Id);
        builder.HasOne(e => e.MessageMatch).WithMany(e => e.MatchChatMessages).HasForeignKey(e => e.MessageMatchId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(e => e.SenderProfile).WithMany().HasForeignKey(e => e.SenderId).OnDelete(DeleteBehavior.Restrict);
        builder.Property(e => e.CreatedDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
    }
}