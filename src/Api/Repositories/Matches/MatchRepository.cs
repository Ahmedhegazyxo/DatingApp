namespace Api.Repositories;
public class MatchRepository : BaseRepository<ProfileMatch, Guid>, IMatchRepository
{
    public MatchRepository(ApplicationDbContext context) : base(context)
    {
    }
}