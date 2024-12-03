using Microsoft.EntityFrameworkCore;
using CarShopMicroservices.ContactService.Models;

namespace CarShopMicroservices.ContactService.Data
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options) { }

        // DbSet pentru Contact
        public DbSet<Contact> Contacts { get; set; }

        // Configurare suplimentară pentru modelul Contact, dacă este necesar
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Exemple de configurare suplimentară
            modelBuilder.Entity<Contact>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<Contact>()
                .Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<Contact>()
                .Property(c => c.Message)
                .IsRequired()
                .HasMaxLength(500);
        }
    }
}
