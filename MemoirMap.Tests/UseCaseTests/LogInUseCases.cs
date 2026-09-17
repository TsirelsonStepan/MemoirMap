using System.Net;
using System.Net.Http.Json;

using MemoirMap.Models.DTOs;

namespace MemoirMap.Tests.UseCaseTests;

public abstract class LogInUseCasesBase
{
    private readonly TestFactoryBase _factory;

    public LogInUseCasesBase(TestFactoryBase factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task LogInUseCaseTest_LockedOutFailure()
    {
        // Arrange
        string username = Guid.NewGuid().ToString();
        string password = "Password";
        HttpClient client = _factory.CreateClient();
        await SignUp(client, username, password);

        // Act
        for (int i = 0; i < 5; i++)
            await LogIn(client, username, "WrongPassword");
        HttpResponseMessage response = await LogIn(client, username, "WrongPassword");
        ErrorsResponse? errorsResponse = await response.Content.ReadFromJsonAsync<ErrorsResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.Locked, response.StatusCode);
        Assert.NotNull(errorsResponse);
        Assert.NotEmpty(errorsResponse.Errors);
        Assert.Contains(ApplicationErrorType.user_locked_out.ToString(), errorsResponse.Errors);
    }

    [Fact]
    public async Task LogInUseCaseTest_InvalidCredentialsFailure()
    {
        // Arrange
        string username = Guid.NewGuid().ToString();
        string password = "Password";
        HttpClient client = _factory.CreateClient();
        await SignUp(client, username, password);

        // Act
        HttpResponseMessage response = await LogIn(client, username, "WrongPassword");
        ErrorsResponse? errorsResponse = await response.Content.ReadFromJsonAsync<ErrorsResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        Assert.NotNull(errorsResponse);
        Assert.NotEmpty(errorsResponse.Errors);
        Assert.Contains(ApplicationErrorType.invalid_credentials.ToString(), errorsResponse.Errors);
    }

    private async Task<HttpResponseMessage> SignUp(HttpClient client, string username, string password)
    {
        HttpResponseMessage responseMessage = await client.PostAsJsonAsync
        (
            "/auth/signup",
            new LoginRequest
            {
                Username = username,
                Password = password
            }
        );

        responseMessage.EnsureSuccessStatusCode();
        return responseMessage;
    }

    private async Task<HttpResponseMessage> LogIn(HttpClient client, string username, string password)
    {
        HttpResponseMessage responseMessage = await client.PostAsJsonAsync
        (
            "/auth/login",
            new LoginRequest
            {
                Username = username,
                Password = password
            }
        );

        return responseMessage;
    }
}

public class LogInUseCases_Identity : LogInUseCasesBase, IClassFixture<IdentityTestFactory>
{
    public LogInUseCases_Identity(IdentityTestFactory factory) : base(factory) { }
}

/*
public class LogInUseCases_Custom : LogInUseCasesBase, IClassFixture<IdentityTestFactory>
{
    public LogInUseCases_Custom(IdentityTestFactory factory) : base(factory) { }
}
*/