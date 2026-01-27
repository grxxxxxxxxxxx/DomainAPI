using BookingProject.DTOs;
using BookingProject.Services;
using Microsoft.AspNetCore.Mvc;
namespace BookingProject.Endpoint.Booking;

public static class Endpoint
{
    public static void OrderEndpoint(this WebApplication app)
    {
        app.MapPost("/api/orders", async ([FromServices] IOrderService orderService ,
            [FromBody] OrderDto dto) =>
        {
            try
            {
                var result = await orderService.CreateOrderAsync(dto);

                return result.IsSuccess
                    ? Results.Created($"/api/orders/{result.Data!.Id}" , result.Data)
                    : Results.BadRequest(new { error = result.Error });
            }
            catch (Exception)
            {
                return Results.Problem();
            }
        }).RequireAuthorization(policy => policy.RequireRole("User"));
        
        app.MapDelete("/api/orders/{id}", async ([FromServices] IOrderService orderService,
            Guid id) =>
        {
            try
            {
                var result = await orderService.DeleteOrderAsync(id);

                return result.IsSuccess
                    ? Results.Ok()
                    : Results.BadRequest(new { error = result.Error });

            }
            catch (Exception)
            {
                return Results.Problem();
            }
        }).RequireAuthorization(policy => policy.RequireRole("User"));
    }
}