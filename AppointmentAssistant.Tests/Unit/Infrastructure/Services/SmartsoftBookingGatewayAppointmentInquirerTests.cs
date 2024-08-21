using AppointmentAssistant.Application.Interfaces;
using AppointmentAssistant.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentAssistant.Tests.Unit.Infrastructure.Services
{
    [TestClass]
    public class SmartsoftBookingGatewayAppointmentInquirerTests
    {
        private IAppointmentInquirer _smartsoftBookingGatewayAppointmentInquirer;

        [TestInitialize]
        public  void Initialize()
        {
            _smartsoftBookingGatewayAppointmentInquirer = new SmartsoftBookingGatewayAppointmentInquirer();
        }

        [TestMethod]
        public async Task GetNextAvailableAppointment()
        {
            await _smartsoftBookingGatewayAppointmentInquirer.GetNextAvailableAppointment();
        }
    }
}
