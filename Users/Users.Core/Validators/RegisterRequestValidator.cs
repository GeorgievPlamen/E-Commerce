using FluentValidation;
using Users.Core.DTO;

namespace Users.Core.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Password)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.PersonName)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.Gender)
            .IsInEnum();
    }
}