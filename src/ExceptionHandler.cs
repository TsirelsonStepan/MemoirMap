class ExceptionHandlerMiddleware
{
	private readonly RequestDelegate _next;
	private readonly ILogger<ExceptionHandlerMiddleware> _logger;
	public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
	{
		_logger = logger;
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception e)
		{
			_logger.LogError(e, "Unexpected error: {Message}", e.Message);

			context.Response.StatusCode = StatusCodes.Status500InternalServerError;
		}
	}
}