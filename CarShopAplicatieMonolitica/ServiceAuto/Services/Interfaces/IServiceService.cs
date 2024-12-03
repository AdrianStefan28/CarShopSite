using ServiceAuto.Models;

namespace ServiceAuto.Services.Interfaces
{
    public interface IServiceService
    {
        IEnumerable<Service> GetServices();
        Service GetService(int id);
        Service AddService(Service service);
        Service UpdateService(Service service);
        void DeleteService(int id);

        Service GetServiceByName(string name);
        Service GetServiceByCity(string city);
    }
}
