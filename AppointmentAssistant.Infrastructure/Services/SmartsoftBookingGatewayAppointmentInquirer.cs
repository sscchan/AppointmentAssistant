using AppointmentAssistant.Application.Interfaces;
using Microsoft.Playwright;
using System.Globalization;
using System.Runtime.InteropServices;

namespace AppointmentAssistant.Infrastructure.Services
{
    public class SmartsoftBookingGatewayAppointmentInquirer : IAppointmentInquirer
    {
        private TimeProvider _timeProvider;

        public SmartsoftBookingGatewayAppointmentInquirer(TimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public async Task<DateTime?> GetNextAvailableAppointment(AppointmentInquirerConfiguration input)
        {
            using (var playwright = await Playwright.CreateAsync())
            await using (var browser = await playwright.Chromium.LaunchAsync(new()
            {
                Headless = true,
                SlowMo = 50
            }))
            {
                var bookingPageUrl = input.InquiryContext["BookingGatewayUrl"];

                var page = await browser.NewPageAsync();

                await page.GotoAsync(bookingPageUrl);

                // Existing client question
                await page.GetByLabel("Yes, I am an existing client").ClickAsync();
                // Location
                await page.GetByLabel(input.InquiryContext["Location"]).ClickAsync();
                // Health Service
                await page.GetByLabel(input.InquiryContext["Service"]).First.ClickAsync();

                // Practitioner
                // There are instances where only a single practitioner is in a particular location.
                await Task.Delay(1000);
                // In these instances the practitioner is auto selected by the system and the label will point to two elements  (a radio button and an tabpanel)
                var practitionerRadioButtonLocator = page.GetByLabel(input.InquiryContext["Practitioner"]);
                if (await practitionerRadioButtonLocator.CountAsync() == 1)
                {
                    await practitionerRadioButtonLocator.ClickAsync();
                }

                // Appointment Type
                await page.GetByLabel(input.InquiryContext["AppointmentType"]).ClickAsync();

                // Click "Next" Button
                await page.ClickAsync("input#NextButton");

                // In most cases, a "loading" animation is visible while the booking system searches for an appointment.
                // However, in some cases, the system return "no appointments found" results so quickly that the loading animation element is never visible.
                // In these cases, we proceed to look for the appointment or a "no appointments found" message.
                try
                {
                    // Wait until the "loading" animated element is visible.
                    await page.Locator("label.AjaxLoading").WaitForAsync();

                    // Wait until the "loading" animated elements is no longer visible. (Loading is complete)
                    await page.Locator("label.AjaxLoading").WaitForAsync(new LocatorWaitForOptions() { State = WaitForSelectorState.Hidden, Timeout = 60000 });
                }
                catch (TimeoutException)
                {
                    // Deliberate no-op.
                }

                // Extract appointment time if available
                // I have noted the typo on the div class
                if (await page.Locator("div.SeletedAppointmentsFound").CountAsync() > 0)
                {
                    var firstAppointmentDate = (await page.Locator("div.SeletedAppointmentsFound > label > div.AppTableDate").First.InnerTextAsync()).Replace("\n", " ");
                    var firstAppointmentTime = (await page.Locator("div.SeletedAppointmentsFound > label > div.AppTableTime").First.InnerTextAsync()).Split('\n')[0];
                    var firstAppointmentDateAndTime = $"{firstAppointmentDate} {firstAppointmentTime}";

                    return ToDateTimeInFuture(firstAppointmentDateAndTime, input.InquiryContext["AppointmentTimeZone"]);
                }
                else if (await page.Locator("div#NoAppsAvailable").CountAsync() > 0)
                {
                    return null;
                }
                else
                {
                    throw new ExternalException("Appointment query failed.");
                }
            }
        }

        /// <summary>
        /// Parse a <see cref="DateTime"/> string without a year value that is in the format (DayOfWeek) (Day) (MonthName, 3 Character) H:MM PM/AM to the nearest future date.
        /// </summary>
        /// <param name="bookingSystemDateTimeString"></param>
        /// <returns>The parsed <see cref="DateTime"/> value.</returns>
        internal DateTime ToDateTimeInFuture(string bookingSystemDateTimeString, string bookingSystemTimeZoneId)
        {
            var bookingSystemTimeZone = TimeZoneInfo.FindSystemTimeZoneById(bookingSystemTimeZoneId);
            
            var bookingSystemDateTimeStringWithCurrentYear = $"{bookingSystemDateTimeString} {_timeProvider.GetUtcNow().ToString("yyyy", CultureInfo.InvariantCulture)}";
            var bookingSystemDateTimeStringWithNextYear = $"{bookingSystemDateTimeString} {_timeProvider.GetUtcNow().AddYears(1).ToString("yyyy", CultureInfo.InvariantCulture)}";

            // Try to parse the DateTime assuming the year is the current year.
            // If the parse fails, it means that year assumption is incorrect because the Day of week will be wrong for the current year.
            if (DateTime.TryParseExact(bookingSystemDateTimeStringWithCurrentYear, "dddd d MMM h:mm tt yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTimeCurrentYear))
            {
                var dateTimeInUtc = TimeZoneInfo.ConvertTimeToUtc(dateTimeCurrentYear, bookingSystemTimeZone);
                return dateTimeInUtc;
            }

            if (DateTime.TryParseExact(bookingSystemDateTimeStringWithNextYear, "dddd d MMM h:mm tt yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dateTimeNextYear))
            {
                var dateTimeInUtc = TimeZoneInfo.ConvertTimeToUtc(dateTimeNextYear, bookingSystemTimeZone);
                
                return dateTimeInUtc;
            }

            throw new ArgumentException($"Invalid input datetime string with value '{bookingSystemDateTimeString}'", nameof(bookingSystemDateTimeString));
        }
    }
}
