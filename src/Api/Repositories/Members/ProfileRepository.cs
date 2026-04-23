namespace Api.Repositories.Members;

public class ProfileRepository : BaseRepository<Profile, Guid>, IProfileRepository
{
    public ProfileRepository(ApplicationDbContext context) : base(context) { }
}