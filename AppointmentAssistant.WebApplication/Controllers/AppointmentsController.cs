using Microsoft.AspNetCore.Mvc;

namespace AppointmentAssistant.WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly ILogger<AppointmentsController> _logger;

        public AppointmentsController(ILogger<AppointmentsController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<String> GetAppointments()
        {
            return new string[] { "dummy stub" };
        }
    }
}
