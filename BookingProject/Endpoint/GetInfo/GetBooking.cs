using System.Data.Entity;
using BookingProject.Data;

namespace BookingProject.Endpoint.GetInfo;

public static class GetBooking
{
    public static void BookingGet(this WebApplication app)
    {
        app.MapGet("/api/booking", async (AppDbContext db ,
            ILogger<GetBookingLogger> logger , HttpContext httpContext) =>
        {
            try
            {
                logger.LogInformation("Get list of reservations -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                    
                var booking = await db.Bookings.ToListAsync();
                logger.LogInformation("Successfully received the list of reservations -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                return Results.Ok(booking);
            }
            catch (Exception)
            {
                logger.LogError("Failed to retrieve the list of reservations -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                return Results.Problem();
            }
        }).RequireAuthorization(policy => policy.RequireRole("Admin"));
        
        app.MapGet("/api/booking/{id}", async (AppDbContext db, Guid id ,
            ILogger<GetBookingLogger> logger , HttpContext httpContext) =>
        {
            try
            {
                logger.LogInformation("Get a list of reservations by ID -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                
                var bookingReal = await db.Bookings.FindAsync(id);
                if (bookingReal is null) return Results.NotFound();

                logger.LogInformation("Successfully received a list of reservations by ID -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                return Results.Ok(bookingReal);
            }
            catch (Exception)
            {
                logger.LogError("Failed to get list of reservations by ID -> {RemoteIp}" ,
                    httpContext.Connection.RemoteIpAddress);
                return Results.Problem();
            }

        }).RequireAuthorization(policy => policy.RequireRole("Admin"));
    }
}
public class GetBookingLogger { }