using CarShopMicroservices.CarPartService.Models;
using CarShopMicroservices.CarPartService.Repositories;

namespace CarShopMicroservices.CarPartService.Services
{
    public class CarPartService : ICarPartService
    {
        private readonly ICarPartRepository _carPartRepository;

        public CarPartService(ICarPartRepository carPartRepository)
        {
            _carPartRepository = carPartRepository;
        }

        public IEnumerable<CarPart> GetCarParts() => _carPartRepository.GetAll();
        public CarPart GetCarPart(int id) => _carPartRepository.Get(id);
        public CarPart AddCarPart(CarPart carPart) => _carPartRepository.Add(carPart);
        public CarPart UpdateCarPart(CarPart carPart) => _carPartRepository.Update(carPart);
        public void DeleteCarPart(int id) => _carPartRepository.Remove(id);
    }

}
