using System.Net.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using MemoirMap.Models.DTOs;
using MemoirMap.Infrastructure.Identity;
using MemoirMap.Infrastructure.Custom;

namespace MemoirMap.Tests.UseCaseTests;

public abstract class DeleteAccountUseCasesBase
{
    protected readonly TestFactoryBase _factory;

    protected DeleteAccountUseCasesBase(TestFactoryBase factory)
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
            new LogInRequest
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
            new LogInRequest
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
        // Act
        bool existsBeforeDelete = await IsInDb(username);
        HttpResponseMessage responseMessage = await client.DeleteAsync("/my/account");
        bool existsAfterDelete = await IsInDb(username);

        // Assert
        responseMessage.EnsureSuccessStatusCode();
        Assert.True(existsBeforeDelete && !existsAfterDelete, "User should be deleted");
    }

    public abstract Task<bool> IsInDb(string username);
}

public class DeleteAccountUseCases_Identity : DeleteAccountUseCasesBase, IClassFixture<IdentityTestFactory>
{
    public DeleteAccountUseCases_Identity(IdentityTestFactory factory) : base(factory) { }

    public override async Task<bool> IsInDb(string username)
    {
        using IServiceScope scope = _factory.CreateScope();
        IdentityApplicationDbContext db = scope.ServiceProvider.GetRequiredService<IdentityApplicationDbContext>();
        return await db.Users.AnyAsync(x => x.UserName == username);
    }
}


public class DeleteAccountUseCases_Custom : DeleteAccountUseCasesBase, IClassFixture<CustomTestFactory>
{
    public DeleteAccountUseCases_Custom(CustomTestFactory factory) : base(factory) { }

    public override async Task<bool> IsInDb(string username)
    {
        using IServiceScope scope = _factory.CreateScope();
        CustomApplicationDbContext db = scope.ServiceProvider.GetRequiredService<CustomApplicationDbContext>();
        return await db.UserAccounts.AnyAsync(x => x.Username == username);
    }
}
