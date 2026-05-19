using Api.Enums;
namespace Api.Entities;

public class User : BaseEntity<Guid>
{
    public string Username { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public bool Deactivated { get; private set; }
    public string PhoneNumber { get; private set; } = string.Empty;
    public string HashPassword { get; private set; } = string.Empty;
    public string HashSalt { get; private set; } = string.Empty;
    public DateTime Birthdate { get; private set; }
    public virtual Profile? Profile { get; private set; }
    public static User Create(string username,
     string email,
      DateTime birthdate,
       string hashPassword,
        string hashSalt,
        string firstName,
        string lastName,
        string phoneNumber,
        Gender gender)
    {
        return new User
        {
            Birthdate = birthdate,
            Username = username.ToLower(),
            Email = email.ToLower(),
            HashPassword = hashPassword,
            HashSalt = hashSalt,
            PhoneNumber = phoneNumber,
            Profile = Profile.Create(firstName, lastName, gender)
        };
    }
    public void Update(string? username = null, DateTime? birthdate = null)
    {
        if (username != null)
            this.Username = username.ToLower();
        if (birthdate != null)
            this.Birthdate = birthdate.Value;
    }
}