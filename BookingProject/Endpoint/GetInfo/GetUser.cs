using BookingProject.Data;
using Microsoft.EntityFrameworkCore;
using BookingProject.Models;

namespace BookingProject.Endpoint.GetInfo;

public static class Info
{
    public static void GetUser(this WebApplication app)
    {
        app.MapGet("/api/users", async (AppDbContext db , 
            ILogger<GetUserLogger> logger , HttpContext httpContext) =>
        {
            try
            {
                logger.LogInformation("Getting list of users -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                
                var userList = await db.Users.ToListAsync();
                
                logger.LogInformation("Successfully retrieved list of users -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                return Results.Ok(userList);
            }
            catch (Exception)
            {
                logger.LogError("Failed get user list -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                return Results.Problem();
            }
        }).RequireAuthorization(policy => policy.RequireRole("Admin"));
        
        app.MapGet("/api/users/{id}", async (AppDbContext db, Guid id,
            ILogger<GetUserLogger> logger , HttpContext httpContext ) =>
        {
            try
            {
                logger.LogInformation("Getting list of users by ID -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                
                var user = await db.Users.FindAsync(id);
                if (user is null)
                {
                    logger.LogWarning("User not found -> {RemoteIp}" ,
                        httpContext.Connection.RemoteIpAddress);
                    return Results.NotFound();
                }
                logger.LogInformation("Successfully retrieved user by ID -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                return Results.Ok(user);
            }
            catch (Exception)
            {
                logger.LogError("Failed get user by ID -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                return Results.Problem();
            }
        }).RequireAuthorization(policy => policy.RequireRole("Admin"));
    }
}
public class GetUserLogger { }