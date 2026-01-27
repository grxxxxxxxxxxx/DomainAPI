using BookingProject.Models;
namespace BookingProject.DTOs;

public class DomainResponseDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string? TDL { get; set; } = string.Empty;
    public string? Name { get; set; } = string.Empty;

    public decimal Price { get; set; }
    public DateTime IsCreated { get; set; } = DateTime.UtcNow;
    public List<Order> Orders { get; set; } = new();
    public List<DomainDto> Domains { get; set; } = new();
}