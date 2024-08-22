using AppointmentAssistant.Application.Interfaces;

namespace AppointmentAssistant.Infrastructure.Data
{
    public static class AppointmentInquirerConfigurations
    {
        public static readonly IList<AppointmentInquirerConfiguration> data = new List<AppointmentInquirerConfiguration> ()
        {
            new AppointmentInquirerConfiguration(
                practitionerName: "Ben Smith",
                location: "Holden Hill",
                service: "Physiotherapy",
                inquiryContext: new Dictionary<string, string> ()
                {
                    { "BookingGatewayUrl", "https://myphysiomyhealth.appointment.mobi/BookingGateway" },
                    { "Location", "My Physio My Health - Holden Hill" },
                    { "Service", "Physiotherapy" },
                    { "Practitioner", "Ben Smith" },
                    { "AppointmentType", "Standard Consult" },
                    { "AppointmentTimeZone", "Australia/Adelaide" }
                }),

            new AppointmentInquirerConfiguration(
                practitionerName: "Natalia Pinto",
                location: "Holden Hill",
                service: "Massage Therapy",
                inquiryContext: new Dictionary<string, string> ()
                {
                    { "BookingGatewayUrl", "https://myphysiomyhealth.appointment.mobi/BookingGateway" },
                    { "Location", "My Physio My Health - Holden Hill" },
                    { "Service", "Massage Therapy" },
                    { "Practitioner", "Natalia Pinto MT" },
                    { "AppointmentType", "Massage 30 mins" },
                    { "AppointmentTimeZone", "Australia/Adelaide" }
                }),

            new AppointmentInquirerConfiguration(
                practitionerName: "Natalia Pinto",
                location: "Lightsview",
                service: "Massage Therapy",
                inquiryContext: new Dictionary<string, string> ()
                {
                    { "BookingGatewayUrl", "https://myphysiomyhealth-lightsview.appointment.mobi:8443/BookingGateway/" },
                    { "Location", "Lightsview" },
                    { "Service", "Massage Therapy" },
                    { "Practitioner", "Natalia Pinto MT" },
                    { "AppointmentType", "Standard Consult" },
                    { "AppointmentTimeZone", "Australia/Adelaide" }
                }),

            new AppointmentInquirerConfiguration(
                practitionerName: "Rodrigo Gouveia",
                location: "Holden Hill",
                service: "Massage Therapy",
                inquiryContext: new Dictionary<string, string> ()
                {
                    { "BookingGatewayUrl", "https://myphysiomyhealth.appointment.mobi/BookingGateway" },
                    { "Location", "My Physio My Health - Holden Hill" },
                    { "Service", "Massage Therapy" },
                    { "Practitioner", "Rodrigo Gouveia MT" },
                    { "AppointmentType", "Massage 30 mins" },
                    { "AppointmentTimeZone", "Australia/Adelaide" }
                }),
        };
    }
}
