using BookingProject.DTOs;
using BookingProject.Models;
using BookingProject.Services;
using FluentValidation;

namespace BookingProject.FluentValidation;

public class LoginDtoValidate : AbstractValidator<LoginDto>
{
    public LoginDtoValidate()
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