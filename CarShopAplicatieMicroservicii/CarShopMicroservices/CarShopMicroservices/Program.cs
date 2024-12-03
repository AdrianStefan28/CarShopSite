using CarShopMicroservices.CarPartService.Repositories;
using CarShopMicroservices.CarPartService.Services;
using CarShopMicroservices.CarService.Repositories;
using CarShopMicroservices.CarService.Services;
using CarShopMicroservices.ContactService.Repositories;
using CarShopMicroservices.ContactService.Services;
using CarShopMicroservices.ServiceAppointmentService.Controllers;
using CarShopMicroservices.ServiceAppointmentService.Repositories;
using CarShopMicroservices.ServiceAppointmentService.Services;
using Microsoft.EntityFrameworkCore;
using CarShopMicroservices.CarPartService.Data;
using CarShopMicroservices.CarService.Data;
using CarShopMicroservices.ContactService.Data;  // Asta depinde de locația fișierului ApplicationDbContext
using CarShopMicroservices.ServiceAppointmentService.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Configurare pentru CarPartDbContext
builder.Services.AddDbContext<CarPartDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CarPartConnection")));

// Configurare pentru CarDbContext
builder.Services.AddDbContext<CarDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CarConnection")));

// Configurare pentru ContactDbContext
builder.Services.AddDbContext<ContactDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ContactConnection")));

// Configurare pentru ServiceAppointmentDbContext
builder.Services.AddDbContext<ServiceAppointmentDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ServiceAppointmentConnection")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add your services here.
builder.Services.AddScoped<ICarPartRepository, CarPartRepository>();
builder.Services.AddScoped<ICarPartService, CarPartService>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddScoped<IServiceAppointmentRepository, ServiceAppointmentRepository>();
builder.Services.AddScoped<IServiceAppointmentService, ServiceAppointmentService>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<IContactService, ContactService>();

// Add CORS policy to allow requests from the frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://localhost:7276") // URL-ul aplicației frontend
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Use CORS policy
app.UseCors("AllowFrontend");

app.UseAuthorization();

app.MapControllers();

app.Run();