using MemoirMap.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//MINE BEGIN
builder.Services.InjectCommon();

builder.Services.InjectIdentityUser();
builder.Services.InitializeIdentity();
//builder.Services.InitializeNpgsqlDatabaseWithIdentity(builder.Configuration.GetConnectionString("PostgresqlConnection"));
builder.Services.InitializeSqliteDatabaseWithIdentity(builder.Configuration.GetConnectionString("SqliteConnection"));

builder.Services.AddAuthorizationToOpenApiEndpoints();

builder.Services.AddExceptionHandler<ApplicationExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.InitializeJwt
(
    builder.Configuration.GetSection("Jwt").Get<JwtOptions>()
    ?? throw new InvalidOperationException("JWT configuration is undefined")
);
builder.Services.AddAuthorization();

//MINE END

var app = builder.Build();

//MINE BEGIN
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "v1");
    });
}

//app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseExceptionHandler();
//MINE END

app.UseHsts();
app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();