using FluentValidation;
using BookingProject.Models;
using BookingProject.DTOs;

namespace BookingProject.FluentValidation;

public class RegisterDtoValidator : AbstractValidator<UsersDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .Must(x => !string.IsNullOrEmpty(x))
            .WithMessage("Invalid name")
            .MinimumLength(3).WithMessage("Short name")
            .MaximumLength(25).WithMessage("Long name");

        RuleFor(x => x.Email)
            .NotNull()
            .NotEmpty()
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage("Invalid email")
            .EmailAddress()
            .Must(x => x.Contains('@') && x.Contains('.'))
            .WithMessage("Invalid email format")
            .Must(x => !x.Contains(',') || !x.Contains('/'))
            .WithMessage("Invalid mail format");

        RuleFor(x => x.Password)
            .NotNull()
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage("Password is null")
            .MinimumLength(8)
            .WithMessage("Short password")
            .Must(x => x.Contains('!') || x.Contains('@') || x.Contains('#') 
                       || x.Contains('$') || x.Contains('%') || x.Contains('&'))
            .WithMessage("Missing special characters");

        RuleFor(x => x.Phone)
            .NotNull()
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage("Phone is null")
            .MinimumLength(11)
            .WithMessage("Invalid phone number");
    }
}