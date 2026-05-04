using FluentValidation;

namespace Api.Validators;
public class UpdateProfileValidator : AbstractValidator<UpdateProfileDTO> , IValidator<UpdateProfileDTO>
{
    public UpdateProfileValidator()
    {
        RuleFor(e=>e.Birthdate).LessThan(DateTime.UtcNow);
        RuleFor(e=>e.Username).NotNull().NotEmpty().WithMessage("Required Field");
        RuleFor(e=>e.FirstName).NotNull().NotEmpty().WithMessage("Required Field");
        RuleFor(e=>e.LastName).NotNull().NotEmpty().WithMessage("Required Field");
        RuleFor(e=>e.PhoneNumber).NotNull().NotEmpty().WithMessage("Required Field");
    }
}