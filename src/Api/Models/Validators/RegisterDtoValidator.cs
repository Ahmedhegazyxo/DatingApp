using FluentValidation;
using FluentValidation.Results;

namespace Api.Validators;
public class RegisterDtoValidator : AbstractValidator<RegisterDTO> , IValidator<RegisterDTO>
{
    public RegisterDtoValidator()
    {
        RuleFor(e=>e.Birthdate).LessThan(DateTime.UtcNow);
        RuleFor(e=>e.Email).NotNull().NotEmpty().WithMessage("Required Field");
        RuleFor(e=>e.Username).NotNull().NotEmpty().WithMessage("Required Field");
        RuleFor(e=>e.FirstName).NotNull().NotEmpty().WithMessage("Required Field");
        RuleFor(e=>e.LastName).NotNull().NotEmpty().WithMessage("Required Field");
        RuleFor(e=>e.PhoneNumber).NotNull().NotEmpty().WithMessage("Required Field");
        RuleFor(e=>e.Password).NotNull().NotEmpty().WithMessage("Required Field").MinimumLength(8).WithMessage("Password must be more than 8 characters");
        RuleFor(e=>e.Email).EmailAddress().WithMessage("Must be a valid mail");
    }
}