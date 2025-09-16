namespace Home_test_V1.Models.Interfaces
{
    /// <summary>
    /// Represents a request for a mathematical operation containing two numbers as strings.
    /// </summary>
    public interface IMathOpRequest
    {
        /// <summary>
        /// Gets or sets the first number as a string.
        /// </summary>
        string Number1 { get; set; }

        /// <summary>
        /// Gets or sets the second number as a string.
        /// </summary>
        string Number2 { get; set; }
    }
}