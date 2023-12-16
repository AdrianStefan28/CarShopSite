using ServiceAuto.Models;

namespace ServiceAuto.Services.Interfaces
{
    public interface ICarPartService
    {
        IEnumerable<CarPart> GetCarParts();
        CarPart GetCarPart(int id);
        CarPart AddCarPart(CarPart car);
        CarPart UpdateCarPart(CarPart car);
        void DeleteCarPart(int id);

    }
}
