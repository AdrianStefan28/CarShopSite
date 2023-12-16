using Microsoft.EntityFrameworkCore;
using ServiceAuto.Models;
using ServiceAuto.Repositories.Interfaces;

namespace ServiceAuto.Repositories
{
    public class ServiceRepository : BaseRepository<Service>, IServiceRepository
    {
        public ServiceRepository(DbContext dbContext) : base(dbContext) { }

        public Service GetServiceByName(string name)
        {
            return dbContext.Set<Service>().Where(s => s.ServiceName.Equals(name)).FirstOrDefault();
        }
        public Service GetServiceByCity(string city)
        {
            return dbContext.Set<Service>().Where(s => s.ServiceCity.Equals(city)).FirstOrDefault();
        }

    }
}
