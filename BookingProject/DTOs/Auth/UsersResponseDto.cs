namespace BookingProject.DTOs;

public class UsersResponseDto
{
    public Guid Id { get; set; }
    public string? Email { get; set; } = string.Empty;
    public string? Name { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;
    public string? Role { get; set; } = string.Empty;
    public DateTime IsCreated { get; set; }


    public List<UsersDto> Users { get; set; }
}