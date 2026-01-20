namespace BookingProject.Models;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string? Password { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public DateTime IsCreate { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
    public string? Role { get; set; }
    public List<Booking> Bookings { get; set; } = new();
}