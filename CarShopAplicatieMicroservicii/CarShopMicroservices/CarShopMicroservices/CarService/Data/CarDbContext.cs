namespace CarShopMicroservices.CarService.Data
{
    using Microsoft.EntityFrameworkCore;
    using CarShopMicroservices.CarService.Models;

    public class CarDbContext : DbContext
    {
        public CarDbContext(DbContextOptions<CarDbContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>()
                .Property(c => c.Price)
                .HasConversion(
                    v => v,     // Convertirea la valoarea originală
                    v => v);    // Convertirea înapoi la valoarea originală
        }

    }

}
