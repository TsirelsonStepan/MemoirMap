namespace MemoirMap.Controllers;

static class ErrorToHttpStatusCodeMapper
{
    private static readonly IReadOnlyDictionary<string, int> ErrorCodesMap = new Dictionary<string, int>()
    {
        // these are redundant, because 500 error is thrown as an exception rather then returned, so it doesn't require a mapping
        //["DefaultError"] = StatusCodes.Status500InternalServerError,
        //["UserLockoutNotEnabled"] = StatusCodes.Status500InternalServerError,

        ["UserNotInRole"] = StatusCodes.Status403Forbidden,

        ["DuplicateEmail"] = StatusCodes.Status409Conflict,
        ["DuplicateUserName"] = StatusCodes.Status409Conflict,
        ["DuplicateRoleName"] = StatusCodes.Status409Conflict,
        ["LoginAlreadyAssociated"] = StatusCodes.Status409Conflict,
        ["UserAlreadyHasPassword"] = StatusCodes.Status409Conflict,
        ["UserAlreadyInRole"] = StatusCodes.Status409Conflict,
        ["ConcurrencyFailure"] = StatusCodes.Status409Conflict,

        ["InvalidEmail"] = StatusCodes.Status400BadRequest,
        ["InvalidUserName"] = StatusCodes.Status400BadRequest,
        ["InvalidRoleName"] = StatusCodes.Status400BadRequest,
        ["PasswordMismatch"] = StatusCodes.Status400BadRequest,
        ["PasswordRequiresDigit"] = StatusCodes.Status400BadRequest,
        ["PasswordRequiresLower"] = StatusCodes.Status400BadRequest,
        ["PasswordRequiresNonAlphanumeric"] = StatusCodes.Status400BadRequest,
        ["PasswordRequiresUniqueChars"] = StatusCodes.Status400BadRequest,
        ["PasswordRequiresUpper"] = StatusCodes.Status400BadRequest,
        ["PasswordTooShort"] = StatusCodes.Status400BadRequest,
        ["InvalidToken"] = StatusCodes.Status400BadRequest,
        ["RecoveryCodeRedemptionFailed"] = StatusCodes.Status400BadRequest,
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