using System.Net;
using System.Net.Http.Json;

using MemoirMap.Models.DTOs;

namespace MemoirMap.Tests.ControllerTests;

public abstract class SignUpControllerTestsBase
{
    private readonly TestFactoryBase _factory;

    public SignUpControllerTestsBase(TestFactoryBase factory)
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
            new LogInRequest
            {
                Username = username,
                Password = password
            }
        );

        HttpResponseMessage response = await client.PostAsJsonAsync
        (
            "/auth/signup",
            new LogInRequest
            {
                Username = username,
                Password = password
            }
        );

        // Assert
        Assert.Equal(HttpStatusCode.Conflict, response.StatusCode);
        ErrorResponse? errorsResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
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
            new LogInRequest
            {
                Username = username,
                Password = password
            }
        );

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        ErrorResponse? errorsResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.NotNull(errorsResponse);
        Assert.NotEmpty(errorsResponse.Errors);
        Assert.Contains(ApplicationErrorType.invalid_user_name.ToString(), errorsResponse.Errors);
    }

    [Theory]
    [InlineData("")]
    [InlineData("123")]
    [InlineData("_")]
    [InlineData("abcde")]
    
    public async Task SignUpControllerTest_BadPasswordFailure(string password)
    {
        // Arrange
        string username = Guid.NewGuid().ToString();
        HttpClient client = _factory.CreateClient();

        // Act
        HttpResponseMessage response = await client.PostAsJsonAsync
        (
            "/auth/signup",
            new LogInRequest
            {
                Username = username,
                Password = password
            }
        );

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        ErrorResponse? errorsResponse = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.NotNull(errorsResponse);
        Assert.NotEmpty(errorsResponse.Errors);
        Assert.Contains(ApplicationErrorType.password_too_short.ToString(), errorsResponse.Errors);
    }
}

public class SignUpControllerTestsBase_Identity : SignUpControllerTestsBase, IClassFixture<IdentityTestFactory>
{
    public SignUpControllerTestsBase_Identity(IdentityTestFactory factory) : base(factory) { }
}


public class SignUpControllerTestsBase_Custom : SignUpControllerTestsBase, IClassFixture<CustomTestFactory>
{
    public SignUpControllerTestsBase_Custom(CustomTestFactory factory) : base(factory) { }
}
