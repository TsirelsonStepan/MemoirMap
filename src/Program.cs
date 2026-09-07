using MemoirMap.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


//MINE BEGIN
builder.Services.InjectServices();
builder.Services.AddAuthorizationToOpenApiEndpoints();
builder.Services.InitializeNpgsqlDatabase
(
    builder.Configuration.GetConnectionString("DefaultConnection")
);
builder.Services.InitializeIdentity();

builder.Services.AddExceptionHandler<ApplicationExceptionHandler>();

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

/*

// ... other services

app.UseExceptionHandler(); // must be placed after routing, but before endpoints




string? pemFilePath = builder.Configuration["PrivateKeyFilePath"];
string privateKeyPem = File.ReadAllText(pemFilePath ?? throw new Exception());
builder.Configuration.AddInMemoryCollection(new Dictionary<string, string?>
{
    ["PrivateKeyPem"] = privateKeyPem
});

builder.Services.InitializeLocalRsaSigner
(
    builder.Configuration.GetValue<string>("PrivateKeyPem"),
    out ISigner signer
);
builder.Services.InitializeJwtAuthentication
(
    builder.Configuration,
    signer
);
builder.Services.AddAuthorization();
*/