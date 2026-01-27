namespace BookingProject.DTOs;

public class LoginResponseDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; } = Guid.NewGuid();
    public string? Role { get; set; } = string.Empty;
    public string? Token { get; set; } = string.Empty;
    public string? Email { get; set; } = string.Empty;
    public string? Name { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public DateTime IsEnter { get; set; } = DateTime.UtcNow;

}