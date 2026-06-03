using Api.Enums;

namespace Api.Views;

public class MemberView
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public DateTime Birthdate { get; set; }
    public Gender Gender { get; set; }
    public bool IsLiked {get;set;}
    public bool IsMatched {get;set;}
    public Guid? MatchId {get;set;}
    public string? ProfilePhotoId {get;set;}
    public double Age => Math.Round((DateTime.UtcNow - Birthdate).TotalDays / 365 );

}