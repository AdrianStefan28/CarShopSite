using ServiceAuto.Models;
using ServiceAuto.Repositories.Interfaces;
using ServiceAuto.Services.Interfaces;

namespace ServiceAuto.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository carRepository;

        public CarService(ICarRepository carRepository)
        {
            this.carRepository = carRepository;
        }
        public IEnumerable<Car> GetCars()
        {
            return carRepository.GetAll();
        }

        public Car GetCar(int id)
        {
            return carRepository.Get(id);
        }

        public Car AddCar(Car car)
        {
            return carRepository.Add(car);
        }
        public Car UpdateCar(Car car)
        {
            return carRepository.Update(car);
        }

        public void DeleteCar(int id)
        {
            carRepository.Remove(id);
        }

    }
}
