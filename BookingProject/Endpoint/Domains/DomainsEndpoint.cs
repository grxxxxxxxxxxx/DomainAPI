using BookingProject.Common;
using BookingProject.Data;
using BookingProject.Models;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using BookingProject.DTOs;
using BookingProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookingProject.Endpoint.Rooms;

public static class Domains
{
    public static void DomainEndpoint(this WebApplication app)
    {
        app.MapPost("/api/domains", async ([FromServices] IDomainService domainService ,
            [FromBody] DomainDto dto) =>
        {
            try
            {
                var result = await domainService.CreateDomainAsync(dto);

                return result.IsSuccess
                    ? Results.Created($"/api/domains/{result.Data!.Id}" , result.Data)
                    : Results.BadRequest(new { error = result.Error });
            }
            catch (Exception)
            {
                return Results.Problem();
            }
        }).RequireAuthorization(policy => policy.RequireRole("Admin"));
        
        app.MapPut("/api/domains/{id}", async ([FromServices] IDomainService domainService ,
            Guid id ,[FromBody] DomainDto dto) =>
        {
            try
            {
                var result = await domainService.UpdateDomainAsync(id, dto);

                return result.IsSuccess
                    ? Results.Ok(result.Data)
                    : Results.BadRequest(new { error = result.Error });
            }
            catch (Exception)
            {
                return Results.Problem();
            }
        }).RequireAuthorization(policy => policy.RequireRole("Admin"));
        
        app.MapDelete("/api/domains/{id}", async ([FromServices] IDomainService domainService ,
            Guid id) =>
        {
            try
            {
                var result = await domainService.DeleteDomainAsync(id);

                return result.IsSuccess
                    ? Results.Ok()
                    : Results.BadRequest(new { error = result.Error });
            }
            catch (Exception)
            {
                return Results.Problem();
            }
        }).RequireAuthorization(policy => policy.RequireRole("Admin"));
    }
}