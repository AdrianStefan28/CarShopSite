using CarShopMicroservices.CarPartService.Models;
using Microsoft.EntityFrameworkCore;

namespace CarShopMicroservices.CarPartService.Data
{
    public class CarPartDbContext : DbContext
    {
        public CarPartDbContext(DbContextOptions<CarPartDbContext> options) : base(options) { }

        public DbSet<CarPart> CarParts { get; set; }

        // Aici adaugi configurarea pentru Price
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurează precizia și scala pentru proprietatea Price
            modelBuilder.Entity<CarPart>()
                .Property(c => c.Price)
                .HasColumnType("decimal(18,2)"); // 18 cifre total și 2 zecimale
        }
    }
 }
