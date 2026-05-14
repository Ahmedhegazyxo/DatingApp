namespace Api.Repositories;

public class AttachmentRepository : BaseRepository<Attachment, Guid>, IAttachmentRepository
{
    public AttachmentRepository(ApplicationDbContext context) : base(context)
    {
    }
}