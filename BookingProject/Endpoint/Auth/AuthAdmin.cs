using BookingProject.Services;
using BookingProject.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookingProject.Endpoint.Auth;

public static class AuthAdmin
{
    public static void MapAuthAdminEndpoint(this WebApplication app)
    {
        app.MapPost("/api/register/admin"!, async ([FromBody] UsersDto dto ,
            [FromServices] IUserService registerService) =>
        {
            try
            {
                var result = await registerService.RegisterAsync(dto , "Admin");

                return result.IsSuccess
                    ? Results.Created($"/api/users/{result.Data!.Id}", result.Data)
                    : Results.BadRequest(new { error = result.Error });
            }
            catch (Exception)
            {
                return Results.Problem();
            }
        }).RequireAuthorization(policy => policy.RequireRole("Admin") );
        
        //Create First Admin
        app.MapPost("/api/register/admin/first", async ([FromBody] UsersDto dto ,
            [FromServices] IUserService regitserService) =>
        {
            try
            {
                var result = await regitserService.RegisterAsync(dto, "Admin");

                return result.IsSuccess
                    ? Results.Created($"/api/users/admin/first{result.Data!.Id}", result.Data)
                    : Results.BadRequest(new { error = result.Error });
            }
            catch (Exception)
            {
                return Results.Problem();
            }
        });
        
        app.MapDelete("/api/users/admin/{id}", async (Guid id ,
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
        }).RequireAuthorization(policy => policy.RequireRole("Admin"));
    
        app.MapPut("/api/users/admin/{id}", async (Guid id ,
            [FromBody] UsersDto dto ,[FromServices] IUserService userService) =>
        {
            try
            {
                var result = await userService.UpdateAsync(id, dto);

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