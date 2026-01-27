using BookingProject.Models;
using Microsoft.EntityFrameworkCore;

namespace BookingProject.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<User> Users { get; set; }
    public DbSet<Domain> Domains { get; set; }
    public DbSet<Order> Orders { get; set; }
}