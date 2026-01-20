using BookingProject.Data;
using BookingProject.Models;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace BookingProject.Endpoint.AuthAdmin;

public static class AuthAdmin
{
    public static void CreateAdmin(this WebApplication app)
    {
        app.MapPost("/api/register/admin"!, async (AppDbContext db,
            IValidator<User> validator, ILogger<AdminLog> logger , HttpContext httpContext , User user) =>
        {
            try
            {
                logger.LogInformation("Creating an Administrator -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                
                var validateResult = await validator.ValidateAsync(user);
                if (!validateResult.IsValid)
                {
                    logger.LogWarning("Invalid data for creation -> {RemoteIp}" ,
                        httpContext.Connection.RemoteIpAddress);
                    
                    return Results.BadRequest(validateResult.Errors);
                }
                
                var userNew = await db.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if (userNew != null)
                {
                    logger.LogWarning("Admin already exists -> {RemoteIp}" ,
                        httpContext.Connection.RemoteIpAddress);
                    
                    return Results.Problem(); //serilog
                }
                
                user.Role = "Admin";
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
                
                db.Users.Add(user);
                await db.SaveChangesAsync();
                
                logger.LogInformation("Admin created successfully -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                
                user.Password = null;
                return Results.Created($"/api/register/admin/{user.Id}", user);
            }
            catch (Exception)
            {
                logger.LogError("Failed to create admin -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                
                return Results.Problem();
            }
        }).RequireAuthorization(policy => policy.RequireRole("Admin") );

        //Create First Admin
        app.MapPost("/api/register/admin/first", async (AppDbContext db, IValidator<User> validator
            , ILogger<AdminLog> logger , HttpContext httpContext , User user) =>
        {
            try
            {
                logger.LogInformation("Creating the first admin -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                
                var validatorResult = await validator.ValidateAsync(user);
                if (!validatorResult.IsValid)
                {
                    logger.LogWarning("data has not been validated -> {RemoteIp}" ,
                        httpContext.Connection.RemoteIpAddress);
                    
                    return Results.BadRequest(validatorResult.Errors);
                }
                
                user.Role = "Admin";
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            
                db.Users.Add(user);
                await db.SaveChangesAsync();
                
                logger.LogInformation("Admin created successfully -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                
                user.Password = null; //security moment ;)
                return Results.Created($"/api/register/admin/{user.Id}", user);
            }
            catch (Exception)
            {
                logger.LogError("Failed to create admin -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                
                return Results.Problem();
            }
        });
    }
    public static void DeleteAdmin(this WebApplication app)
    {
        app.MapDelete("/api/users/admin/{id}", async (AppDbContext db, Guid id ,
            ILogger<AdminLog> logger , HttpContext httpContext) =>
        {
            try
            {
                logger.LogInformation("Removing an admin -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                
                var adminValid = await db.Users.FindAsync(id);
                if (adminValid is null)
                {
                    logger.LogWarning("Admin not found -> {RemoteIp}" ,
                        httpContext.Connection.RemoteIpAddress);
                    return Results.NotFound();
                }

                db.Users.Remove(adminValid);
                await db.SaveChangesAsync();
                
                logger.LogInformation("Admin successfully removed -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                
                return Results.Ok();
            }
            catch (Exception)
            {
                logger.LogError("Failed deleting admin -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                return Results.Problem();
            }
        }).RequireAuthorization(policy => policy.RequireRole("Admin"));
    }
    
    public static void PutAdmin(this WebApplication app)
    {
        app.MapPut("/api/users/admin/{id}", async (AppDbContext db, IValidator<User> validator,
            User update, Guid id , ILogger<AdminLog> logger , HttpContext httpContext) =>
        {
            try
            {
                logger.LogInformation("Updating admin data -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                
                var validatorResults = await validator.ValidateAsync(update);
                if (!validatorResults.IsValid)
                {
                    logger.LogWarning("Invalid data for update -> {RemoteIp}" ,
                        httpContext.Connection.RemoteIpAddress);
                    return Results.BadRequest(validatorResults.Errors);
                }
                
                var user = await db.Users.FindAsync(id);
                if (user is null)
                {
                    logger.LogWarning("Admin not found -> {RemoteIp}" ,
                        httpContext.Connection.RemoteIpAddress);
                    return Results.NotFound();
                }

                user!.Email = update.Email;
                user!.Name = update.Name;
                
                update.Password = BCrypt.Net.BCrypt.HashPassword(update.Password);
                
                user!.Password = update.Password;
                user!.Phone = update.Phone;

                await db.SaveChangesAsync();
                
                logger.LogInformation("Admin data updated successfully -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                
                return Results.NoContent();
            }
            catch (Exception)
            {
                logger.LogError("Failed updating data admin -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                return Results.Problem();
            }
        }).RequireAuthorization(policy => policy.RequireRole("Admin"));
    }
}
public class AdminLog { }