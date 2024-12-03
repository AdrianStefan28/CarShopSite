using CarShopMicroservices.CarPartService.Models;

namespace CarShopMicroservices.CarPartService.Services
{
        public interface ICarPartService
        {
            IEnumerable<CarPart> GetCarParts();

            CarPart GetCarPart(int id);

            CarPart AddCarPart(CarPart carPart);

            CarPart UpdateCarPart(CarPart carPart);

            void DeleteCarPart(int id);
        }

}
