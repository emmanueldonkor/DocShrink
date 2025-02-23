using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;

namespace API;


/// <summary>
/// A global exception handler that logs the exception and generates a problem details
/// response with a title that corresponds to the exception type.
/// </summary>
public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> logger;

    /// <summary>
    /// Creates a new instance of <see cref="GlobalExceptionHandler"/>.
    /// </summary>
    /// <param name="logger">The logger to use for logging.</param>
    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        this.logger = logger;
    }

    /// <inheritdoc/>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
        logger.LogError(
            exception,
            "Could not process a request on machine {MachineName}. TraceId: {TraceId}",
            Environment.MachineName, traceId);

        var (statusCode, title) = MapException(exception);

        await Results.Problem(
            title: title,
            statusCode: statusCode,
            extensions: new Dictionary<string, object?>
            {
                { "traceId", traceId }
            }
        ).ExecuteAsync(httpContext);
        return true;
    }

    // Maps the given exception to a status code and title.
    private static (int statusCode, string Title) MapException(Exception exception)
    {
        return exception switch
        {
            ArgumentNullException => (StatusCodes.Status400BadRequest, exception.Message),
            _ => (StatusCodes.Status500InternalServerError, "We are very sorry, we are working on it to fix it immediately")
        };
    }
}

