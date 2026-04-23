namespace Api.Entities;

public class ProfileLike : BaseEntity<Guid>
{
    public virtual Profile? CreatorProfile { get; private set; }
    public virtual Profile? ReceptorProfile { get; private set; }
    public Guid CreatorId { get; private set; }
    public Guid ReceptorId { get; private set; }
    public static ProfileLike Create(Guid creatorId, Guid receptorId)
    {
        return new ProfileLike
        {
            CreatorId = creatorId,
            ReceptorId = receptorId
        };
    }
}