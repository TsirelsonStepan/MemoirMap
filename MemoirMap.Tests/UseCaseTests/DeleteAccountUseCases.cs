using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using MemoirMap.Infrastructure;
using MemoirMap.Models.DTOs;

namespace MemoirMap.Tests.UseCaseTests;

public class DeleteAccountUseCases : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public DeleteAccountUseCases(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task DeleteAccountUseCaseTest_Success()
    {
        // Arrange
        string username = Guid.NewGuid().ToString();
        string password = "Password";
        HttpClient client = _factory.CreateClient();

        // Act
        await SignUp(client, username, password);

        string token = await LogIn(client, username, password);
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        await DeleteAccount(client, username);
    }

    private async Task<HttpResponseMessage> SignUp(HttpClient client, string username, string password)
    {
        // Act
        HttpResponseMessage responseMessage = await client.PostAsJsonAsync
        (
            "/auth/signup",
            new LoginRequest
            {
                Username = username,
                Password = password
            }
        );

        // Assert
        responseMessage.EnsureSuccessStatusCode();

        return responseMessage;
    }

    private async Task<string> LogIn(HttpClient client, string username, string password)
    {
        // Act
        HttpResponseMessage responseMessage = await client.PostAsJsonAsync
        (
            "/auth/login",
            new LoginRequest
            {
                Username = username,
                Password = password
            }
        );

        // Assert
        responseMessage.EnsureSuccessStatusCode();
        LogInResponse? logInResponse = await responseMessage.Content.ReadFromJsonAsync<LogInResponse>();
        Assert.NotNull(logInResponse);
        Assert.NotNull(logInResponse.AccessToken);
        
        return logInResponse.AccessToken;
    }

    private async Task DeleteAccount(HttpClient client, string username)
    {
        // Arrange
        using IServiceScope scope = _factory.Services.CreateScope();
        ApplicationDbContext db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Act
        bool existsBeforeDelete = await db.Users.AnyAsync(x => x.UserName == username);
        HttpResponseMessage responseMessage = await client.DeleteAsync("/my/account");
        bool existsAfterDelete = await db.Users.AnyAsync(x => x.UserName == username);

        // Assert
        responseMessage.EnsureSuccessStatusCode();
        Assert.True(existsBeforeDelete && !existsAfterDelete, "User should be deleted");
    }
}