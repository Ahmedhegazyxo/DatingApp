using Api.Repositories;

namespace Api.Services;

public class ProfileLikeRepository : BaseRepository<ProfileLike, Guid>, IProfileLikeRepository
{
    public ProfileLikeRepository(ApplicationDbContext context) : base(context)
    {
    }
}