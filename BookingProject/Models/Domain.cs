namespace BookingProject.Models;

public class Domain
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; }
    public string? TDL { get; set; }
    public decimal Price { get; set; }
    public DateTime IsCreated { get; set; } = DateTime.UtcNow;

    public List<Order> Orders { get; set; } = new();
}