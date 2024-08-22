using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentAssistant.Application.Interfaces
{
    public interface IAppointmentInquirerConfigurationRepository
    {
        public Task<IList<AppointmentInquirerConfiguration>> GetAll();
    }
}
