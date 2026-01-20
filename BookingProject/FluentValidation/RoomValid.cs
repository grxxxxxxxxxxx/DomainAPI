using BookingProject.Models;
using FluentValidation;

namespace BookingProject.FluentValidation;

public class RoomValid : AbstractValidator<Room>
{
    public RoomValid()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage("Name is null")
            .MinimumLength(3)
            .WithMessage("Short name")
            .MaximumLength(25)
            .WithMessage("Long name");

        RuleFor(x => x.Class)
            .NotNull()
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage("Class is null")
            .Must(x => x.Contains("Standard")
                       || x.Contains("Gold") || x.Contains("Platinum"))
            .WithMessage("Invalid class")
            .MaximumLength(8)
            .WithMessage("Invalid class length");

        RuleFor(x => x.Description)
            .NotNull()
            .WithMessage("Description is null")
            .MinimumLength(15)
            .WithMessage("Short description")
            .MaximumLength(50)
            .WithMessage("Long description");

        RuleFor(x => x.Price)
            .NotNull()
            .WithMessage("Price is null")
            .ExclusiveBetween(1000, 10000)
            .WithMessage("Invalid price room");
    }
}