using BookingProject.Models;
using FluentValidation;
using BookingProject.DTOs;

namespace BookingProject.FluentValidation;

public class OrderValid : AbstractValidator<OrderDto>
{
    public OrderValid()
    {
        RuleFor(x => x.StartData)
            .NotEmpty()
            .WithMessage("Check-in date is required")
            
            .GreaterThan(DateTime.Today)
            .WithMessage("The check-in date must be in the future")
            
            .LessThan(x => x.ExpiresData)
            .WithMessage("The check-in date must be before the check-out date");
        
        RuleFor(x => x.ExpiresData)
            .NotEmpty()
            .WithMessage("Departure date is required")
            
            .GreaterThan(x => x.StartData)
            .WithMessage("The check-out date must be later than the check-in date")
            
            .LessThan(x => x.StartData.AddMonths(12))
            .WithMessage("The maximum order duration is 12 months.");
        
        // Проверка минимальной длительности
        RuleFor(x => x)
            .Must(order => 
                (order.ExpiresData - order.StartData).TotalDays >= 30)
            .WithMessage("Minimum order: 30 days")
            
            .Must(order =>
                (order.ExpiresData - order.StartData).TotalDays <= 365)
            .WithMessage("Maximum order period is 365 days");

    }
}