using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EuPagoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeepAliveController : ControllerBase
    {

        // GET api/<KeepAliveController>
        [HttpGet]
        public string Get()
        {
            return "Keep Alive!";
        }

    }
}
