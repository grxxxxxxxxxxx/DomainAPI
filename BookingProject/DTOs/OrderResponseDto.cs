using BookingProject.Models;
namespace BookingProject.DTOs;

public class OrderResponseDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid DomainId { get; set; }
    public Domain Domain { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime ExpiresData { get; set; }
    
    public List<OrderDto> Orders { get; set; }
    
}