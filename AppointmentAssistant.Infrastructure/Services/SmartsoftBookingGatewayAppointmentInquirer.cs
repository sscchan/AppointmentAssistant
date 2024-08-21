using AppointmentAssistant.Application.Interfaces;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentAssistant.Infrastructure.Services
{
    public class SmartsoftBookingGatewayAppointmentInquirer : IAppointmentInquirer
    {
        public async Task<DateTime?> GetNextAvailableAppointment()
        {
            using (var playwright = await Playwright.CreateAsync())
            await using (var browser = await playwright.Chromium.LaunchAsync(new()
            {
                Headless = false,
                SlowMo = 50
            }))
            {
                var bookingPageUrl = "https://myphysiomyhealth.appointment.mobi/BookingGateway/Account/LogOn?ReturnUrl=%2fBookingGateway%2f";

                var page = await browser.NewPageAsync();

                await page.GotoAsync(bookingPageUrl);

                // Existing client question
                await page.GetByLabel("Yes, I am an existing client").ClickAsync();
                // Location
                await page.GetByLabel("My Physio My Health - Holden Hill").ClickAsync();
                // Health Service
                await page.GetByLabel("Massage Therapy").ClickAsync();
                // Practitioner
                await page.GetByLabel("Natalia Pinto MT").ClickAsync();
                // Appointment Type
                await page.GetByLabel("Massage 60 mins").ClickAsync();

                // Click "Next" Button
                await page.ClickAsync("input#NextButton");

                // Wait until the "loading" animated element is visible.
                await page.Locator("label.AjaxLoading").WaitForAsync();

                // Wait until the "loading" animated elements is no longer visible. (Loading is complete)
                await page.Locator("label.AjaxLoading").WaitForAsync(new LocatorWaitForOptions() { State = WaitForSelectorState.Hidden });

                // Wait until the available appointment time elements are visible.
                // I have noted the typo on the div class
                try
                {
                    await page.Locator("div.SeletedAppointmentsFound").WaitForAsync(new LocatorWaitForOptions() { Timeout = 30000 });

                    var firstAppointmentDate = (await page.Locator("div.SeletedAppointmentsFound > label > div.AppTableDate").First.InnerTextAsync()).Replace("\n", " ");
                    var firstAppointmentTime = (await page.Locator("div.SeletedAppointmentsFound > label > div.AppTableTime").First.InnerTextAsync());
                    var firstAppointmentDateAndTime = $"{firstAppointmentDate} {firstAppointmentTime}";
                }
                catch (TimeoutException e)
                {
                    // The text stating no appointment is available exists
                    if (await page.Locator("div#NoAppsAvailable").CountAsync() > 0)
                    {
                        return null;
                    }
                    else throw new ExternalException("Appointment query failed.");
                }
            } 

            return DateTime.UtcNow;
        }
    }
}
