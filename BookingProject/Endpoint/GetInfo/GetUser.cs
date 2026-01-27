using BookingProject.Common;
using BookingProject.Data;
using Microsoft.EntityFrameworkCore;
using BookingProject.Models;
using BookingProject.DTOs;
using BookingProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingProject.Endpoint.GetInfo;

public static class Info
{
    public static void GetUser(this WebApplication app)
    {
        app.MapGet("/api/users", async ([FromServices] IUserService userService) =>
        {
            try
            {
                var result = await userService.GetUserAsync();

                return result.IsSuccess
                    ? Results.Ok(result.Data)
                    : Results.BadRequest(new { error = result.Error });
            }
            catch (Exception)
            {
                return Results.Problem();
            }
        }).RequireAuthorization(policy => policy.RequireRole("Admin"));
        
        app.MapGet("/api/users/{id}", async ([FromServices] IUserService userService, 
            Guid id) =>
        {
            try
            {
                var result = await userService.GetUserIdAsync(id);

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