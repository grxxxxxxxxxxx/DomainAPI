using BookingProject.Data;
using BookingProject.Models;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using Serilog;

namespace BookingProject.Endpoint.Auth;

public static class AuthUser
{
    public static void CreateUser(this WebApplication app)
    {
        app.MapPost("/api/register", async (User user, IValidator<User> validator ,
            ILogger<UserLogger> logger , AppDbContext db , HttpContext httpContext) =>
        {
            try
            {
                logger.LogInformation("Creating a user -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                
                var validatorResult = await validator.ValidateAsync(user);
                if (!validatorResult.IsValid)
                {
                    logger.LogWarning("Invalid user data -> {RemoteIp}" ,
                        httpContext.Connection.RemoteIpAddress);
                    
                    return Results.BadRequest(validatorResult.Errors);
                }
                
                var userNew = await db.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (userNew != null)
                {
                    logger.LogWarning("User already exists -> {RemoteIp}" ,
                        httpContext.Connection.RemoteIpAddress);
                    
                    return Results.Problem();
                }

                user.Role = "User";
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                db.Users.Add(user);
                await db.SaveChangesAsync();
                
                logger.LogInformation("User successfully created -> {RemoteIp}" , 
                    httpContext.Connection.RemoteIpAddress);
                
                user.Password = null; //security moment :)
                return Results.Created($"/api/users/{user.Id}", user);
            }
            catch (Exception)
            {
                logger.LogError("Failed user creating -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                return Results.Problem();
            }
        });
    }
    public static void UpdateUser(this WebApplication app)
    {
        app.MapPut("/api/users/{id}", async (AppDbContext db,
            IValidator<User> validator , Guid id, User update , ILogger<UserLogger> logger , HttpContext httpContext) =>
        {
            try
            {
                logger.LogInformation("Updating User Data -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                
                var validatorResult = await validator.ValidateAsync(update);
                if (!validatorResult.IsValid)
                { 
                    logger.LogWarning("Invalid data for update -> {RemoteIp}" ,
                        httpContext.Connection.RemoteIpAddress);
                    
                    return Results.BadRequest(validatorResult.Errors);
                }
                
                var user = await db.Users.FindAsync(id);
                if (user is null)
                {
                    logger.LogWarning("User not found -> {RemoteIp}" ,
                        httpContext.Connection.RemoteIpAddress);
                    
                    return Results.NotFound();
                }
                
                update.Password = BCrypt.Net.BCrypt.HashPassword(update.Password);
                
                user.Password = update.Password;
                user.Email = update.Email;
                user.Name = update.Name;
                user.Phone = update.Phone;

                await db.SaveChangesAsync();
                
                logger.LogInformation("Data updated successfully -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                
                return Results.NoContent();
            }
            catch (Exception)
            {
                logger.LogError("Failed update user data -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                return Results.Problem();
            }
        });
    }
    public static void DeleteUser(this WebApplication app)
    {
        app.MapDelete("/api/users/{id}", async (Guid id, AppDbContext db ,
            ILogger<UserLogger> logger , HttpContext httpContext) =>
        {
            try
            {
                logger.LogInformation("Deleting a user -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                
                var user = await db.Users.FindAsync(id);
                if (user is null)
                {
                    logger.LogWarning("User for deletion not found -> {RemoteIp}" ,
                        httpContext.Connection.RemoteIpAddress);
                    return Results.NotFound();
                }

                db.Users.Remove(user);
                await db.SaveChangesAsync();
                
                logger.LogInformation("User deleted successfully -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                
                return Results.NoContent();
            }
            catch (Exception)
            {
                logger.LogError("Failed user delete -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                return Results.Problem();
            }
        });
    }
}
public class UserLogger { }