using BookingProject.Data;
using BookingProject.DTOs;
using FluentValidation;
using BookingProject.Common;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BookingProject.Services;

public interface ILoginService
{
    Task<Result<LoginResponseDto>> LoginAsync(LoginDto dto);
}

public class LoginService : ILoginService
{
    private readonly AppDbContext _db;
    private readonly ILogger<LoginService> _logger;
    private readonly IValidator<LoginDto> _validator;
    private readonly IConfiguration _config;

    public LoginService(AppDbContext db, ILogger<LoginService> logger,
        IValidator<LoginDto> validator , IConfiguration config)
    {
        _db = db;
        _logger = logger;
        _validator = validator;
        _config = config;
    }

    public async Task<Result<LoginResponseDto>> LoginAsync(LoginDto dto)
    {
        try
        {
            _logger.LogInformation("Login attempt {Email}" , dto.Email);
            var validatorResult = await _validator.ValidateAsync(dto);
            if (!validatorResult.IsValid)
            {
                _logger.LogWarning("Incorrect mail format: {Email}" , dto.Email);
                return Result<LoginResponseDto>.Failure("Incorrect mail format");
            }
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user is null)
            {
                _logger.LogWarning("User not found {Email}" , dto.Email);
                return Result<LoginResponseDto>.Failure("User not found");
            }
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                _logger.LogWarning("Invalid password {Email}" , dto.Email);
                return Result<LoginResponseDto>.Failure("Invalid password");
            }
            _logger.LogInformation("Create JWT {Email}" , dto.Email);
        
        var jwtSettings = _config.GetSection("Jwt");
        var tokenHandler = new JwtSecurityTokenHandler();
        var keyBytes = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Role , user.Role!),
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                new Claim(ClaimTypes.Name , user.Name!),
                new Claim(ClaimTypes.Email , user.Email!),
                new Claim(ClaimTypes.MobilePhone , user.Phone!)
            }),
            Expires = DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwtSettings["ExpiresMinutes"])),
            Issuer = jwtSettings["Issuer"],
            Audience = jwtSettings["Audience"],
            SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(keyBytes) , SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);
        var response = new LoginResponseDto
        {
            Token = tokenString
        };
        _logger.LogInformation("Successful login {Email}" , dto.Email);
        return Result<LoginResponseDto>.Success(response);
        }
        catch (Exception)
        {
            _logger.LogError("Failed login {Email}" , dto.Email);
            return Result<LoginResponseDto>.Failure("Failed login");
        }
    }
}