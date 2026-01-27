using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace BookingTests.FluentTestsNegative;

public class AuthErrorTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public AuthErrorTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Register_UserShortName_Return400()
    {
        var random = new Random();
        var uniqueEmail = $"test{random.Next(100000)}@example.com";
        
        var user = new
        {
            Name = "Se",
            Email = uniqueEmail,
            Password = "Testsdkfh9898h32$Hbsd",
            Phone = "+79159086211"
        };

        var response = await _client.PostAsJsonAsync("/api/register" , user);
        Assert.Equal(HttpStatusCode.BadRequest , response.StatusCode);
    }
    [Fact]
    public async Task Register_UserLongName_Return400()
    {
        var random = new Random();
        var uniqueEmail = $"test{random.Next(100000)}@example.com";
        
        var user = new
        {
            Name = "Seasdsadasdsadsadasdsadsadsadadasdsadsadsadasdasdsadadsadasdasd",
            Email = uniqueEmail,
            Password = "Testsdkfh9898h32$Hbsd",
            Phone = "+79159086211"
        };

        var response = await _client.PostAsJsonAsync("/api/register" , user);
        Assert.Equal(HttpStatusCode.BadRequest , response.StatusCode);
    }
    [Fact]
    public async Task Register_UserNullName_Return400()
    {
        var random = new Random();
        var uniqueEmail = $"test{random.Next(100000)}@example.com";
        
        var user = new
        {
            Name = "",
            Email = uniqueEmail,
            Password = "Testsdkfh9898h32$Hbsd",
            Phone = "+79159086211"
        };

        var response = await _client.PostAsJsonAsync("/api/register" , user);
        Assert.Equal(HttpStatusCode.BadRequest , response.StatusCode);
    }

    [Fact]
    public async Task Register_UserEmailNull_Return400()
    {
        
        var user = new
        {
            Name = "UGT6Fsdfsdf",
            Email = "",
            Password = "Testsdkfh9898h32$Hbsd",
            Phone = "+79159086211"
        };
        
        var response = await _client.PostAsJsonAsync("/api/register" , user);
        Assert.Equal(HttpStatusCode.BadRequest , response.StatusCode);
    }
    [Fact]
    public async Task Register_UserEmailInvalid_Return400()
    {
        
        var user = new
        {
            Name = "UGT6Fsdfsdf",
            Email = "testgmail,com",
            Password = "Testsdkfh9898h32$Hbsd",
            Phone = "+79159086211"
        };
        
        var response = await _client.PostAsJsonAsync("/api/register" , user);
        Assert.Equal(HttpStatusCode.BadRequest , response.StatusCode);
    }
    [Fact]
    public async Task Register_UserPassNull_Return400()
    {
        
        var user = new
        {
            Name = "UGT6Fsdfsdf",
            Email = "test123123@gmail.com",
            Password = "",
            Phone = "+79159086211"
        };
        
        var response = await _client.PostAsJsonAsync("/api/register" , user);
        Assert.Equal(HttpStatusCode.BadRequest , response.StatusCode);
    }
    [Fact]
    public async Task Register_UserPassShort_Return400()
    {
        
        var user = new
        {
            Name = "UGT6Fsdfsdf",
            Email = "test123123@gmail.com",
            Password = "1233@",
            Phone = "+79159086211"
        };
        
        var response = await _client.PostAsJsonAsync("/api/register" , user);
        Assert.Equal(HttpStatusCode.BadRequest , response.StatusCode);
    }
    [Fact]
    public async Task Register_UserPassSpecialChart_Return401()
    {
        
        var user = new
        {
            Name = "UGT6Fsdfsdf",
            Email = "test123123@gmail.com",
            Password = "JYUu6btubxsuefqw3xw3xdw3f",
            Phone = "+79159086211"
        };
        
        var response = await _client.PostAsJsonAsync("/api/register" , user);
        Assert.Equal(HttpStatusCode.BadRequest , response.StatusCode);
    }
    [Fact]
    public async Task Register_UserPhoneNull_Return400()
    {
        
        var user = new
        {
            Name = "UGT6Fsdfsdf",
            Email = "test123123@gmail.com",
            Password = "JYUu6btu%bxsuefqw3x$w3xdw3f",
            Phone = ""
        };
        
        var response = await _client.PostAsJsonAsync("/api/register" , user);
        Assert.Equal(HttpStatusCode.BadRequest , response.StatusCode);
    }
    [Fact]
    public async Task Register_UserPhoneShort_Return400()
    {
        
        var user = new
        {
            Name = "UGT6Fsdfsdf",
            Email = "test123123@gmail.com",
            Password = "JYUu6btu%bxsuefqw3x$w3xdw3f",
            Phone = "+791577333"
        };
        
        var response = await _client.PostAsJsonAsync("/api/register" , user);
        Assert.Equal(HttpStatusCode.BadRequest , response.StatusCode);
    }
}