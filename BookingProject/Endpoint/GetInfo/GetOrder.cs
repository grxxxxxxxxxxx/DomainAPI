using System.Data.Entity;
using BookingProject.Common;
using BookingProject.Data;
using BookingProject.DTOs;
using BookingProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingProject.Endpoint.GetInfo;

public static class GetOrder
{
    public static void OrderGet(this WebApplication app)
    {
        app.MapGet("/api/orders", async ([FromServices] IOrderService orderService) =>
        {
            try
            {
                var result = await orderService.GetOrderAsync();
                return result.IsSuccess
                    ? Results.Ok(result.Data)
                    : Results.BadRequest(new { error = result.Error });
            }
            catch (Exception)
            {
                return Results.Problem();
            }
        }).RequireAuthorization(policy => policy.RequireRole("Admin"));
        
        app.MapGet("/api/orders/{id}", async ([FromServices] IOrderService orderService , Guid id) =>
        {
            try
            {
                var result = await orderService.GetOrderIdAsync(id);

                return result.IsSuccess
                    ? Results.Ok(result.Data)
                    : Results.BadRequest(new { error = result.Error });
            }
            catch (Exception)
            {
                return Results.Problem();
            }

        }).RequireAuthorization(policy => policy.RequireRole("Admin"));
    }
}