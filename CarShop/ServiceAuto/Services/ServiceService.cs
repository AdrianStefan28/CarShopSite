using ServiceAuto.Models;
using ServiceAuto.Repositories.Interfaces;
using ServiceAuto.Services.Interfaces;

namespace ServiceAuto.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository serviceRepository;
        public ServiceService(IServiceRepository serviceRepository)
        {
            this.serviceRepository = serviceRepository;
        }

        public IEnumerable<Service> GetServices()
        {
            return serviceRepository.GetAll();
        }

        public Service GetService(int id)
        {
            return serviceRepository.Get(id);
        }

        public Service AddService(Service service)
        {
            return serviceRepository.Add(service);
        }
        public Service UpdateService(Service service)
        {
            return serviceRepository.Update(service);
        }

        public void DeleteService(int id)
        {
            serviceRepository.Remove(id);
        }

        public Service GetServiceByName(string name)
        {
            return serviceRepository.GetServiceByName(name);
        }
        public Service GetServiceByCity(string city)
        {
            return serviceRepository.GetServiceByCity(city);
        }

    }
}
