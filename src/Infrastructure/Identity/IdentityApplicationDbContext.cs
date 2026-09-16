using MemoirMap.Models.EntityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MemoirMap.Infrastructure.Identity;

public class IdentityApplicationDbContext : IdentityDbContext<IdentityUserAccountEntity>
{
    public IdentityApplicationDbContext(DbContextOptions<IdentityApplicationDbContext> options): base(options) {}

    //Future DbSets
}