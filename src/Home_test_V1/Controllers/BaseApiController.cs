using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Home_test_V1.Controllers
{
    /// <summary>
    /// Serves as the base class for all API controllers in the application.
    /// Applies common attributes such as <see cref="ApiControllerAttribute"/>, routing, and authorization.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BaseApiController : ControllerBase
    {

    }
}
