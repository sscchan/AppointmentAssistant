using AppointmentAssistant.Application.Interfaces;
using AppointmentAssistant.Infrastructure.Data;
using AppointmentAssistant.Infrastructure.Services;
using FakeItEasy;


namespace AppointmentAssistant.Tests.Integration.Infrastructure.Services
{
    [TestClass]
    public class SmartsoftBookingGatewayAppointmentInquirerTests
    {
        private IAppointmentInquirer _smartsoftBookingGatewayAppointmentInquirer;

        [TestInitialize]
        public  void Initialize()
        {
            _smartsoftBookingGatewayAppointmentInquirer = new SmartsoftBookingGatewayAppointmentInquirer(TimeProvider.System);
        }


        public static IEnumerable<object[]> GetNextAvailableAppointmentTestData()
        {
            foreach (var appointmentInquirerConfiguration in AppointmentInquirerConfigurations.data)
            {
                yield return new object[] { appointmentInquirerConfiguration };
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(GetNextAvailableAppointmentTestData), DynamicDataSourceType.Method)]
        public async Task GetNextAvailableAppointmentInHoldenHill(AppointmentInquirerConfiguration testApointmentInquirerConfiguration)
        {
            // Act & Assert doesn't throw exceptions
            await _smartsoftBookingGatewayAppointmentInquirer.GetNextAvailableAppointment(testApointmentInquirerConfiguration);
        }
    }
}
