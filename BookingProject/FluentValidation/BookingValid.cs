using BookingProject.Models;
using FluentValidation;

namespace BookingProject.FluentValidation;

public class BookingValid : AbstractValidator<Booking>
{
    public BookingValid()
    {
        RuleFor(x => x.CheckInDate)
            .NotEmpty()
            .WithMessage("Check-in date is required")
            
            .GreaterThan(DateTime.Today)
            .WithMessage("The check-in date must be in the future")
            
            .LessThan(x => x.CheckOutDate)
            .WithMessage("The check-in date must be before the check-out date");
        
        RuleFor(x => x.CheckOutDate)
            .NotEmpty()
            .WithMessage("Departure date is required")
            
            .GreaterThan(x => x.CheckInDate)
            .WithMessage("The check-out date must be later than the check-in date")
            
            .LessThan(x => x.CheckInDate.AddMonths(3))
            .WithMessage("The maximum booking duration is 3 months.");
        
        // Проверка минимальной длительности
        RuleFor(x => x)
            .Must(booking => 
                (booking.CheckOutDate - booking.CheckInDate).TotalDays >= 1)
            .WithMessage("Minimum booking: 1 day")
            
            .Must(booking =>
                (booking.CheckOutDate - booking.CheckInDate).TotalDays <= 30)
            .WithMessage("Maximum booking period is 30 days");

    }
}