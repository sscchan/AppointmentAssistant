namespace AppointmentAssistant.WebApplication.Dtos
{
    public class GetAppointmentsResponseDtoElement
    {
        public string PractitionerName { get; }
        public string Location { get; }
        public string Service { get; }
        public string DateTime { get; }

        public GetAppointmentsResponseDtoElement(string practitionerName, string location, string service, string dateTime)
        {
            PractitionerName = practitionerName;
            Location = location;
            Service = service;
            DateTime = dateTime;
        }
    }
}
