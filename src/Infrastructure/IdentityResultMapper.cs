
using Microsoft.AspNetCore.Identity;

namespace MemoirMap.Infrastructure;

public static class IdentityResultMapper
{
    private static readonly IReadOnlyDictionary<string, ApplicationErrorType> IdentityErrorCodeRenameMap = new Dictionary<string, ApplicationErrorType>()
    {
        // these are redundant, because 500 error is thrown as an exception rather then returned, so it doesn't require a mapping
        //["DefaultError"] = ApplicationErrorType.default_error,
        //["UserLockoutNotEnabled"] = ApplicationErrorType.user_lockout_not_enabled,

        ["UserNotInRole"] = ApplicationErrorType.user_not_in_role,

        ["DuplicateEmail"] = ApplicationErrorType.duplicate_email,
        ["DuplicateUserName"] = ApplicationErrorType.duplicate_user_name,
        ["DuplicateRoleName"] = ApplicationErrorType.duplicate_role_name,
        ["LoginAlreadyAssociated"] = ApplicationErrorType.login_already_associated,
        ["UserAlreadyHasPassword"] = ApplicationErrorType.user_already_has_password,
        ["UserAlreadyInRole"] = ApplicationErrorType.user_already_in_role,
        ["ConcurrencyFailure"] = ApplicationErrorType.concurrency_failure,

        ["InvalidEmail"] = ApplicationErrorType.invalid_email,
        ["InvalidUserName"] = ApplicationErrorType.invalid_user_name,
        ["InvalidRoleName"] = ApplicationErrorType.invalid_role_name,
        ["PasswordMismatch"] = ApplicationErrorType.password_mismatch,
        ["PasswordRequiresDigit"] = ApplicationErrorType.password_requires_digit,
        ["PasswordRequiresLower"] = ApplicationErrorType.password_requires_lower,
        ["PasswordRequiresNonAlphanumeric"] = ApplicationErrorType.password_requires_non_alphanumeric,
        ["PasswordRequiresUniqueChars"] = ApplicationErrorType.password_requires_unique_chars,
        ["PasswordRequiresUpper"] = ApplicationErrorType.password_requires_upper,
        ["PasswordTooShort"] = ApplicationErrorType.password_too_short,
        ["InvalidToken"] = ApplicationErrorType.invalid_token,
        ["RecoveryCodeRedemptionFailed"] = ApplicationErrorType.recovery_code_redemption_failed,
    };

    private static ApplicationErrorType MapIdentityErrorCode(string errorCode)
    {
        if (IdentityErrorCodeRenameMap.TryGetValue(errorCode, out ApplicationErrorType internalCode)) return internalCode;
        else throw new InvalidOperationException($"Unknown HTTP status code: {errorCode}");
    }

    public static ApplicationResult Map(this IdentityResult identityResult)
    {
        if (identityResult.Succeeded) return ApplicationResult.Success();
        
        IEnumerable<ApplicationErrorType> errorCodes = identityResult.Errors.Select(error => MapIdentityErrorCode(error.Code));

        return ApplicationResult.Failure(errorCodes);
    }
}