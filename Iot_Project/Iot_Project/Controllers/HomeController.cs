using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Iot_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Welcome to the IoT Project API!");
        }
    }
}
