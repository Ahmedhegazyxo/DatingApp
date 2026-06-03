namespace Api.Entities;

public class ProfileMatch : BaseEntity<Guid>
{
    public Guid CreatorProfileId { get; private set; }
    public Guid ReceptorProfileId { get; private set; }
    public virtual Profile? CreatorProfile { get; private set; }
    public virtual Profile? ReceptorProfile { get; private set; }
    public virtual List<ChatMessage> MatchChatMessages { get; private set; } = [];
    public static ProfileMatch Create(Guid creatorProfileId, Guid receptorProfileId)
    {
        return new ProfileMatch
        {
            CreatorProfileId = creatorProfileId,
            ReceptorProfileId = receptorProfileId
        };
    }
}