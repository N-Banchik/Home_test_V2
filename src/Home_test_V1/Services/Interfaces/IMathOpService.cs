using System.Threading.Tasks;

namespace Home_test_V1.Services.Interfaces
{
    /// <summary>
    /// Interface for a service that performs mathematical operations.
    /// </summary>
    public interface IMathOpService
    {
        /// <summary>
        /// Calculates the result of a mathematical operation.
        /// </summary>
        /// <param name="num1s">The first number as a string.</param>
        /// <param name="num2s">The second number as a string.</param>
        /// <param name="operation">The mathematical operation to perform.</param>
        /// <returns>The result of the calculation.</returns>
        public Task<double> Calculate(string num1s, string num2s, string operation);

    }
}
