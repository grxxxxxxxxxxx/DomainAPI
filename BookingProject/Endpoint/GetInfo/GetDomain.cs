using BookingProject.Common;
using BookingProject.DTOs;
using BookingProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingProject.Endpoint.GetInfo;

public static class Get
{
    public static void GetDomain(this WebApplication app)
    {
        app.MapGet("/api/domains", async ([FromServices] IDomainService domainService) =>
        {
            try
            {
                var result = await domainService.GetDomainAsync();

                return result.IsSuccess
                    ? Results.Ok(result.Data)
                    : Results.BadRequest(new { error = result.Error });
            }
            catch (Exception)
            {
                return Results.Problem();
            }
        });
        app.MapGet("/api/domains/{id}", async ([FromServices] IDomainService domainService , Guid id) =>
        {
            try
            {
                var result = await domainService.GetDomainIdAsync(id);

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