using BookingProject.Data;
using Microsoft.EntityFrameworkCore;

namespace BookingProject.Endpoint.GetInfo;

public static class Get
{
    public static void GetRoom(this WebApplication app)
    {
        app.MapGet("/api/rooms", async (AppDbContext db ,
            ILogger<GetRoomLogger> logger , HttpContext httpContext) =>
        {
            try
            {
                logger.LogInformation("Get list of rooms -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                
                var roomList = await db.Rooms.ToListAsync();
                logger.LogInformation("Room list successfully retrieved -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                return Results.Ok(roomList);
            }
            catch (Exception)
            {
                logger.LogError("Failed to get room list -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                return Results.Problem();
            }
        });
        app.MapGet("/api/rooms/{id}", async (Guid id, AppDbContext db ,
            ILogger<GetRoomLogger> logger , HttpContext httpContext) =>
        {
            try
            {
                logger.LogInformation("Get list of rooms by ID -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
            
                var room = await db.Rooms.FindAsync(id);
                if (room is null)
                {
                    logger.LogWarning("Room by ID not found -> {RemoteIp}" ,
                        httpContext.Connection.RemoteIpAddress);
                    return Results.NotFound();
                }
                logger.LogInformation("Successfully received list of rooms by ID -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                return Results.Ok(room);
            }
            catch (Exception)
            {
                logger.LogError("Failed to get list of rooms by Id -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                return Results.Problem();
            }
        }).RequireAuthorization(policy => policy.RequireRole("Admin"));
    }
}
public class GetRoomLogger { }