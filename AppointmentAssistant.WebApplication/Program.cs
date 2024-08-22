using AppointmentAssistant.Application.Interfaces;
using AppointmentAssistant.Application.Services;
using AppointmentAssistant.Infrastructure.Repositories;
using AppointmentAssistant.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton(TimeProvider.System);

builder.Services.AddScoped<IAppointmentSearchService, AppointmentSearchService>();

builder.Services.AddScoped<IAppointmentInquirer, SmartsoftBookingGatewayAppointmentInquirer>();
builder.Services.AddScoped<IAppointmentInquirerConfigurationRepository, AppointmentInquirerConfigurationInMemoryRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
