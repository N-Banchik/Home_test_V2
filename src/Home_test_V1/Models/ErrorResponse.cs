using Home_test_V1.Models.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace Home_test_V1.Models
{
    /// <summary>
    /// Represents an error response returned by the API.
    /// </summary>
    public class ErrorResponse : IErrorResponse
    {
        /// <summary>
        /// Gets or sets the HTTP status code associated with the error.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets a short message describing the error.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets additional details about the error, if available.
        /// </summary>
        public string Details { get; set; }
    }
}
