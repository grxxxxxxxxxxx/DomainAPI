namespace BookingProject.Models;

public class Order
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime StartDate { get; set; }
    public DateTime ExpiresDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public Guid DomainId { get; set; }
    public Domain Domain { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
}