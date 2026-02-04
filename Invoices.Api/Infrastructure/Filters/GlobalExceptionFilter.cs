using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Invoices.Api.Infrastructure.Filters
{
    /// <summary>
    /// A global exception filter that catches all unhandled exceptions
    /// occurring inside the ASP.NET Core MVC pipeline.
    ///
    /// Responsibilities:
    /// - Logs the exception using the configured ILogger (Azure, console, files).
    /// - Converts the exception into a standardized RFC 7807 ProblemDetails response.
    /// - Prevents the API from returning raw 500 errors without context.
    ///
    /// This filter ensures consistent error responses across the entire API.
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> logger;

        /// <summary>
        /// Creates a new instance of the global exception filter.
        /// </summary>
        /// <param name="logger">Logger used to record unhandled exceptions.</param>
        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// Called automatically when an unhandled exception occurs.
        /// Logs the exception and returns a standardized error response.
        /// </summary>
        /// <param name="context">Context containing exception details.</param>
        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            // Log full exception details (stack trace included)
            logger.LogError(exception, "Unhandled exception occurred.");

            // Map known exception types to specific HTTP status codes
            var (status, title) = exception switch
            {
                KeyNotFoundException => (HttpStatusCode.NotFound, "Resource Not Found"),
                ArgumentException => (HttpStatusCode.BadRequest, "Invalid Request"),
                UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "Unauthorized"),
                _ => (HttpStatusCode.InternalServerError, "Internal Server Error")
            };

            // Create standardized RFC 7807 response
            var problem = new ProblemDetails
            {
                Title = title,
                Detail = exception.Message, 
                Status = (int)status
            };

            context.Result = new ObjectResult(problem)
            {
                StatusCode = problem.Status
            };

            context.ExceptionHandled = true;
        }
    }
}
