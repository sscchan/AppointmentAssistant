using AppointmentAssistant.Application.Services;
using AppointmentAssistant.WebApplication.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentAssistant.WebApplication.Controllers
{
    [ApiController]
    [Route("Appointments")]
    public class AppointmentsController : ControllerBase
    {
        private readonly ILogger<AppointmentsController> _logger;
        private readonly IAppointmentSearchService _appointmentSearchService;

        private readonly string RESPONSE_TIME_ZONE = "Australia/Adelaide";

        public AppointmentsController(ILogger<AppointmentsController> logger, IAppointmentSearchService appointmentSearchService)
        {
            _logger = logger;
            _appointmentSearchService = appointmentSearchService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IList<GetAppointmentsResponseDtoElement>> GetAppointments([FromQuery] string? service, [FromQuery] string? location, [FromQuery] string? practitionerName)
        {
            var appointments = await _appointmentSearchService.GetFirstAppointment(service, location, practitionerName);

            return appointments
                .Select(a => new GetAppointmentsResponseDtoElement(
                   practitionerName: a.PractitionerName,
                   location: a.Location,
                   service: a.Service,
                   dateTime: TimeZoneInfo.ConvertTimeBySystemTimeZoneId(a.DateTime, RESPONSE_TIME_ZONE).ToString("yyyy-MM-dd HH:mm zzz")))
                .ToList();
        }
    }
}
