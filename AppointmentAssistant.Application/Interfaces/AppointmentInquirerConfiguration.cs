using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentAssistant.Application.Interfaces
{
    public class AppointmentInquirerConfiguration
    {
        public string PractitionerName { get; }
        public string Location { get; }
        public string Service { get;  }
        public IDictionary<string, string> InquiryContext { get; }

        public AppointmentInquirerConfiguration(string practitionerName, string location, string service, IDictionary<string, string> inquiryContext)
        {
            PractitionerName = practitionerName;
            Location = location;
            Service = service;
            InquiryContext = inquiryContext ?? new Dictionary<string, string>();
        }
    }
}
