using AppointmentAssistant.Application.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentAssistant.Application.Services
{
    public interface IAppointmentSearchService
    {
        public Task<IList<Appointment>> GetFirstAppointment(string? service, string? location, string? practitionerName);
    }
}
