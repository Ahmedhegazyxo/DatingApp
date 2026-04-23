using Api.Enums;

namespace Api.DTOs;
public class RegisterDTO
{
    public string Email {get;set;} = string.Empty;
    public string FirstName {get;set;} = string.Empty;
    public string LastName {get;set;} = string.Empty;
    public string Password {get;set;} = string.Empty;
    public string Username {get;set;} = string.Empty;
    public string PhoneNumber {get;set;} = string.Empty;
    public DateTime Birthdate {get;set;}
    public Gender Gender {get;set;} 
}