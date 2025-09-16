using System;
using System.Threading.Tasks;
using FluentAssertions;
using Home_test_V1.Exceptions;
using Home_test_V1.Services;
using Home_test_V1.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Home_test_V1.Tests
{
    /// <summary>
    /// Unit tests for MathOpService, covering arithmetic operations and error handling.
    /// </summary>
    public class MathOpServiceTests
    {
        private readonly IMathOpService _service;
        private readonly Mock<ILogger<MathOpService>> _loggerMock;

        public MathOpServiceTests()
        {
            _loggerMock = new Mock<ILogger<MathOpService>>();
            _service = new MathOpService(_loggerMock.Object);
        }

        /// <summary>
        /// Tests successful addition with standard numbers.
        /// </summary>
        [Fact]
        public async Task Calculate_Addition_ReturnsCorrectResult()
        {
            // Arrange
            string num1 = "10.5";
            string num2 = "5.2";
            string operation = "add";

            // Act
            double result = await _service.Calculate(num1, num2, operation);

            // Assert
            result.Should().BeApproximately(15.7, 0.0001);
        }

        /// <summary>
        /// Tests successful addition using symbol (+).
        /// </summary>
        [Fact]
        public async Task Calculate_AdditionSymbol_ReturnsCorrectResult()
        {
            // Arrange
            string num1 = "10.5";
            string num2 = "5.2";
            string operation = "+";

            // Act
            double result = await _service.Calculate(num1, num2, operation);

            // Assert
            result.Should().BeApproximately(15.7, 0.0001);
        }

        /// <summary>
        /// Tests successful subtraction with standard numbers.
        /// </summary>
        [Fact]
        public async Task Calculate_Subtraction_ReturnsCorrectResult()
        {
            // Arrange
            string num1 = "10.5";
            string num2 = "5.2";
            string operation = "subtract";

            // Act
            double result = await _service.Calculate(num1, num2, operation);

            // Assert
            result.Should().BeApproximately(5.3, 0.0001);
        }

        /// <summary>
        /// Tests successful multiplication with standard numbers.
        /// </summary>
        [Fact]
        public async Task Calculate_Multiplication_ReturnsCorrectResult()
        {
            // Arrange
            string num1 = "10.5";
            string num2 = "5";
            string operation = "multiply";

            // Act
            double result = await _service.Calculate(num1, num2, operation);

            // Assert
            result.Should().BeApproximately(52.5, 0.0001);
        }

        /// <summary>
        /// Tests successful division with standard numbers.
        /// </summary>
        [Fact]
        public async Task Calculate_Division_ReturnsCorrectResult()
        {
            // Arrange
            string num1 = "10.5";
            string num2 = "2";
            string operation = "divide";

            // Act
            double result = await _service.Calculate(num1, num2, operation);

            // Assert
            result.Should().BeApproximately(5.25, 0.0001);
        }

        /// <summary>
        /// Tests E-notation input parsing for addition.
        /// </summary>
        [Fact]
        public async Task Calculate_ENotationInput_ReturnsCorrectResult()
        {
            // Arrange
            string num1 = "4E2"; // 400
            string num2 = "1.5E-1"; // 0.15
            string operation = "add";

            // Act
            double result = await _service.Calculate(num1, num2, operation);

            // Assert
            result.Should().BeApproximately(400.15, 0.0001);
        }

        /// <summary>
        /// Tests invalid number input throws MathOpException.
        /// </summary>
        [Fact]
        public async Task Calculate_InvalidNumber_ThrowsMathOpException()
        {
            // Arrange
            string num1 = "abc";
            string num2 = "5.2";
            string operation = "add";

            // Act
            Func<Task> act = async () => await _service.Calculate(num1, num2, operation);

            // Assert
            await act.Should().ThrowAsync<MathOpException>()
                .WithMessage("Invalid number input. Supports standard and E notation (e.g., 4E2).");
        }

        /// <summary>
        /// Tests NaN input throws MathOpException.
        /// </summary>
        [Fact]
        public async Task Calculate_NaNInput_ThrowsMathOpException()
        {
            // Arrange
            string num1 = "NaN";
            string num2 = "5.2";
            string operation = "add";

            // Act
            Func<Task> act = async () => await _service.Calculate(num1, num2, operation);

            // Assert
            await act.Should().ThrowAsync<MathOpException>()
                .WithMessage("Invalid number input. Supports standard and E notation (e.g., 4E2).");
        }

        /// <summary>
        /// Tests Infinity input throws MathOpException.
        /// </summary>
        [Fact]
        public async Task Calculate_InfinityInput_ThrowsMathOpException()
        {
            // Arrange
            string num1 = "Infinity";
            string num2 = "5.2";
            string operation = "add";

            // Act
            Func<Task> act = async () => await _service.Calculate(num1, num2, operation);

            // Assert
            await act.Should().ThrowAsync<MathOpException>()
                .WithMessage("Invalid number input. Supports standard and E notation (e.g., 4E2).");
        }

        /// <summary>
        /// Tests invalid operation throws MathOpException.
        /// </summary>
        [Fact]
        public async Task Calculate_InvalidOperation_ThrowsMathOpException()
        {
            // Arrange
            string num1 = "10.5";
            string num2 = "5.2";
            string operation = "mod";

            // Act
            Func<Task> act = async () => await _service.Calculate(num1, num2, operation);

            // Assert
            await act.Should().ThrowAsync<MathOpException>()
                .WithMessage("Invalid operation. Allowed: add (+), subtract (-), multiply (*), divide (/).");
        }

        /// <summary>
        /// Tests division by zero throws MathOpException.
        /// </summary>
        [Fact]
        public async Task Calculate_DivisionByZero_ThrowsMathOpException()
        {
            // Arrange
            string num1 = "10.5";
            string num2 = "0";
            string operation = "divide";

            // Act
            Func<Task> act = async () => await _service.Calculate(num1, num2, operation);

            // Assert
            await act.Should().ThrowAsync<MathOpException>()
                .WithMessage("Attempted to divide by zero.");
        }
    }
}