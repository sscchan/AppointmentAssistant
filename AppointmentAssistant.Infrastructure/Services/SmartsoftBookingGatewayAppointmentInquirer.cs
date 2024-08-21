using AppointmentAssistant.Application.Interfaces;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentAssistant.Infrastructure.Services
{
    public class SmartsoftBookingGatewayAppointmentInquirer : IAppointmentInquirer
    {
        public async Task<DateTime> GetNextAvailableAppointment()
        {
            using (var playwright = await Playwright.CreateAsync())
            await using (var browser = await playwright.Chromium.LaunchAsync(new()
            {
                Headless = false,
                SlowMo = 50
            }))
            {
                var page = await browser.NewPageAsync();
                await page.GotoAsync("https://playwright.dev/dotnet");
                await page.ScreenshotAsync(new()
                {
                    Path = "screenshot.png"
                });
            } 


            return DateTime.UtcNow;
        }
    }
}
