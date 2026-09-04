public class ServiceResult
{
    public bool IsSuccess { get; }
    public IReadOnlyCollection<string> Errors { get; }

    protected ServiceResult(bool result, IReadOnlyCollection<string> errors)
    {
        IsSuccess = result;
        Errors = errors;
    }

    public static ServiceResult Success() => new(true, []);

    public static ServiceResult Failure(IEnumerable<string> errors) => new(false, errors.ToArray());
}

public class ServiceResult<T> : ServiceResult
{
    public T Value { get; }

    private ServiceResult(T value, bool result, IReadOnlyCollection<string> errors): base(result, errors)
    {
        Value = value;
    }

    public static ServiceResult<T> Success(T value) => new(value, true, []);
}