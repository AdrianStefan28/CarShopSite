using Microsoft.EntityFrameworkCore;
using ServiceAuto.Models;
using Microsoft.AspNetCore.Identity;
using ServiceAuto.Repositories.Interfaces;
using ServiceAuto.Repositories;
using ServiceAuto.Services.Interfaces;
using ServiceAuto.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ServiceingDb");

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<CarService>();
builder.Services.AddScoped<ICarPartRepository, CarPartRepository>();
builder.Services.AddScoped<CarPartService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<IEmployeeAddressRepository, EmployeeAddressRepository>();
builder.Services.AddScoped<EmployeeAddressService>();
builder.Services.AddScoped<IExpenseReportRepository, ExpenseReportRepository>();
builder.Services.AddScoped<ExpenseReportService>();
builder.Services.AddScoped<IServiceRepository, ServiceRepository>();
builder.Services.AddScoped<ServiceService>();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
builder.Services.AddScoped<ContactService>();
builder.Services.AddScoped<IProgramareServiceRepository, ProgramareServiceRepository>();
builder.Services.AddScoped<ProgramareServiceService>();


builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ServiceingContext>();
builder.Services.AddDbContext<DbContext, ServiceingContext>(options =>
options.UseSqlServer(connectionString));


builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = true;
    options.Password.RequireLowercase = false;
    options.Password.RequiredUniqueChars = 4;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
    options.Lockout.MaxFailedAccessAttempts = 10;
    options.Lockout.AllowedForNewUsers = true;

    // User Settings
    options.User.RequireUniqueEmail = true;
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
