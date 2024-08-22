namespace AppointmentAssistant.Application.Entities
{
    public class Appointment
    {
        public string PractitionerName { get; }
        public string Location { get; }
        public string Service { get; }
        public DateTime DateTime { get; }

        public Appointment(string practitionerName, string location, string service, DateTime dateTime)
        {
            PractitionerName = practitionerName;
            Location = location;
            Service = service;
            DateTime = dateTime;
        }
    }
}
