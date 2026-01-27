using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Net;

namespace BookingTests.EndpointAdminTests;

public class LoginTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public LoginTest(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }
    [Fact]
    public async Task Login_User_ReturnUnauthorized401()
    {
        var random = new Random();
        var uniqueEmail = $"test{random.Next(100000)}@example.com";

        var registerData = new
        {
            Name = "Dsdfi7IBd",
            Email = uniqueEmail,
            Password = "Testsdkfh9898h32$Hbsd",
            Phone = "+79159086211"
        };
        
        var loginData = new
        {
            Email = uniqueEmail,
            Password = "Testsdkfasd"
        };

        var responseRegitser = await _client.PostAsJsonAsync("/api/register", registerData);
        Assert.Equal(HttpStatusCode.Created , responseRegitser.StatusCode);

        var response = await _client.PostAsJsonAsync("/api/login", loginData);
        Assert.Equal(HttpStatusCode.Unauthorized , response.StatusCode);
    }
}