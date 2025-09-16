using Home_test_V1.Models;
using System;

namespace Home_test_V1.Services.Interfaces
{
    /// <summary>
    /// Interface for a service that handles errors.
    /// </summary>
    public interface IErrorHandlingService
    {
        /// <summary>
        /// Handles an error and generates an appropriate error response.
        /// </summary>
        /// <param name="ex">The exception that occurred.</param>
        /// <returns>An <see cref="ErrorResponse"/> representing the error.</returns>
        ErrorResponse HandleError(Exception ex);
    }
}
