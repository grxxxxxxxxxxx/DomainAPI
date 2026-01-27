using BookingProject.DTOs;
using BookingProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingProject.Endpoint.Auth;

public static class AuthUser
{
    public static void MapAuthUserEndpoint(this WebApplication app)
    {
        app.MapPost("/api/register", async ([FromBody] UsersDto dto ,
            [FromServices] IUserService userService) =>
        {
            try
            {
                var result = await userService.RegisterAsync(dto, "User");

                return result.IsSuccess
                    ? Results.Created($"/api/users/{result.Data!.Id}", result.Data)
                    : Results.BadRequest();
            }
            catch (Exception)
            {
                return Results.Problem();
            }
        });
    
        app.MapPut("/api/users/{id}", async (Guid id ,
            [FromBody] UsersDto dto ,[FromServices] IUserService userService) =>
        {
            try
            {
                var result = await userService.UpdateAsync(id, dto);
                return result.IsSuccess
                    ? Results.Ok(result.Data)
                    : Results.BadRequest();
            }
            catch (Exception)
            {
                return Results.Problem();
            }
        }).RequireAuthorization(policy => policy.RequireRole("User"));
    
        app.MapDelete("/api/users/{id}", async (Guid id ,
            [FromServices] IUserService userService) =>
        {
            try
            {
                var result = await userService.DeleteAsync(id);

                return result.IsSuccess
                    ? Results.Ok()
                    : Results.BadRequest();
            }
            catch (Exception)
            {
                return Results.Problem();
            }
        }).RequireAuthorization(policy => policy.RequireRole("User"));
    }
}