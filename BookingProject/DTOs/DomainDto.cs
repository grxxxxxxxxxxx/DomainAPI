using BookingProject.Models;
namespace BookingProject.DTOs;

public class DomainDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? Name { get; set; } = string.Empty;
    public string? TDL { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public DateTime IsCreated { get; set; } = DateTime.UtcNow;
    public List<Order> Orders { get; set; } = new();
}