using Home_test_V1.Exceptions;
using Home_test_V1.Models;
using Home_test_V1.Models.Interfaces;
using Home_test_V1.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Home_test_V1.Controllers
{
    /// <summary>
    /// Controller for performing arithmetic operations via a POST request.
    /// Inherits from BaseApiController, which enforces JWT authorization.
    /// </summary>
    /// <description>Calculates result of two numbers based on the operation specified in the X-Operation header. Requires JWT authorization. Numbers are provided as strings and parsed to doubles (supports standard and E-notation, e.g., '4E2').</description>
    public class CalculateController : BaseApiController
    {
        private readonly IMathOpService _mathOpService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CalculateController"/> class.
        /// </summary>
        /// <param name="mathOpService">The service for performing arithmetic operations.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="mathOpService"/> is null.</exception>
        public CalculateController(IMathOpService mathOpService)
        {
            _mathOpService = mathOpService ?? throw new ArgumentNullException(nameof(mathOpService));
        }

        /// <summary>
        /// Performs an arithmetic operation on two numbers provided in a form-data request.
        /// The operation is specified in the X-Operation header.
        /// </summary>
        /// <param name="request">The request containing the two numbers to operate on.</param>
        /// <returns>An IActionResult containing the result of the operation or an error response.</returns>
        /// <exception cref="MathOpException">Thrown when the X-Operation header is missing or an error occurs during calculation.</exception>
        /// <response code="200">Returns the result of the arithmetic operation.</response>
        /// <response code="400">Returned when the input is invalid, the operation is unsupported, or division by zero occurs.</response>
        /// <response code="401">Returned when JWT authorization fails.</response>
        [HttpPost]
        public async Task<IActionResult> Calculate([FromForm] MathOpRequest request)
        {
            if (!Request.Headers.TryGetValue("X-Operation", out var operation))
            {
                throw new MathOpException("X-Operation header is required.");
            }
            try
            {
                var result = await _mathOpService.Calculate(request.Number1, request.Number2, operation!);
                return Ok(new CalculationResponse { Result = result });
            }
            catch (Exception ex)
            {
                throw new MathOpException(ex.Message);
            }
        }
    }
}
