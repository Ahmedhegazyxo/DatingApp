using Api.Enums;

namespace Api.Entities;

public class ProfilePreference : BaseEntity<Guid>
{
    public Guid ProfileId { get; private set; }
    public virtual Profile? Profile { get; private set; }
    public int? PreferredCityId {get;private set;}
    public Gender PreferredGender {get;private set;}

    public static ProfilePreference Create(Gender preferredGender,int? preferredCityId = default)
    {
        return new ProfilePreference
        {
            PreferredGender = preferredGender,
            PreferredCityId = preferredCityId
        };
    }
}