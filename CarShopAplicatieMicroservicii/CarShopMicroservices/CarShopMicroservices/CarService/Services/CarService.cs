using CarShopMicroservices.CarService.Models;
using CarShopMicroservices.CarService.Repositories;

namespace CarShopMicroservices.CarService.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public IEnumerable<Car> GetCars() => _carRepository.GetAll();
        public Car GetCar(int id) => _carRepository.Get(id);
        public Car AddCar(Car car) => _carRepository.Add(car);
        public Car UpdateCar(Car car) => _carRepository.Update(car);
        public void DeleteCar(int id) => _carRepository.Remove(id);
    }

}
