using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.TestHost;

using MemoirMap.Infrastructure.Identity;

namespace MemoirMap.Tests;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            ServiceDescriptor? dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IDbContextOptionsConfiguration<IdentityApplicationDbContext>));

            if (dbContextDescriptor != null) services.Remove(dbContextDescriptor);

            ServiceDescriptor? dbConnectionDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbConnection));

            if (dbConnectionDescriptor != null) services.Remove(dbConnectionDescriptor);

            // Create open SqliteConnection so EF won't automatically close it.
            services.AddSingleton<DbConnection>(container =>
            {
                SqliteConnection connection = new("DataSource=:memory:");
                connection.Open();

                return connection;
            });

            services.AddDbContext<IdentityApplicationDbContext>((container, options) =>
            {
                DbConnection connection = container.GetRequiredService<DbConnection>();
                options.UseSqlite(connection);
            });
        });

        builder.UseEnvironment("Development");
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        IHost host = base.CreateHost(builder);

        using IServiceScope scope = host.Services.CreateScope();
        IdentityApplicationDbContext db = scope.ServiceProvider.GetRequiredService<IdentityApplicationDbContext>();
        db.Database.EnsureCreated();

        return host;
    }
}