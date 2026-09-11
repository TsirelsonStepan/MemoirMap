using MemoirMap.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//MINE BEGIN
builder.Services.InjectServices();
builder.Services.AddAuthorizationToOpenApiEndpoints();

//builder.Services.InitializeNpgsqlDatabase(builder.Configuration.GetConnectionString("PostgresqlConnection"));
builder.Services.InitializeSqliteDatabase(builder.Configuration.GetConnectionString("SqliteConnection"));

builder.Services.InitializeIdentity();

builder.Services.AddExceptionHandler<ApplicationExceptionHandler>();
builder.Services.InitializeJwt
(
    builder.Configuration.GetSection("Jwt").Get<JwtOptions>() ?? throw new InvalidOperationException("JWT configuration is undefined"),
    builder.Configuration.GetValue<string>("PrivateKeyPem")
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
app.UseExceptionHandler(exceptionHandlerApp =>
{
    exceptionHandlerApp.Run(async context =>
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
    });
});
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