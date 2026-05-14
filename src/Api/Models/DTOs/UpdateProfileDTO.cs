using Api.Enums;

namespace Api.DTOs;
public class UpdateProfileDTO
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string? Biography { get; set; }
    public string? Username { get; set; }
    public DateTime Birthdate { get; set; } = new();
    public Gender Gender { get; set; }
}