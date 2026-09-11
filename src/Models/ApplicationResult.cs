public class ApplicationResult
{
    public bool IsSuccess { get; }
    public IReadOnlyCollection<ApplicationErrorType> Errors { get; }

    protected ApplicationResult(bool result, IReadOnlyCollection<ApplicationErrorType> errors)
    {
        IsSuccess = result;
        Errors = errors;
    }

    public static ApplicationResult Success() => new(true, []);

    public static ApplicationResult Failure(IEnumerable<ApplicationErrorType> errors) => new(false, errors.ToArray());//TO DO: add error count check
}

public class ApplicationResult<T> : ApplicationResult
{
    public T? Value { get; }

    private ApplicationResult(bool result, IReadOnlyCollection<ApplicationErrorType> errors, T? value = default) : base(result, errors)
    {
        Value = value;
    }

    public static ApplicationResult<T> Success(T value) => new(true, [], value);
    public static new ApplicationResult<T> Failure(IEnumerable<ApplicationErrorType> errors) => new(false, errors.ToArray());
}