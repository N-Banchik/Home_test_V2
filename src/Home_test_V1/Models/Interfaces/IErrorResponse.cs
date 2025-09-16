namespace Home_test_V1.Models.Interfaces
{
    /// <summary>
    /// Represents an error response with status code, message, and optional details.
    /// </summary>
    public interface IErrorResponse
    {
        /// <summary>
        /// Gets or sets the HTTP status code associated with the error.
        /// </summary>
        int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        string Message { get; set; }

        /// <summary>
        /// Gets or sets additional details about the error.
        /// </summary>
        string? Details { get; set; }
    }
}
