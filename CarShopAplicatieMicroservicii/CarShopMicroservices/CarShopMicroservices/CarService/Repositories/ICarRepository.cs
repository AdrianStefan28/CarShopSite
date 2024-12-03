using CarShopMicroservices.CarService.Models;

namespace CarShopMicroservices.CarService.Repositories
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetAll();
        Car Get(int id);
        Car Add(Car car);
        Car Update(Car car);
        void Remove(int id);
    }

}
