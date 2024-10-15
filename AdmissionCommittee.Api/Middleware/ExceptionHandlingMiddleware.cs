namespace AdmissionCommittee.Api.Middleware;

/// <summary>
/// Middleware for handling exceptions during request processing.
/// Catches exceptions and returns an appropriate HTTP status code and message.
/// </summary>
public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    /// <summary>
    /// Invokes the next middleware in the pipeline and handles any exceptions that occur.
    /// </summary>
    /// <param name="httpContext">The HTTP context of the current request.</param>
    /// <returns>A task that represents the completion of the request.</returns>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong: {ex.Message}");
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    /// <summary>
    /// Handles the exception by logging it and returning a proper HTTP response.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <param name="ex">The exception that occurred.</param>
    /// <returns>A task representing the HTTP response writing process.</returns>
    private Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        _logger.LogError($"Handling exception: {ex.GetType().Name} - {ex.Message}");
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = ex switch
        {
            InvalidOperationException => StatusCodes.Status400BadRequest,
            ArgumentException => StatusCodes.Status400BadRequest,
            KeyNotFoundException => StatusCodes.Status404NotFound,
            _ => StatusCodes.Status500InternalServerError
        };

        var response = new ErrorDetails
        {
            StatusCode = context.Response.StatusCode,
            Message = ex.Message
        };
        return context.Response.WriteAsync(response.ToString());
    }
}
