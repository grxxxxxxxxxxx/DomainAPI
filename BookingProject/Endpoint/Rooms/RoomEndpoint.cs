using BookingProject.Data;
using BookingProject.Models;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

namespace BookingProject.Endpoint.Rooms;

public static class Rooms
{
    public static void RoomEndpoint(this WebApplication app)
    {
        app.MapPost("/api/rooms", async (AppDbContext db,
            IValidator<Room> validator , Room room , ILogger<RoomsLogger> logger , HttpContext httpContext) =>
        {
            try
            {
                logger.LogInformation("Creating new room: {RoomName} -> {RemoteIp}" ,
                    room.Name , httpContext.Connection.RemoteIpAddress);
                
                var validatorResult = await validator.ValidateAsync(room);
                if (!validatorResult.IsValid)
                {
                    logger.LogWarning("Invalid data for creating room -> {RemoteIp}" ,
                        httpContext.Connection.RemoteIpAddress);
                    return Results.BadRequest(validatorResult.Errors);
                }
            
                db.Rooms.Add(room);
                await db.SaveChangesAsync();
                
                logger.LogInformation("Successful creation of room: {RoomName} -> {RemoteIp}" ,
                    room.Name , httpContext.Connection.RemoteIpAddress);
                
                return Results.Created($"/api/rooms/{room.Id}", room);
            }
            catch (Exception)
            {
                logger.LogError("Failed to create room: {RoomId} -> {RemoteIp}" ,
                    room.Id , httpContext.Connection.RemoteIpAddress);
                return Results.Problem();
            }
        }).RequireAuthorization(policy => policy.RequireRole("Admin"));
        
        app.MapPut("/api/rooms/{id}", async (AppDbContext db,
            IValidator<Room> validator , Room update, Guid id ,
            ILogger<RoomsLogger> logger , HttpContext httpContext) =>
        {
            try
            {
                logger.LogInformation("Room update {RoomId} -> {RemoteIp}" ,
                    id , httpContext.Connection.RemoteIpAddress);
                
                var validatorResult = await validator.ValidateAsync(update);
                if (!validatorResult.IsValid)
                {
                    logger.LogWarning("Invalid room data -> {RemoteIp}" ,
                        httpContext.Connection.RemoteIpAddress);
                    return Results.BadRequest(validatorResult.Errors);
                }
                
                var room = await db.Rooms.FindAsync(id);
                if (room is null)
                {
                    logger.LogWarning("Room for update not found -> {RemoteIp}" ,
                        httpContext.Connection.RemoteIpAddress);
                    return Results.NotFound();
                }

                room.Name = update.Name;
                room.Price = update.Price;
                room.Description = update.Description;
                room.Class = update.Class;

                await db.SaveChangesAsync();
                logger.LogInformation("Room {RoomId} data updated successfully -> {RemoteIp}" ,
                    id , httpContext.Connection.RemoteIpAddress);
                return Results.Ok();
            }
            catch (Exception)
            {
                logger.LogError("Failed to update room data -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                return Results.Problem();
            }

        }).RequireAuthorization(policy => policy.RequireRole("Admin"));
        app.MapDelete("/api/rooms/{id}", async (AppDbContext db, Guid id ,
            ILogger<RoomsLogger> logger , HttpContext httpContext) =>
        {
            try
            {
                logger.LogInformation("Deleting room {RoomId} -> {RemoteIp}" ,
                    id , httpContext.Connection.RemoteIpAddress);
                
                var room = await db.Rooms.FindAsync(id);
                if (room is null)
                {
                    logger.LogWarning("Room for removal not found -> {RemoteIp}" ,
                        httpContext.Connection.RemoteIpAddress);
                    return Results.NotFound();
                }
            
                var freedomRoom = await db.Bookings.AnyAsync(r => r.RoomId == id);
                if (freedomRoom)
                {
                    logger.LogWarning("You can't delete an occupied room -> {RemoteIp}" ,
                        httpContext.Connection.RemoteIpAddress);
                    return Results.BadRequest("Room is booked");
                }
            
                db.Rooms.Remove(room);
                await db.SaveChangesAsync();
                logger.LogInformation("Successfully deleted room {RoomId} -> {RemoteIp}" ,
                    id , httpContext.Connection.RemoteIpAddress);
                return Results.NoContent();
            }
            catch (Exception)
            {
                logger.LogError("Failed to delete room -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                return Results.Problem();
            }
            
        }).RequireAuthorization(policy => policy.RequireRole("Admin"));
    }
}
public class RoomsLogger { }