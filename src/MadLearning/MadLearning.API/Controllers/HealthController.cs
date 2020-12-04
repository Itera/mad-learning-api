using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MadLearning.API.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public sealed class HealthController : ApiControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok();
        }
    }
}
