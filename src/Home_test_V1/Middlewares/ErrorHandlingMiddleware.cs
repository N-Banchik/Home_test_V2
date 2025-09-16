using Home_test_V1.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Home_test_V1.Middlewares
{
    /// <summary>
    /// Middleware for handling exceptions and returning standardized error responses.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IErrorHandlingService _errorHandlingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlingMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="errorHandlingService">The service used to handle errors and generate error responses.</param>
        public ErrorHandlingMiddleware(RequestDelegate next, IErrorHandlingService errorHandlingService)
        {
            _next = next;
            _errorHandlingService = errorHandlingService;
        }

        /// <summary>
        /// Invokes the middleware to handle exceptions and write error responses.
        /// </summary>
        /// <param name="context">The current HTTP context.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var errorResponse = _errorHandlingService.HandleError(ex);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = errorResponse.StatusCode;

                var json = JsonSerializer.Serialize(new
                {
                    errorResponse.Message,
                    errorResponse.Details
                });

                await context.Response.WriteAsync(json);
            }
        }
    }
}
