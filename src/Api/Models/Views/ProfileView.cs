using Api.Enums;
using Humanizer;
using Humanizer.Localisation;

public class ProfileView
{
    public Guid ProfileId {get;set;}
    public string Username { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime Birthdate { get; set; } = new();
    public int Age => CalculateAgeWithMonths(Birthdate);
    public Gender Gender { get; set; }
    public static int CalculateAgeWithMonths(DateTime birthDate)
{
    var today = DateTime.Today;

    int years = today.Year - birthDate.Year;
    int months = today.Month - birthDate.Month;

    if (today.Day < birthDate.Day)
        months--;

    if (months < 0)
    {
        years--;
    }

    return years;
}
}