namespace Api.Views;
public class LikeResponseView
{
    public Guid UserId {get;set;}
    public bool IsLiked {get;set;} = false;
    public bool IsMatched {get;set;} = false;
}