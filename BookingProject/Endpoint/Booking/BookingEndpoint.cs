using BookingProject.Data;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
namespace BookingProject.Endpoint.Booking;

public static class Endpoint
{
    public static void BookingEndpoint(this WebApplication app)
    {
        app.MapPost("/api/booking", async (AppDbContext db,
            IValidator<Models.Booking> validator, Models.Booking booking ,
            ILogger<BookingLogger> logger , HttpContext httpContext) =>
        {
            try
            {
                logger.LogInformation("Reservation request {UserId} -> {RemoteIp}" ,
                    booking.UserId , httpContext.Connection.RemoteIpAddress);
                
                var validatorResult = await validator.ValidateAsync(booking);
                if (!validatorResult.IsValid)
                {
                    logger.LogWarning("Invalid booking data {UserId} -> {RemoteIp}" ,
                        booking.UserId , httpContext.Connection.RemoteIpAddress);
                    return Results.BadRequest(validatorResult.Errors);
                }
                
                var freedomRoom = await db.Bookings
                    .AnyAsync(r => r.RoomId == booking.RoomId && 
                                   r.CheckInDate < booking.CheckOutDate && r.CheckOutDate > booking.CheckInDate);
                if (freedomRoom)
                {
                    logger.LogWarning("The room is occupied for this period -> {RemoteIp}" ,
                        httpContext.Connection.RemoteIpAddress);
                    return Results.BadRequest("Room is busy");
                }

                db.Bookings.Add(booking);
                await db.SaveChangesAsync();
                
                logger.LogInformation("Room {RoomId} has been successfully booked {UserId} -> {RemoteIp}" ,
                    booking.RoomId , booking.UserId , httpContext.Connection.RemoteIpAddress);
                
                return Results.Created($"/api/booking/{booking.Id}", booking);
            }
            catch (Exception)
            {
                logger.LogError("Failed booking room {RoomId} -> {RemoteIp}" ,
                    booking.RoomId , httpContext.Connection.RemoteIpAddress);
                return Results.Problem();
            }
        }).RequireAuthorization(policy => policy.RequireRole("User"));
        
        app.MapDelete("/api/booking/{id}", async (AppDbContext db, Guid id ,
            ILogger<BookingLogger> logger , HttpContext httpContext) =>
        {
            try
            {
                logger.LogInformation("Cancellation of reservation {Id} -> {RemoteIp}" ,
                   id  , httpContext.Connection.RemoteIpAddress);
                
                var bookingReal = await db.Bookings.FindAsync(id);
                if (bookingReal is null)
                {
                    logger.LogWarning("Such a reservation does not exist -> {RemoteIp}" ,
                        httpContext.Connection.RemoteIpAddress);
                    return Results.NotFound();
                }

                db.Bookings.Remove(bookingReal);
                await db.SaveChangesAsync();
                
                logger.LogInformation("Room reservation {Id} has been successfully cancelled -> {RemoveIp}" ,
                    id , httpContext.Connection.RemoteIpAddress );
                
                return Results.Ok();
            }
            catch (Exception)
            {
                logger.LogError("Failed delete booking room {Id} -> {RemoteIp}" ,
                    id , httpContext.Connection.RemoteIpAddress);
                return Results.Problem();
            }
        }).RequireAuthorization(policy => policy.RequireRole("User"));
    }
}
public class BookingLogger { }