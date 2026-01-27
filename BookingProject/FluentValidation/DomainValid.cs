using BookingProject.Models;
using FluentValidation;
using BookingProject.DTOs;

namespace BookingProject.FluentValidation;

public class DomainValid : AbstractValidator<DomainDto>
{
    public DomainValid()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage("Domain is null")
            .MinimumLength(3)
            .WithMessage("Short domain")
            .MaximumLength(25)
            .WithMessage("Long domain");

        RuleFor(x => x.TDL)
            .NotNull()
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage("Domain is null")
            .Must(x => x.Contains('.'))
            .WithMessage("Invalid domain")
            .MinimumLength(8)
            .WithMessage("Invalid domain length");

        RuleFor(x => x.Price)
            .NotNull()
            .WithMessage("Price is null")
            .ExclusiveBetween(100, 10000)
            .WithMessage("Invalid price domain");
    }
}