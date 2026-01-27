using System.Text;
using Microsoft.EntityFrameworkCore;
using BookingProject.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using BookingProject.Endpoint.Auth;
using BookingProject.Endpoint.Booking;
using BookingProject.Endpoint.GetInfo;
using BookingProject.Endpoint.Rooms;
using BookingProject.Services;
using FluentValidation;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
    .WriteTo.Console()
    .WriteTo.File("logs/identity-.txt", 
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>(x =>
    x.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddValidatorsFromAssemblyContaining<Program>();



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddScoped<IUserService , UserService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IDomainService, DomainService>();

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    });
    builder.Services.AddAuthorization();
    builder.Host.UseSerilog();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapAuthUserEndpoint();
app.MapAuthAdminEndpoint();

app.UserLogin();
app.GetUser();
app.OrderGet();
app.OrderEndpoint();
app.GetDomain();
app.DomainEndpoint();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();