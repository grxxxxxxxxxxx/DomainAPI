using BookingProject.Services;
using BookingProject.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace BookingProject.Endpoint.Auth;

public static class Login
{
    public static void UserLogin(this WebApplication app)
    {
        app.MapPost("/api/login", async ([FromServices] ILoginService loginService ,
            [FromBody] LoginDto dto) =>
        {
            try
            {
                var result = await loginService.LoginAsync(dto);

                return result.IsSuccess
                    ? Results.Ok(result.Data)
                    : Results.Unauthorized();
            }
            catch (Exception)
            {
                return Results.Problem();
            }
        });
    }
}