using Api.Enums;

namespace Api.Entities;

public class Profile : BaseEntity<Guid>
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public Gender Gender { get; private set; }
    public virtual User? User { get; private set; }
    public virtual List<ProfileLike> LikesSent  {get;private set;} = [];
    public virtual List<ProfileLike> LikesReceived  {get;private set;} = [];
    public virtual List<ProfileMatch> MatchesSent {get;private set;} = [];
    public virtual List<ProfileMatch> MatchesReceived {get;private set;} = [];
    public static Profile Create(string firstName, string lastName, Gender gender)
    {
        return new Profile
        {
            FirstName = firstName,
            LastName = lastName,
            Gender = gender
        };
    }
    public void AddLike(Guid creatorId, Guid receptorId)
    {
        LikesSent.Add(ProfileLike.Create(creatorId, receptorId));
    }
    public void AddMatch(Guid creatorId, Guid receptorId)
    {
        MatchesSent.Add(ProfileMatch.Create(creatorId, receptorId));
    }
}