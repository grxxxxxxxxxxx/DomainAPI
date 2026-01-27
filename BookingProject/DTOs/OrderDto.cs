using BookingProject.Models;
namespace BookingProject.DTOs;

public class OrderDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime StartData { get; set; }
    public DateTime ExpiresData { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid DomainId { get; set; }
    public Domain Domain { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
}