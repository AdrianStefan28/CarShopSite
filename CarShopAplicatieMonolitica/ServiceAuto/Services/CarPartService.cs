using ServiceAuto.Models;
using ServiceAuto.Repositories.Interfaces;
using ServiceAuto.Services.Interfaces;

namespace ServiceAuto.Services
{
    public class CarPartService : ICarPartService
    {
        private readonly ICarPartRepository carPartRepository;

        public CarPartService(ICarPartRepository carPartRepository)
        {
            this.carPartRepository = carPartRepository;
        }
        public IEnumerable<CarPart> GetCarParts()
        {
            return carPartRepository.GetAll();
        }

        public CarPart GetCarPart(int id)
        {
            return carPartRepository.Get(id);
        }

        public CarPart AddCarPart(CarPart carPart)
        {
            return carPartRepository.Add(carPart);
        }
        public CarPart UpdateCarPart(CarPart carPart)
        {
            return carPartRepository.Update(carPart);
        }

        public void DeleteCarPart(int id)
        {
            carPartRepository.Remove(id);
        }
    }
}
