using FluentValidation;
using FluentValidation.Results;

namespace Api.Validators;
public class LoginDtoValidator : AbstractValidator<LoginDTO> , IValidator<LoginDTO>
{
    public LoginDtoValidator()
    {
        RuleFor(e=>e.Username).NotNull().NotEmpty().WithMessage("Required Field");
        RuleFor(e=>e.Password).NotNull().NotEmpty().WithMessage("Required Field");
        RuleFor(e=>e.Username).MinimumLength(3);
        RuleFor(e=>e.Password).MinimumLength(8).WithMessage("Password must be more than 8 characters");
    }
}