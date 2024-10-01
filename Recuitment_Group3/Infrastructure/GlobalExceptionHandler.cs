using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Recuitment_Group3.Infrastructure
{
    public class GlobalExceptionHandler(

        ILogger<GlobalExceptionHandler> logger

        ) : IExceptionHandler
    {

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
                                              Exception exception,
                                              CancellationToken cancellationToken)
        {
            logger.LogError(exception, "An unhandled exception has occurred.");

            var problemDetails = new ProblemDetails
            {
                Title = "An error occurred",
                Status = StatusCodes.Status500InternalServerError,
                Detail = exception.Message
            };

            return true;
        }
    }
}
