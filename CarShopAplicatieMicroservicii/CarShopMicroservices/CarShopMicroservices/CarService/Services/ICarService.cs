using CarShopMicroservices.CarService.Models;

namespace CarShopMicroservices.CarService.Services
{
    public interface ICarService
    {
        IEnumerable<Car> GetCars();
        Car GetCar(int id);
        Car AddCar(Car car);
        Car UpdateCar(Car car);
        void DeleteCar(int id);
    }

}
