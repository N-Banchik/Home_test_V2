using System;

namespace Home_test_V1.Exceptions
{
    /// <summary>
    /// Represents errors that occur during mathematical operations.
    /// </summary>
    public class MathOpException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MathOpException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public MathOpException(string message) : base(message)
        {
        }
    }
}
