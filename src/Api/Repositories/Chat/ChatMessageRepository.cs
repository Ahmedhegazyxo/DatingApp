namespace Api.Repositories;

public class ChatMessageRepository : BaseRepository<ChatMessage, Guid>, IChatMessageRepository
{
    public ChatMessageRepository(ApplicationDbContext context) : base(context)
    {
    }
}

