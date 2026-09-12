using MemoirMap.Models.EntityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MemoirMap.Infrastructure;

public class ApplicationDbContext : IdentityDbContext<UserAccountEntity>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {}

    //Future DbSets
}