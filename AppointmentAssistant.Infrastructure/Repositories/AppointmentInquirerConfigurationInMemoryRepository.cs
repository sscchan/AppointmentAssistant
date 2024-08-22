using AppointmentAssistant.Application.Interfaces;
using AppointmentAssistant.Infrastructure.Data;

namespace AppointmentAssistant.Infrastructure.Repositories
{
    public class AppointmentInquirerConfigurationInMemoryRepository : IAppointmentInquirerConfigurationRepository
    {
        public async Task<IList<AppointmentInquirerConfiguration>> GetAll()
        {
            return AppointmentInquirerConfigurations.data;
        }
    }
}
