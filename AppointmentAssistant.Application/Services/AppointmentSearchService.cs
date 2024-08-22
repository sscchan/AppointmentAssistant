using AppointmentAssistant.Application.Entities;
using AppointmentAssistant.Application.Interfaces;

namespace AppointmentAssistant.Application.Services
{
    public class AppointmentSearchService : IAppointmentSearchService
    {
        private readonly IAppointmentInquirer _appointmentInquirer;
        private readonly IAppointmentInquirerConfigurationRepository _appointmentInquirerConfigurationRepository;

        public AppointmentSearchService(IAppointmentInquirer appointmentInquirer, IAppointmentInquirerConfigurationRepository appointmentInquirerConfigurationRepository)
        {
            _appointmentInquirer = appointmentInquirer;
            _appointmentInquirerConfigurationRepository = appointmentInquirerConfigurationRepository;
        }

        public async Task<IList<Appointment>> GetFirstAppointment(string? service, string? location, string? practitionerName)
        {
            IEnumerable<AppointmentInquirerConfiguration> inquirerConfigurations = await _appointmentInquirerConfigurationRepository.GetAll();

            if (service != null)
            {
                inquirerConfigurations = inquirerConfigurations.Where(c => c.Service == service);
            }

            if (location != null)
            {
                inquirerConfigurations = inquirerConfigurations.Where(c => c.Location == location);
            }

            if (practitionerName != null)
            {
                inquirerConfigurations = inquirerConfigurations.Where(c => c.PractitionerName == practitionerName);
            }

            var nextAvailableAppointments = new List<Appointment>();
            foreach (var inquirerConfiguration in inquirerConfigurations)
            {
                var nextAvailableAppointment = await _appointmentInquirer.GetNextAvailableAppointment(inquirerConfiguration);
                if (nextAvailableAppointment.HasValue)
                {
                    nextAvailableAppointments.Add(
                        new Appointment(
                            practitionerName: inquirerConfiguration.PractitionerName,
                            location: inquirerConfiguration.Location, 
                            service: inquirerConfiguration.Service, 
                            dateTime: nextAvailableAppointment.Value));
                }
            }

            return nextAvailableAppointments;
        }
    }
}
