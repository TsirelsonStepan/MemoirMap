namespace MemoirMap.Controllers;

static class ErrorToHttpStatusCodeMapper
{
    private static readonly IReadOnlyDictionary<ApplicationErrorType, int> ErrorCodesMap = new Dictionary<ApplicationErrorType, int>()
    {
        // these are redundant, because 500 error is thrown as an exception rather then returned, so it doesn't require a mapping
        //[ApplicationErrorType.default_error] = StatusCodes.Status500InternalServerError,
        //[ApplicationErrorType.user_lockout_not_enabled] = StatusCodes.Status500InternalServerError,

        [ApplicationErrorType.user_not_in_role] = StatusCodes.Status403Forbidden,

        [ApplicationErrorType.duplicate_email] = StatusCodes.Status409Conflict,
        [ApplicationErrorType.duplicate_user_name] = StatusCodes.Status409Conflict,
        [ApplicationErrorType.duplicate_role_name] = StatusCodes.Status409Conflict,
        [ApplicationErrorType.login_already_associated] = StatusCodes.Status409Conflict,
        [ApplicationErrorType.user_already_has_password] = StatusCodes.Status409Conflict,
        [ApplicationErrorType.user_already_in_role] = StatusCodes.Status409Conflict,
        [ApplicationErrorType.concurrency_failure] = StatusCodes.Status409Conflict,

        [ApplicationErrorType.invalid_email] = StatusCodes.Status400BadRequest,
        [ApplicationErrorType.invalid_user_name] = StatusCodes.Status400BadRequest,
        [ApplicationErrorType.invalid_role_name] = StatusCodes.Status400BadRequest,
        [ApplicationErrorType.password_mismatch] = StatusCodes.Status400BadRequest,
        [ApplicationErrorType.password_requires_digit] = StatusCodes.Status400BadRequest,
        [ApplicationErrorType.password_requires_lower] = StatusCodes.Status400BadRequest,
        [ApplicationErrorType.password_requires_non_alphanumeric] = StatusCodes.Status400BadRequest,
        [ApplicationErrorType.password_requires_unique_chars] = StatusCodes.Status400BadRequest,
        [ApplicationErrorType.password_requires_upper] = StatusCodes.Status400BadRequest,
        [ApplicationErrorType.password_too_short] = StatusCodes.Status400BadRequest,
        [ApplicationErrorType.invalid_token] = StatusCodes.Status400BadRequest,
        [ApplicationErrorType.recovery_code_redemption_failed] = StatusCodes.Status400BadRequest,
        
        [ApplicationErrorType.invalid_credentials] = StatusCodes.Status401Unauthorized,
        [ApplicationErrorType.two_factor_required] = StatusCodes.Status401Unauthorized,

        [ApplicationErrorType.user_not_allowed] = StatusCodes.Status403Forbidden,

        [ApplicationErrorType.user_locked_out] = StatusCodes.Status423Locked,
    };

    private static readonly int[] HttpStatusCodesPriority =
    [
        StatusCodes.Status403Forbidden,
        StatusCodes.Status409Conflict,
        StatusCodes.Status401Unauthorized,
        StatusCodes.Status423Locked,
        StatusCodes.Status400BadRequest,
    ];

    private static int MapErrorCode(ApplicationErrorType errorCode)
    {
        if (ErrorCodesMap.TryGetValue(errorCode, out int httpStatusCode)) return httpStatusCode;
        else throw new InvalidOperationException($"Unknown HTTP status code: {errorCode}");
    }

    public static int HttpStatusCodeFromErrorCodes(IEnumerable<ApplicationErrorType> errorCodes)
    {
        IEnumerable<int> httpStatusCodes = errorCodes.Select(MapErrorCode).Distinct();

        foreach (int httpStatusCode in HttpStatusCodesPriority)
            if (httpStatusCodes.Contains(httpStatusCode)) return httpStatusCode;
        
        throw new InvalidOperationException($"Unknown HTTP status code(s): {string.Join(", ", httpStatusCodes)}");
    }
}