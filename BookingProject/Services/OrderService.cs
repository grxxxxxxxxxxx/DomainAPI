using System.Data.Entity.Core.Mapping;
using BookingProject.DTOs;
using BookingProject.Common;
using BookingProject.Data;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using BookingProject.Models;

namespace BookingProject.Services;

public interface IOrderService
{
    Task<Result<OrderResponseDto>> CreateOrderAsync(OrderDto dto);
    Task<Result> DeleteOrderAsync(Guid id);

    Task<Result<OrderResponseDto>> GetOrderAsync();
    Task<Result<OrderDto>> GetOrderIdAsync(Guid id);
}

public class OrderService : IOrderService
{

    private readonly AppDbContext _db;
    private readonly ILogger<OrderService> _logger;
    private readonly IValidator<OrderDto> _validator;

    public OrderService(AppDbContext db , 
        ILogger<OrderService> logger , IValidator<OrderDto> validator)
    {
        _db = db;
        _logger = logger;
        _validator = validator;

    }
    public async Task<Result<OrderResponseDto>> CreateOrderAsync(OrderDto dto)
    {
        try
        {
            _logger.LogInformation("Domain {DomainId} reservation -> {UserId}" ,
                dto.DomainId , dto.UserId);

            var validatorResult = await _validator.ValidateAsync(dto);
            if (!validatorResult.IsValid)
            {
                _logger.LogWarning("Invalid data order -> {UserId}" , dto.UserId);
                return Result<OrderResponseDto>.Failure("Invalid data order");
            }

            var bookingFreedom = await _db.Orders
                .AnyAsync(r => r.DomainId == dto.DomainId
                               && r.StartDate < dto.ExpiresData
                               && r.ExpiresDate > dto.StartData);
            if (bookingFreedom)
            {
                _logger.LogWarning("The domain is occupied for this period -> {DomainId}" , dto.DomainId);
                return Result<OrderResponseDto>.Failure("The domain is occupied for this period");
            }

            var newOrder = new Order
            {
                Id = dto.Id,
                Domain = dto.Domain,
                DomainId = dto.DomainId,
                CreatedAt = DateTime.UtcNow,
                StartDate = dto.StartData,
                ExpiresDate = dto.ExpiresData,
                User = dto.User,
                UserId = dto.UserId
            };

            await _db.Orders.AddAsync(newOrder);
            await _db.SaveChangesAsync();
            
            _logger.LogInformation("Domain {DomainId} has been successfully booked {UserId}" ,
                dto.DomainId , dto.UserId);
            
            return Result<OrderResponseDto>.Success(new OrderResponseDto
            {
                Id = newOrder.Id,
                Domain = newOrder.Domain,
                DomainId = newOrder.DomainId,
                CreatedAt = newOrder.CreatedAt,
                StartDate = newOrder.StartDate,
                ExpiresData = newOrder.ExpiresDate
            });

        }
        catch (Exception)
        {
            _logger.LogError("Failed booking domain {DomainId} -> {UserId}" ,
                dto.DomainId , dto.UserId);
            return Result<OrderResponseDto>.Failure("Failed booking domain");
        }
    }

    public async Task<Result> DeleteOrderAsync(Guid id)
    {
        try
        {
            _logger.LogInformation("Deleting domain -> {DomainId}" , id);
            var order = await _db.Orders.FindAsync(id);
            if (order is null)
            {
                _logger.LogWarning("Order not found -> {OrderId}" , id);
                return Result.Failure("Order not found");
            }

            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();
            return Result.Success();
        }
        catch (Exception)
        {
            _logger.LogError("Failed order deleted -> {OrderId}" , id);
            return Result.Failure("Failed order deleted");
        }
    }

    public async Task<Result<OrderResponseDto>> GetOrderAsync()
    {
        try
        {
            var orders = await _db.Orders.ToListAsync();
            
            var orderDtos = orders.Select(u => new OrderDto
            {
                Id = u.Id,
                Domain = u.Domain,
                DomainId = u.DomainId,
                User = u.User,
                UserId = u.UserId,
                StartData = u.StartDate,
                ExpiresData = u.ExpiresDate,
                CreatedAt = u.CreatedAt
                
            }).ToList();

            var response = new OrderResponseDto
            {
                Orders = orderDtos
            };
            
            _logger.LogInformation("Successfully retrieved list of orders");
            return Result<OrderResponseDto>.Success(response);
        }
        catch (Exception)
        {
            _logger.LogError("Failed get orders");
            return Result<OrderResponseDto>.Failure("Failed get orders");
        }
    }

    public async Task<Result<OrderDto>> GetOrderIdAsync(Guid id)
    {
        try
        {
            var order = await _db.Orders.FindAsync(id);
            if (order is null)
            {
                _logger.LogWarning("Order not found -> {Id}" , id);
                return Result<OrderDto>.Failure("Order not found");
            }

            var orderDto = new OrderDto
            {
                Id = order.Id,
                Domain = order.Domain,
                DomainId = order.DomainId,
                User = order.User,
                UserId = order.UserId,
                StartData = order.StartDate,
                ExpiresData = order.ExpiresDate,
                CreatedAt = order.CreatedAt
            };
            
            _logger.LogInformation("Successfully retrieved order by Id -> {Id}" , id);
            return Result<OrderDto>.Success(orderDto);
        }
        catch (Exception)
        {
            _logger.LogError("Failed get order Id -> {Id}" , id);
            return Result<OrderDto>.Failure("Failed get order Id");
        }
    }
}