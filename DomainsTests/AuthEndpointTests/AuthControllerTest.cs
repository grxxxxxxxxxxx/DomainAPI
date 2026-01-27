using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace BookingTests.EndpointAdminTests;

public class AuthControllerTest : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;

    public AuthControllerTest(WebApplicationFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task Register_User_ReturnCreated()
    {
        var random = new Random();
        var uniqueEmail = $"test{random.Next(100000)}@example.com";

        var registerData = new
        {
            Name = "Lhlushdkf",
            Email = uniqueEmail,
            Password = "Testsdkfh9898h32$Hbsd",
            Phone = "+79159086211"
        };

        var response = await _httpClient.PostAsJsonAsync("/api/register", registerData);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }

    [Fact]
    public async Task Login_User_ReturnOk()
    {
        var random = new Random();
        var uniqueEmail = $"test{random.Next(100000)}@example.com";

        var registerData = new
        {
            Name = "Uesrscslk",
            Email = uniqueEmail,
            Password = "Testsdkfh9898h32$Hbsd",
            Phone = "+79159086211"
        };
        
        var loginData = new
        {
            Email = uniqueEmail,
            Password = "Testsdkfh9898h32$Hbsd"
        };

        var responseRegitser = await _httpClient.PostAsJsonAsync("/api/register", registerData);
        Assert.Equal(HttpStatusCode.Created , responseRegitser.StatusCode);

        var response = await _httpClient.PostAsJsonAsync("/api/login", loginData);
        Assert.Equal(HttpStatusCode.OK , response.StatusCode);
    }
}