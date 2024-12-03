using Microsoft.EntityFrameworkCore;
using CarShopMicroservices.ServiceAppointmentService.Models;

namespace CarShopMicroservices.ServiceAppointmentService.Data
{
    public class ServiceAppointmentDbContext : DbContext
    {
        public ServiceAppointmentDbContext(DbContextOptions<ServiceAppointmentDbContext> options) : base(options) { }

        // DbSet pentru ServiceAppointment
        public DbSet<ServiceAppointment> ServiceAppointments { get; set; }

        // Configurare suplimentară pentru modelul ServiceAppointment, dacă este necesar
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Exemple de configurare suplimentară
            modelBuilder.Entity<ServiceAppointment>()
                .Property(sa => sa.ServiceDescription)
                .IsRequired()
                .HasMaxLength(500);

            modelBuilder.Entity<ServiceAppointment>()
                .Property(sa => sa.AppointmentDate)
                .IsRequired();
        }
    }
}
