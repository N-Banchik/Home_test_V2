using Home_test_V1.Exceptions;
using Home_test_V1.Models;
using Home_test_V1.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Net;

namespace Home_test_V1.Services
{
    /// <summary>
    /// Service for handling errors and generating error responses.
    /// </summary>
    public class ErrorHandlingService : IErrorHandlingService
    {
        /// <summary>
        /// logger instance for logging errors.
        /// </summary>
        private readonly ILogger<ErrorHandlingService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorHandlingService"/> class.
        /// </summary>
        /// <param name="logger">The logger instance for logging errors.</param>
        public ErrorHandlingService(ILogger<ErrorHandlingService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Handles an error and generates an appropriate error response.
        /// </summary>
        /// <param name="ex">The exception that occurred.</param>
        /// <returns>An <see cref="ErrorResponse"/> representing the error.</returns>
        public ErrorResponse HandleError(Exception ex)
        {
            _logger.LogError(ex, "An error occurred: {Message}", ex.Message);

            return ex switch
            {
                DivideByZeroException => new ErrorResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Division by zero is not allowed.",
                    Details = ex.Message
                },
                MathOpException => new ErrorResponse
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Invalid math operation.",
                    Details = ex.Message
                },
                _ => new ErrorResponse
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError,
                    Message = "A General error occurred.",
                    Details = ex.Message
                }
            };
        }
    }
}
