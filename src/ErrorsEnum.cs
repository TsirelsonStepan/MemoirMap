public enum ApplicationErrorType
{
    user_not_in_role,

    duplicate_email,
    duplicate_user_name,
    duplicate_role_name,
    login_already_associated,
    user_already_has_password,
    user_already_in_role,
    concurrency_failure,

    invalid_email,
    invalid_user_name,
    invalid_role_name,
    password_mismatch,
    password_requires_digit,
    password_requires_lower,
    password_requires_non_alphanumeric,
    password_requires_unique_chars,
    password_requires_upper,
    password_too_short,
    invalid_token,
    recovery_code_redemption_failed,
    
    user_locked_out,
    user_not_allowed,
    two_factor_required,
    invalid_credentials,
}