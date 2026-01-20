using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookingProject.Data;
using BookingProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using FluentValidation;

namespace BookingProject.Endpoint.Auth;

public static class Login
{
    public static void UserLogin(this WebApplication app)
    {
        app.MapPost("/api/login", async (AppDbContext db,
            IValidator<LoginRequest> validator , IConfiguration config , LoginRequest login ,
            ILogger<LoginLogger> logger , HttpContext httpContext) =>
        {
            try
            {
                logger.LogInformation("Login attempt {Email} -> {RemoteIp}" , login.Email,
                    httpContext.Connection.RemoteIpAddress);
                
                var validatorResult = await validator.ValidateAsync(login);
                if (!validatorResult.IsValid)
                {
                    logger.LogWarning("Invalid login data {Email} -> {RemoteIp}" ,
                        login.Email , httpContext.Connection.RemoteIpAddress);
                    return Results.BadRequest(validatorResult.Errors);
                }
                
                var user = await db.Users.FirstOrDefaultAsync(a => a.Email == login.Email);
                if (user is null || !BCrypt.Net.BCrypt.Verify(login.Password, user.Password))
                {
                    logger.LogWarning("Incorrect data or user {Email} does not exist -> {RemoteIp}" ,
                        login.Email , httpContext.Connection.RemoteIpAddress);
                    return Results.Unauthorized();
                }

                var jwtSettings = config.GetSection("Jwt");
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
                
                logger.LogInformation("Create JWT: {Email} -> {RemoteIp}" ,
                    login.Email , httpContext.Connection.RemoteIpAddress);
                
                var tokenString = tokenHandler.WriteToken(token);
                
                logger.LogInformation("Successful login {Email} -> {RemoteIp}" ,
                    login.Email , httpContext.Connection.RemoteIpAddress);
                
                return Results.Ok(new { Token = tokenString });
            }
            catch (Exception)
            {
                logger.LogError("Failed login {Email} -> {RemoteIp}" ,
                    login.Email , httpContext.Connection.RemoteIpAddress);
                return Results.Problem();
            }
        });
    }
}
public class LoginLogger { }