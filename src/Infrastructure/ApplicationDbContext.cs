using MemoirMap.Models.EntityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MemoirMap.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var user = modelBuilder.Entity<UserAccountEntity>();

        user
        .Property(x => x.Username)
        .IsRequired()
        .HasMaxLength(100);

        user
        .HasIndex(x => x.Username)
        .IsUnique();

        user
        .Property(x => x.PasswordHash)
        .IsRequired();
    }

    public DbSet<UserAccountEntity> UserAccounts { get; set; }
}

public class UserAccountStore : IUserStore<UserAccountEntity>, IUserPasswordStore<UserAccountEntity>
{
    private readonly ApplicationDbContext _db;

    public UserAccountStore(ApplicationDbContext db)
    {
        _db = db;
    }

    public Task<string> GetUserIdAsync(UserAccountEntity user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Id);
    }

    public Task<string?> GetUserNameAsync(UserAccountEntity user, CancellationToken cancellationToken)
    {
        return Task.FromResult<string?>(user.Username);
    }

    public Task SetUserNameAsync(UserAccountEntity user, string? userName, CancellationToken cancellationToken)
    {
        user.Username = userName!;
        return Task.CompletedTask;
    }

    public Task<string?> GetNormalizedUserNameAsync(UserAccountEntity user, CancellationToken cancellationToken)
    {
        return Task.FromResult<string?>(user.Username.ToUpperInvariant());
    }

    public Task SetNormalizedUserNameAsync(UserAccountEntity user, string? normalizedName, CancellationToken cancellationToken)
    {
        user.Username = normalizedName!;
        return Task.CompletedTask;
    }

    public async Task<IdentityResult> CreateAsync(UserAccountEntity user, CancellationToken cancellationToken)
    {
        _db.UserAccounts.Add(user);
        await _db.SaveChangesAsync(cancellationToken);

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(UserAccountEntity user, CancellationToken cancellationToken)
    {
        _db.UserAccounts.Update(user);
        await _db.SaveChangesAsync(cancellationToken);

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(UserAccountEntity user, CancellationToken cancellationToken)
    {
        _db.UserAccounts.Remove(user);
        await _db.SaveChangesAsync(cancellationToken);

        return IdentityResult.Success;
    }

    public Task<UserAccountEntity?> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        return _db.UserAccounts.FindAsync([userId], cancellationToken).AsTask();
    }

    public Task<UserAccountEntity?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        return _db.UserAccounts.FirstOrDefaultAsync(x => x.Username == normalizedUserName, cancellationToken);
    }

    public void Dispose()
    {
        
    }

    public Task SetPasswordHashAsync(UserAccountEntity user, string? passwordHash, CancellationToken cancellationToken)
    {
        user.PasswordHash = passwordHash!;
        return Task.CompletedTask;
    }

    public Task<string?> GetPasswordHashAsync(UserAccountEntity user, CancellationToken cancellationToken)
    {
        return Task.FromResult<string?>(user.PasswordHash);
    }

    public Task<bool> HasPasswordAsync(UserAccountEntity user, CancellationToken cancellationToken)
    {
        return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
    }
}