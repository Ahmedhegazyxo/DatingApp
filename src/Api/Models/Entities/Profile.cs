using Api.Enums;

namespace Api.Entities;

public class Profile : BaseEntity<Guid>
{
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public Gender Gender { get; private set; }
    public virtual User? User { get; private set; }
    public virtual ProfilePreference? ProfilePreference { get; private set; }
    public virtual List<ProfileLike> LikesSent { get; private set; } = [];
    public virtual List<ProfileLike> LikesReceived { get; private set; } = [];
    public virtual List<ProfileMatch> MatchesSent { get; private set; } = [];
    public virtual List<ProfileMatch> MatchesReceived { get; private set; } = [];
    public string? Biography { get; private set; }
    public static Profile Create(string firstName, string lastName, Gender gender)
    {
        Gender preferredGender = Gender.Other;
        if (gender == Gender.Male) preferredGender = Gender.Female;
        if (gender == Gender.Female) preferredGender = Gender.Male;
        return new Profile
        {
            FirstName = firstName,
            LastName = lastName,
            Gender = gender,
            ProfilePreference = ProfilePreference.Create(preferredGender)
        };
    }
    public void Update(string? biography, string? firstName = default, string? lastName = default, Gender? gender = null, string? userName = null, DateTime? birthdate = null)
    {
        if (biography is not null) Biography = biography;
        if (firstName is not null) FirstName = firstName;
        if (lastName is not null) LastName = lastName;
        if (gender is not null) Gender = (Gender)gender;
        if (userName is not null || birthdate is not null)
            User!.Update(userName, birthdate);

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