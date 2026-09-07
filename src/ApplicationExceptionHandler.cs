using Microsoft.AspNetCore.Diagnostics;

class ApplicationExceptionHandler : IExceptionHandler
{
	private readonly ILogger<ApplicationExceptionHandler> _logger;
	public ApplicationExceptionHandler(ILogger<ApplicationExceptionHandler> logger)
	{
		_logger = logger;
	}

    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
		_logger.LogError(exception, "Unexpected error: {Message}", exception.Message);
		return ValueTask.FromResult(true);
    }
}