using Microsoft.CodeAnalysis;
using ServiceAuto.Models;

namespace ServiceAuto.Repositories.Interfaces
{
    public interface IServiceRepository : IBaseRepository<Service>
    {
        Service? GetServiceByName(string name);
        Service? GetServiceByCity(string city);
    }
}
