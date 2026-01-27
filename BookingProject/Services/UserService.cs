using BookingProject.Data;
using BookingProject.DTOs;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using BookingProject.Common;
using BookingProject.Models;

namespace BookingProject.Services;

public interface IUserService
{
    Task<Result<UsersResponseDto>> RegisterAsync(UsersDto dto , string role);
    Task<Result<UsersResponseDto>> UpdateAsync(Guid userId , UsersDto dto);
    Task<Result> DeleteAsync(Guid userId);
    Task<Result<UsersResponseDto>> GetUserAsync();
    Task<Result<UsersDto>> GetUserIdAsync(Guid id);
}
public class UserService : IUserService
{
    private readonly AppDbContext _db;
    private readonly ILogger<UserService> _logger;
    private readonly IValidator<UsersDto> _validator;

    public UserService(AppDbContext db,
        ILogger<UserService> logger, IValidator<UsersDto> validator)
    {
        _db = db;
        _logger = logger;
        _validator = validator;
    }

    public async Task<Result<UsersResponseDto>> RegisterAsync(UsersDto dto , string role = "User")
    {
        try
        {
            _logger.LogInformation("User create process -> {Email}" , dto.Email);

            var validatorResult = await _validator.ValidateAsync(dto);
            if (!validatorResult.IsValid)
            {
                _logger.LogWarning("Invalid user data -> {Email}" , dto.Email);
                return Result<UsersResponseDto>.Failure("Failed validation user data");
            }
            var userNew = await _db.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (userNew != null)
            {
                _logger.LogWarning("User already exists -> {Email}" , dto.Email);
                return Result<UsersResponseDto>.Failure("User already exists");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                Email = dto.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Phone = dto.Phone,
                Role = role,
                IsCreate = DateTime.UtcNow,
                IsActive = true
            };

            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();
            _logger.LogInformation("Successful user creation -> {Email}" , dto.Email);
            
            return Result<UsersResponseDto>.Success(new UsersResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Role = user.Role,
                IsCreated = user.IsCreate,
                Users = new List<UsersDto>()
            });
        }
        catch (Exception)
        {
            _logger.LogError("Failed user registration -> {Email}" , dto.Email);
            return Result<UsersResponseDto>.Failure("Failed user create");
        }
    }
    public async Task<Result<UsersResponseDto>> UpdateAsync(Guid userId, UsersDto dto)
    {
        try
        {
            _logger.LogInformation("Update user data -> {Id}" , userId);

            var validatorResult = await _validator.ValidateAsync(dto);
            if (!validatorResult.IsValid)
            {
                _logger.LogWarning("Invalid data for update -> {Id}" , userId);
                return Result<UsersResponseDto>.Failure("Invalid data for update");
            }

            var user = await _db.Users.FindAsync(userId);
            if (user is null)
            {
                _logger.LogWarning("User not found -> {Id}" , userId);
                return Result<UsersResponseDto>.Failure("User not found");
            }

            var userUnique = await _db.Users
                .AnyAsync(u => u.Email == dto.Email && u.Id == userId);
            if (userUnique)
            {
                _logger.LogWarning("This user already exists -> {Id}" , userId);
                return Result<UsersResponseDto>.Failure("This user already exists!");
            }

            if (!string.IsNullOrWhiteSpace(dto.Password))
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
                _logger.LogInformation("Successful password change and hashing -> {Id}" , userId);
            }

            user.Email = dto.Email;
            user.Name = dto.Name;
            user.Phone = dto.Phone;

           await _db.SaveChangesAsync();

           return Result<UsersResponseDto>.Success(new UsersResponseDto
           {
               Id = user.Id,
               Name = user.Name,
               Email = user.Email,
               Phone = user.Phone
               
           });
        }
        catch (Exception)
        {
            _logger.LogError("Failed update data -> {Email}" , dto.Email);
            return Result<UsersResponseDto>.Failure("Failed update user data");
        }
    }
    public async Task<Result> DeleteAsync(Guid userId)
    {
        try
        {
            _logger.LogInformation("Delete user profile -> {Id}" , userId);
            
            var user = await _db.Users.FindAsync(userId);
            if (user is null)
            {
                _logger.LogWarning("User not found -> {Id}" , userId);
                return Result.Failure("User not found");
            }
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            
            _logger.LogInformation("Successfuly user deleted -> {Id}" , userId);
            return Result.Success();
        }
        catch (Exception)
        {
            _logger.LogError("Failed user delete -> {Id}" , userId);
            return Result.Failure("Failed user delete");
        }
    }

    public async Task<Result<UsersResponseDto>> GetUserAsync()
    {
        try
        {
            _logger.LogInformation("Get users");
            var users = await _db.Users.ToListAsync();
            
            var userDtos = users.Select(u => new UsersDto
            {
                Email = u.Email,
                Name = u.Name,
                Phone = u.Phone
                
            }).ToList();
            
            var response = new UsersResponseDto
            {
                Users = userDtos
            };
            
            _logger.LogInformation("Successfully retrieved {Count} users", userDtos.Count);
            return Result<UsersResponseDto>.Success(response);

        }
        catch (Exception)
        {
            _logger.LogError("Failed get users");
            return Result<UsersResponseDto>.Failure("Failed get users");
        }
    }

    public async Task<Result<UsersDto>> GetUserIdAsync(Guid id)
    {
        try
        {
            _logger.LogInformation("Getting users by Id -> {Id}" , id);
            var users = await _db.Users.FindAsync(id);
            if (users is null)
            {
                _logger.LogWarning("Users not found -> {Id}", id);
                return Result<UsersDto>.Failure("Users not found");
            }

            var userDto = new UsersDto
            {
                Email = users.Email,
                Name = users.Name,
                Phone = users.Phone
            };
            
            _logger.LogInformation("Successfully retrieved user by Id -> {Id}" , id);
            return Result<UsersDto>.Success(userDto);
        }
        catch (Exception)
        {
            _logger.LogError("Failed to get users by Id -> {Id}" , id);
            return Result<UsersDto>.Failure("Failed to get users by Id");
        }
    }
}