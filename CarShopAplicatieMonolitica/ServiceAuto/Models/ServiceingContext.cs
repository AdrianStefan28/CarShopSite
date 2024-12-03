using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ServiceAuto.Models
{
    public class ServiceingContext : IdentityDbContext<IdentityUser>
    {
        public ServiceingContext(DbContextOptions<ServiceingContext> options)
            : base(options)
        { }

        public DbSet<Service> Services { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<CarPart> CarParts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeAddress> EmployeeAddresses { get; set; }
        public DbSet<ExpenseReport> ExpenseReports { get; set; }
        public DbSet<ProgramareService> ProgramareServices { get; set; }

    }
}
