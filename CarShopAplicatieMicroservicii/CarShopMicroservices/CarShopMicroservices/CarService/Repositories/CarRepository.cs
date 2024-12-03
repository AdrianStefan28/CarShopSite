using CarShopMicroservices.CarService.Data;
using CarShopMicroservices.CarService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CarShopMicroservices.CarService.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly CarDbContext _context;

        // Constructorul primește DbContext pentru a accesa baza de date
        public CarRepository(CarDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Car> GetAll() => _context.Cars.ToList(); // Returnează toate mașinile din DB

        public Car Get(int id) => _context.Cars.FirstOrDefault(c => c.Id == id); // Căutăm mașina după ID

        public Car Add(Car car)
        {
            _context.Cars.Add(car); // Adăugăm mașina în DB
            _context.SaveChanges(); // Salvează modificările în baza de date
            return car;
        }

        public Car Update(Car car)
        {
            var existingCar = _context.Cars.FirstOrDefault(c => c.Id == car.Id);
            if (existingCar != null)
            {
                existingCar.Make = car.Make;
                existingCar.Model = car.Model;
                existingCar.Year = car.Year;
                existingCar.Price = car.Price;
                _context.SaveChanges(); // Salvează modificările
            }
            return existingCar;
        }

        public void Remove(int id)
        {
            var car = _context.Cars.FirstOrDefault(c => c.Id == id);
            if (car != null)
            {
                _context.Cars.Remove(car); // Ștergem mașina din DB
                _context.SaveChanges(); // Salvează modificările
            }
        }
    }
}
