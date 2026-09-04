using Microsoft.AspNetCore.Identity;

namespace MemoirMap.Infrastructure;

public static class IdentityResultMapper
{
    private static readonly IReadOnlyDictionary<string, string> IdentityErrorRenameMapper = new Dictionary<string, string>()
    {
        ["DefaultError"] = "default_error",
        ["UserLockoutNotEnabled"] = "user_lockout_not_enabled",

        ["UserNotInRole"] = "user_not_in_role",

        ["DuplicateEmail"] = "duplicate_email",
        ["DuplicateUserName"] = "duplicate_user_name",
        ["DuplicateRoleName"] = "duplicate_role_name",
        ["LoginAlreadyAssociated"] = "login_already_associated",
        ["UserAlreadyHasPassword"] = "user_already_has_password",
        ["UserAlreadyInRole"] = "user_already_in_role",
        ["ConcurrencyFailure"] = "concurrency_failure",

        ["InvalidEmail"] = "invalid_email",
        ["InvalidUserName"] = "invalid_user_name",
        ["InvalidRoleName"] = "invalid_role_name",
        ["PasswordMismatch"] = "password_mismatch",
        ["PasswordRequiresDigit"] = "password_requires_digit",
        ["PasswordRequiresLower"] = "password_requires_lower",
        ["PasswordRequiresNonAlphanumeric"] = "password_requires_non_alphanumeric",
        ["PasswordRequiresUniqueChars"] = "password_requires_unique_chars",
        ["PasswordRequiresUpper"] = "password_requires_upper",
        ["PasswordTooShort"] = "password_too_short",
        ["InvalidToken"] = "invalid_token",
        ["RecoveryCodeRedemptionFailed"] = "recovery_code_redemption_failed",
    };

    public static ServiceResult Map(this IdentityResult identityResult)
    {
        if (identityResult.Succeeded) return ServiceResult.Success();
        
        IEnumerable<string> errorCodes = identityResult.Errors.Select(error =>
        {
            if (IdentityErrorRenameMapper.TryGetValue(error.Code, out string? newCode)) return newCode;
            else throw new InvalidOperationException($"Unknown IdentityError error code: {error}");
        });

        return ServiceResult.Failure(errorCodes);
    }
}