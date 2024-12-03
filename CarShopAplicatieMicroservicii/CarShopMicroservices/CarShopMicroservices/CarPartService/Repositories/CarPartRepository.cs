using CarShopMicroservices.CarPartService.Data;
using CarShopMicroservices.CarPartService.Models;
using Microsoft.EntityFrameworkCore;

namespace CarShopMicroservices.CarPartService.Repositories
{
    public class CarPartRepository : ICarPartRepository
    {
        private readonly CarPartDbContext _context;

        // Constructorul primește DbContext pentru a accesa baza de date
        public CarPartRepository(CarPartDbContext context)
        {
            _context = context;
        }

        public IEnumerable<CarPart> GetAll() => _context.CarParts.ToList();

        public CarPart Get(int id) => _context.CarParts.FirstOrDefault(p => p.Id == id);

        public CarPart Add(CarPart carPart)
        {
            _context.CarParts.Add(carPart); // Adăugăm carPart în DbSet
            _context.SaveChanges(); // Salvează modificările în baza de date
            return carPart;
        }

        public CarPart Update(CarPart carPart)
        {
            var existingCarPart = _context.CarParts.FirstOrDefault(p => p.Id == carPart.Id);
            if (existingCarPart != null)
            {
                existingCarPart.Name = carPart.Name;
                existingCarPart.Price = carPart.Price;
                existingCarPart.Stock = carPart.Stock;
                _context.SaveChanges(); // Salvează modificările
            }
            return existingCarPart;
        }

        public void Remove(int id)
        {
            var carPart = _context.CarParts.FirstOrDefault(p => p.Id == id);
            if (carPart != null)
            {
                _context.CarParts.Remove(carPart);
                _context.SaveChanges(); // Salvează modificările
            }
        }
    }
}
