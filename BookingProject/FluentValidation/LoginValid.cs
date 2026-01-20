using BookingProject.Models;
using FluentValidation;

namespace BookingProject.FluentValidation;

public class LoginValid : AbstractValidator<LoginRequest>
{
    public LoginValid()
    {
        RuleFor(x => x.Email)
            .NotNull()
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage("Invalid email")
            .EmailAddress()
            .Must(x => x.Contains('@') && x.Contains('.'))
            .WithMessage("Invalid email format")
            .Must(x => !x.Contains(',') || !x.Contains('/'))
            .WithMessage("Invalid mail format");
        
    }
}