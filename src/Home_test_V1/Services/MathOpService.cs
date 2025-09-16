using Home_test_V1.Exceptions;
using Home_test_V1.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Home_test_V1.Services
{
    /// <summary>
    /// Service for performing basic mathematical operations.
    /// </summary>
    public class MathOpService : IMathOpService

    {
        /// <summary>
        /// logger instance for logging operations and errors.
        /// </summary>
        private readonly ILogger<MathOpService> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MathOpService"/> class.
        /// </summary>
        /// <param name="logger">The logger instance for logging operations and errors.</param>
        public MathOpService(ILogger<MathOpService> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Calculates the result of a mathematical operation of two numbers.
        /// </summary>
        /// <param name="num1s">The first number as a string.</param>
        /// <param name="num2s">The second number as a string.</param>
        /// <param name="operation">The mathematical operation to perform.</param>
        /// <returns>The result of the calculation.</returns>
        async Task<double> IMathOpService.Calculate(string num1s, string num2s, string operation)
        {
            try
            {
                if (!double.TryParse(num1s, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var num1) ||
               !double.TryParse(num2s, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out var num2) || double.IsNaN(num1) || double.IsNaN(num2) ||
                 double.IsInfinity(num1) || double.IsInfinity(num2))
                {
                    throw new ArgumentException("Invalid number input. Supports standard and E notation (e.g., 4E2).");
                }


                _logger.LogInformation("Performing {Operation} on {Num1} and {Num2}", operation, num1, num2);

                // Offload calculation to a background thread
                return await Task.Run(() =>
                {
                    return operation.ToLower() switch
                    {
                        "add" or "+" => num1 + num2,
                        "subtract" or "-" => num1 - num2,
                        "multiply" or "*" => num1 * num2,
                        "divide" or "/" => num2 != 0 ? num1 / num2 : throw new DivideByZeroException(),
                        _ => throw new MathOpException("Invalid operation. Allowed: add (+), subtract (-), multiply (*), divide (/).")
                    };
                }).ConfigureAwait(false);
            }
            catch (Exception e)
            {

                throw new MathOpException(e.Message);
            }
        }
    }
}
