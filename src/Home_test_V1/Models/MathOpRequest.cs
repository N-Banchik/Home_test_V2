using Home_test_V1.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Home_test_V1.Models.Interfaces
{
    /// <summary>
    /// Represents a request for a mathematical operation.
    /// </summary>
    public class MathOpRequest:IMathOpRequest
    {
        /// <summary>
        /// The first number for the operation.
        /// </summary>
        public required string Number1 { get; set; }

        /// <summary>
        /// The second number for the operation.
        /// </summary>
        public required string Number2 { get; set; }
    }
}
