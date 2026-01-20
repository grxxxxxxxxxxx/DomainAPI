namespace BookingProject.Models;

public class Room
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public string? Class { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public DateTime IsCreated { get; set; } = DateTime.UtcNow;

    public List<Booking> Bookings { get; set; } = new();
}