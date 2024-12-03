using CarShopMicroservices.CarPartService.Models;

namespace CarShopMicroservices.CarPartService.Repositories
{
    public interface ICarPartRepository
    {
        IEnumerable<CarPart> GetAll();
        CarPart Get(int id);
        CarPart Add(CarPart carPart);
        CarPart Update(CarPart carPart);
        void Remove(int id);
    }

}
