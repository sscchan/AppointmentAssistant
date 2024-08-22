using Microsoft.AspNetCore.Mvc;

namespace AppointmentAssistant.WebApplication.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        /// <summary>
        /// The "home" endpoint that redirects to the swagger UI endpoint.
        /// </summary>
        /// <remarks>This will be used until / if an proper frontend is built.</remarks>
        [Route("")]
        [HttpGet]
        public IActionResult Index()
        {
            return Redirect("./swagger");
        }
    }
}
