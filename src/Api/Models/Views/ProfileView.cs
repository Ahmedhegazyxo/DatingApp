using Api.Enums;

public class ProfileView
{
    public Guid ProfileId {get;set;}
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime Birthdate { get; set; } = new();
    public Gender Gender { get; set; }
}