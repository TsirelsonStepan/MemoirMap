using System.Net;
using System.Net.Http.Json;

using MemoirMap.Models.DTOs;

namespace MemoirMap.Tests.ControllerTests;

public class SignUpControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public SignUpControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task SignUpControllerTest_DuplicateUsernameFailure()
    {
        // Arrange
        string username = Guid.NewGuid().ToString();
        string password = "Password";
        HttpClient client = _factory.CreateClient();

        // Act
        await client.PostAsJsonAsync
        (
            "/auth/signup",
            new LoginRequest
            {
                Username = username,
                Password = password
            }
        );

        HttpResponseMessage response = await client.PostAsJsonAsync
        (
            "/auth/signup",
            new LoginRequest
            {
                Username = username,
                Password = password
            }
        );
        ErrorsResponse? errorsResponse = await response.Content.ReadFromJsonAsync<ErrorsResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        Assert.NotNull(errorsResponse);
        Assert.NotEmpty(errorsResponse.Errors);
        Assert.Contains(ApplicationErrorType.duplicate_user_name.ToString(), errorsResponse.Errors);
    }

    [Fact]
    public async Task SignUpControllerTest_BadUsernameFailure()
    {
        // Arrange
        string username = "";
        string password = "Password";
        HttpClient client = _factory.CreateClient();

        // Act
        HttpResponseMessage response = await client.PostAsJsonAsync
        (
            "/auth/signup",
            new LoginRequest
            {
                Username = username,
                Password = password
            }
        );
        ErrorsResponse? errorsResponse = await response.Content.ReadFromJsonAsync<ErrorsResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(errorsResponse);
        Assert.NotEmpty(errorsResponse.Errors);
        Assert.Contains(ApplicationErrorType.invalid_user_name.ToString(), errorsResponse.Errors);
    }

    [Fact]
    public async Task SignUpControllerTest_BadPasswordFailure()
    {
        // Arrange
        string username = Guid.NewGuid().ToString();
        string password = "_";
        HttpClient client = _factory.CreateClient();

        // Act
        HttpResponseMessage response = await client.PostAsJsonAsync
        (
            "/auth/signup",
            new LoginRequest
            {
                Username = username,
                Password = password
            }
        );
        ErrorsResponse? errorsResponse = await response.Content.ReadFromJsonAsync<ErrorsResponse>();

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        Assert.NotNull(errorsResponse);
        Assert.NotEmpty(errorsResponse.Errors);
        Assert.Contains(ApplicationErrorType.password_too_short.ToString(), errorsResponse.Errors);
    }
}