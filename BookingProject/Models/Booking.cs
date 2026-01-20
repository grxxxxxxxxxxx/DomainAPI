namespace BookingProject.Models;

public class Booking
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CheckInDate { get; set; }
    public DateTime CheckOutDate { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public Guid RoomId { get; set; }
    public Room Room { get; set; }
    
    public Guid UserId { get; set; }
    public User User { get; set; }
}