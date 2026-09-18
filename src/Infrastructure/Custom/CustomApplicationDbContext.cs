using MemoirMap.Models.EntityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MemoirMap.Infrastructure.Custom;

public class CustomApplicationDbContext : DbContext
{
    public CustomApplicationDbContext(DbContextOptions<CustomApplicationDbContext> options): base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        var user = modelBuilder.Entity<CustomUserAccountEntity>();

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

    public DbSet<CustomUserAccountEntity> UserAccounts { get; set; }
}

public class CustomUserStore : IUserStore<CustomUserAccountEntity>, IUserPasswordStore<CustomUserAccountEntity>
{
    private readonly CustomApplicationDbContext _db;

    public CustomUserStore(CustomApplicationDbContext db)
    {
        _db = db;
    }

    public Task<string> GetUserIdAsync(CustomUserAccountEntity user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.Id);
    }

    public Task<string?> GetUserNameAsync(CustomUserAccountEntity user, CancellationToken cancellationToken)
    {
        return Task.FromResult<string?>(user.Username);
    }

    public Task SetUserNameAsync(CustomUserAccountEntity user, string? userName, CancellationToken cancellationToken)
    {
        user.Username = userName!;
        return Task.CompletedTask;
    }

    public Task<string?> GetNormalizedUserNameAsync(CustomUserAccountEntity user, CancellationToken cancellationToken)
    {
        return Task.FromResult<string?>(user.NormalizedUsername);
    }

    public Task SetNormalizedUserNameAsync(CustomUserAccountEntity user, string? normalizedName, CancellationToken cancellationToken)
    {
        user.NormalizedUsername = normalizedName ?? string.Empty;
        return Task.CompletedTask;
    }

    public async Task<IdentityResult> CreateAsync(CustomUserAccountEntity user, CancellationToken cancellationToken)
    {
        user.Id = Guid.NewGuid().ToString();
        _db.UserAccounts.Add(user);
        await _db.SaveChangesAsync(cancellationToken);

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(CustomUserAccountEntity user, CancellationToken cancellationToken)
    {
        _db.UserAccounts.Update(user);
        await _db.SaveChangesAsync(cancellationToken);

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(CustomUserAccountEntity user, CancellationToken cancellationToken)
    {
        _db.UserAccounts.Remove(user);
        await _db.SaveChangesAsync(cancellationToken);

        return IdentityResult.Success;
    }

    public Task<CustomUserAccountEntity?> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        return _db.UserAccounts.FindAsync([userId], cancellationToken).AsTask();
    }

    public Task<CustomUserAccountEntity?> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        return _db.UserAccounts.FirstOrDefaultAsync(x => x.NormalizedUsername == normalizedUserName, cancellationToken);
    }

    public void Dispose()
    {
        
    }

    public Task SetPasswordHashAsync(CustomUserAccountEntity user, string? passwordHash, CancellationToken cancellationToken)
    {
        user.PasswordHash = passwordHash!;
        return Task.CompletedTask;
    }

    public Task<string?> GetPasswordHashAsync(CustomUserAccountEntity user, CancellationToken cancellationToken)
    {
        return Task.FromResult<string?>(user.PasswordHash);
    }

    public Task<bool> HasPasswordAsync(CustomUserAccountEntity user, CancellationToken cancellationToken)
    {
        return Task.FromResult(!string.IsNullOrEmpty(user.PasswordHash));
    }
}