public class ServiceResult
{
    public bool IsSuccess { get; }
    public IReadOnlyCollection<ApplicationErrorType> Errors { get; }

    protected ServiceResult(bool result, IReadOnlyCollection<ApplicationErrorType> errors)
    {
        IsSuccess = result;
        Errors = errors;
    }

    public static ServiceResult Success() => new(true, []);

    public static ServiceResult Failure(IEnumerable<ApplicationErrorType> errors) => new(false, errors.ToArray());//TO DO: add error count check
}

public class ServiceResult<T> : ServiceResult
{
    public T? Value { get; }

    private ServiceResult(bool result, IReadOnlyCollection<ApplicationErrorType> errors, T? value = default) : base(result, errors)
    {
        Value = value;
    }

    public static ServiceResult<T> Success(T value) => new(true, [], value);
    public static new ServiceResult<T> Failure(IEnumerable<ApplicationErrorType> errors) => new(false, errors.ToArray());
}