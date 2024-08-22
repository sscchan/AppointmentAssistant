using AppointmentAssistant.Application.Interfaces;
using AppointmentAssistant.Infrastructure.Services;
using FakeItEasy;


namespace AppointmentAssistant.Tests.Unit.Infrastructure.Services
{
    [TestClass]
    public class SmartsoftBookingGatewayAppointmentInquirerTests
    {
        private TimeProvider _mockTimeProvider;
        private IAppointmentInquirer _smartsoftBookingGatewayAppointmentInquirer;

        [TestInitialize]
        public  void Initialize()
        {
            _mockTimeProvider = A.Fake<TimeProvider>();
            _smartsoftBookingGatewayAppointmentInquirer = new SmartsoftBookingGatewayAppointmentInquirer(_mockTimeProvider);

            A.CallTo(() => _mockTimeProvider.GetUtcNow()).Returns(DateTime.UtcNow);
        }

        [DataTestMethod]
        [DataRow("Tuesday 10 Sep 7:10 AM", "Australia/Adelaide", 2024, 9, 9, 21, 40)]
        [DataRow("Friday 3 Jan 9:45 PM", "Australia/Adelaide", 2025, 1, 3, 11, 15)]
        public void ToDateTime(string testDateTimeString, string bookingSystemTimeZoneId, int expectedUtcYear, int expectedUtcMonth, int expectedUtcDay, int expectedUtcHour, int expectedUtcMinute)
        {
            //// Arrange
            var bookingSystemTimeZone = TimeZoneInfo.FindSystemTimeZoneById(bookingSystemTimeZoneId);
            
            // Set current DateTime as 2024-08-22 in the same time zone as the booking system
            A.CallTo(() => _mockTimeProvider.GetUtcNow()).Returns(
                DateTime.SpecifyKind(new DateTime(2024, 08, 22), DateTimeKind.Utc)
                .Add(bookingSystemTimeZone.BaseUtcOffset));

            // Act
            var actualDateTime = ((SmartsoftBookingGatewayAppointmentInquirer) _smartsoftBookingGatewayAppointmentInquirer)
                .ToDateTimeInFuture(testDateTimeString, bookingSystemTimeZoneId);

            // Assert
            Assert.AreEqual(expectedUtcYear, actualDateTime.Year);
            Assert.AreEqual(expectedUtcMonth, actualDateTime.Month);
            Assert.AreEqual(expectedUtcDay, actualDateTime.Day);
            Assert.AreEqual(expectedUtcHour, actualDateTime.Hour);
            Assert.AreEqual(expectedUtcMinute, actualDateTime.Minute);

        }
    }
}
