namespace MemoirMap.Controllers;

static class ErrorToHttpStatusCodeMapper
{
    private static readonly IReadOnlyDictionary<string, int> ErrorCodesMap = new Dictionary<string, int>()
    {
        // these are redundant, because 500 error is thrown as an exception rather then returned, so it doesn't require a mapping
        //["default_error"] = StatusCodes.Status500InternalServerError,
        //["user_lockout_not_enabled"] = StatusCodes.Status500InternalServerError,

        ["user_not_in_role"] = StatusCodes.Status403Forbidden,

        ["duplicate_email"] = StatusCodes.Status409Conflict,
        ["duplicate_user_name"] = StatusCodes.Status409Conflict,
        ["duplicate_role_name"] = StatusCodes.Status409Conflict,
        ["login_already_associated"] = StatusCodes.Status409Conflict,
        ["user_already_has_password"] = StatusCodes.Status409Conflict,
        ["user_already_in_role"] = StatusCodes.Status409Conflict,
        ["concurrency_failure"] = StatusCodes.Status409Conflict,

        ["invalid_email"] = StatusCodes.Status400BadRequest,
        ["invalid_user_name"] = StatusCodes.Status400BadRequest,
        ["invalid_role_name"] = StatusCodes.Status400BadRequest,
        ["password_mismatch"] = StatusCodes.Status400BadRequest,
        ["password_requires_digit"] = StatusCodes.Status400BadRequest,
        ["password_requires_lower"] = StatusCodes.Status400BadRequest,
        ["password_requires_non_alphanumeric"] = StatusCodes.Status400BadRequest,
        ["password_requires_unique_chars"] = StatusCodes.Status400BadRequest,
        ["password_requires_upper"] = StatusCodes.Status400BadRequest,
        ["password_too_short"] = StatusCodes.Status400BadRequest,
        ["invalid_token"] = StatusCodes.Status400BadRequest,
        ["recovery_code_redemption_failed"] = StatusCodes.Status400BadRequest,
    };

    private static readonly int[] HttpStatusCodesPriority =
    [
        StatusCodes.Status403Forbidden,
        StatusCodes.Status409Conflict,
        StatusCodes.Status400BadRequest,
    ];

    private static int MapErrorCode(string errorCode)
    {
        if (ErrorCodesMap.TryGetValue(errorCode, out int httpStatusCode)) return httpStatusCode;
        else throw new InvalidOperationException($"Unknown HTTP status code: {errorCode}");
    }

    public static int HttpStatusCodeFromErrorCodes(IEnumerable<string> errorCodes)
    {
        IEnumerable<int> httpStatusCodes = errorCodes.Select(MapErrorCode).Distinct();

        foreach (int httpStatusCode in HttpStatusCodesPriority)
            if (httpStatusCodes.Contains(httpStatusCode)) return httpStatusCode;
        
        throw new InvalidOperationException($"Unknown HTTP status code(s): {string.Join(", ", httpStatusCodes)}");
    }
}