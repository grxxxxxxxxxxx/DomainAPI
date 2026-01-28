using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System.Net.Http;
using System.Threading.Tasks;
using System;

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
        var uniqueEmail = $"test{Guid.NewGuid():N}@example.com";

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
        var uniqueEmail = $"test{Guid.NewGuid():N}@example.com";

        var registerData = new
        {
            Name = "Uesrscslk",
            Email = uniqueEmail,
            Password = "Testsdkfh9898h32$Hbsd",
            Phone = "+79159086211"
        };

        var responseReg = await _httpClient.PostAsJsonAsync("/api/register", registerData);
        
        var loginData = new
        {
            Email = uniqueEmail,
            Password = "Testsdkfh9898h32$Hbsd"
        };

        var response = await _httpClient.PostAsJsonAsync("/api/login", loginData);
        Assert.Equal(HttpStatusCode.OK , response.StatusCode);
    }
}