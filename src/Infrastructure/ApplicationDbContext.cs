using MemoirMap.Models.EntityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MemoirMap.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<UserAccountEntity>
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<UserAccountEntity>().Ignore(x => x.Username);
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {}

    //Future DbSets
}