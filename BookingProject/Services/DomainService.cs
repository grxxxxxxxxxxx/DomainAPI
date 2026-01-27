using BookingProject.DTOs;
using BookingProject.Common;
using BookingProject.Data;
using BookingProject.Models;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BookingProject.Services;

public interface IDomainService
{
    Task<Result<DomainResponseDto>> CreateDomainAsync(DomainDto dto);
    Task<Result<DomainResponseDto>> UpdateDomainAsync(Guid id, DomainDto dto);
    Task<Result> DeleteDomainAsync(Guid id);
    
    Task<Result<DomainResponseDto>> GetDomainAsync();
    Task<Result<DomainResponseDto>> GetDomainIdAsync(Guid id);
}

public class DomainService : IDomainService
{
    private readonly AppDbContext _db;
    private readonly ILogger<DomainService> _logger;
    private readonly IValidator<DomainDto> _validator;

    public DomainService(AppDbContext db,
        ILogger<DomainService> logger, IValidator<DomainDto> validator)
    {
        _db = db;
        _logger = logger;
        _validator = validator;

    }

    public async Task<Result<DomainResponseDto>> CreateDomainAsync(DomainDto dto)
    {
        try
        {
            _logger.LogInformation("Creating domain {Id}" , dto.Id);
            var validatorResult = await _validator.ValidateAsync(dto);
            if (!validatorResult.IsValid)
            {
                _logger.LogWarning("Invalid domain data {Id}" , dto.Id);
                return Result<DomainResponseDto>.Failure("Invalid domain data");
            }

            var domain = new Domain
            {
                Id = Guid.NewGuid(),
                Name = dto.Name,
                TDL = dto.TDL,
                Price = dto.Price,
                Orders = dto.Orders
            };
            await _db.Domains.AddAsync(domain);
            await _db.SaveChangesAsync();
            
            _logger.LogInformation("Successful domain creation {Name}", dto.Name);
            return Result<DomainResponseDto>.Success(new DomainResponseDto
            {
                Id = domain.Id,
                Name = domain.Name,
                TDL = domain.TDL,
                Price = domain.Price,
                IsCreated = domain.IsCreated,
                Orders = domain.Orders
            });
        }
        catch (Exception)
        {
            _logger.LogError("Failed domain created {Id}" , dto.Id);
            return Result<DomainResponseDto>.Failure("Failed domain created");
        }
    }

    public async Task<Result<DomainResponseDto>> UpdateDomainAsync(Guid id, DomainDto dto)
    {
        try
        {
            _logger.LogInformation("Updating domain information {Id}" , dto.Id);
            var validatorResult = await _validator.ValidateAsync(dto);
            if (!validatorResult.IsValid)
            {
                _logger.LogWarning("Invalid data domain {Id}" , dto.Id);
                return Result<DomainResponseDto>.Failure("Invalid data domain");
            }
            var domain = await _db.Domains.FindAsync(id);
            if (domain is null)
            {
                _logger.LogWarning("Domain not found {Id}", dto.Id);
                return Result<DomainResponseDto>.Failure("Domain not found");
            }

            domain.Name = dto.Name;
            domain.TDL = dto.TDL;
            domain.Price = dto.Price;

            await _db.SaveChangesAsync();
            _logger.LogInformation("Domain information updated successfully {Id}", dto.Id);
            return Result<DomainResponseDto>.Success(new DomainResponseDto
            { 
                Id = domain.Id,
                Name = domain.Name,
                TDL = domain.TDL,
                Price = domain.Price,
                IsCreated = domain.IsCreated
            });
        }
        catch (Exception)
        {
            _logger.LogError("Failed update data domain {Id}", dto.Id);
            return Result<DomainResponseDto>.Failure("Failed update data domain");
        }
    }

    public async Task<Result> DeleteDomainAsync(Guid id)
    {
        try
        {
            _logger.LogInformation("Deleting domain {DomainId}", id);
            var domain = await _db.Domains.FindAsync(id);
            if (domain is null)
            {
                _logger.LogWarning("Domain not found {DomainId}", id);
                return Result.Failure("Domain not found");
            }

            _db.Domains.Remove(domain);
            await _db.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            _logger.LogError("Failed domain deleted {DomainId}", id);
            return Result.Failure("Failed domain deleted");
        }
    }

    public async Task<Result<DomainResponseDto>> GetDomainAsync()
    {
        try
        {
            _logger.LogInformation("Getting list of domains");
            var domains = await _db.Domains.ToListAsync();

            var domainDto = domains.Select(r => new DomainDto
            {
                Name = r.Name,
                TDL = r.TDL,
                Price = r.Price,
                Id = r.Id,
                IsCreated = r.IsCreated
                
            }).ToList();

            var response = new DomainResponseDto
            {
                Domains = domainDto
            };
            
            _logger.LogInformation("Successful receipt of domain list");
            return Result<DomainResponseDto>.Success(response);
        }
        catch (Exception)
        {
            _logger.LogError("Failed get domains");
            return Result<DomainResponseDto>.Failure("Failed get domains");
        }
    }

    public async Task<Result<DomainResponseDto>> GetDomainIdAsync(Guid id)
    {
        try
        {
            var domain = await _db.Domains.FindAsync(id);
            if (domain is null)
            {
                _logger.LogWarning("Domain not found {Id}", id);
                return Result<DomainResponseDto>.Failure("Domain not found");
            }
            
            var domainDto = new DomainResponseDto
            {
                Id = domain.Id,
                Name = domain.Name,
                TDL = domain.TDL,
                Price = domain.Price,
                IsCreated = domain.IsCreated,
                Orders = domain.Orders
            };

            _logger.LogInformation("Successfully getting list of domains by Id: {Id}", id);
            return Result<DomainResponseDto>.Success(domainDto);
        }
        catch (Exception)
        {
            _logger.LogError("Failed get domains Id {Id}", id);
            return Result<DomainResponseDto>.Failure("Failed get domains Id");
        } 
    }
}