using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Net;
using System;
using System.Net.Http;
using System.Threading.Tasks;

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
        var uniqueEmail = $"test{Guid.NewGuid():N}@example.com";

        var registerData = new
        {
            Name = "Dsdfi7IBd",
            Email = uniqueEmail,
            Password = "Testsdkfh9898h32$Hbsd",
            Phone = "+79159086211"
        };
        var responseRegv = await _client.PostAsJsonAsync("/api/register", registerData);
        
        var loginData = new
        {
            Email = uniqueEmail,
            Password = "Testsdkfasd"
        };

        var response = await _client.PostAsJsonAsync("/api/login", loginData);
        Assert.Equal(HttpStatusCode.Unauthorized , response.StatusCode);
    }
}