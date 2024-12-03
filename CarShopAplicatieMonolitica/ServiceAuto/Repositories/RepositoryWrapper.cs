using ServiceAuto.Repositories.Interfaces;
using ServiceAuto.Models;

namespace ServiceAuto.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ServiceingContext _serviceingContext;
       


        public RepositoryWrapper(ServiceingContext serviceingContext)
        {
            _serviceingContext = serviceingContext;
        }

        public void Save()
        {
            _serviceingContext.SaveChanges();
        }
    }
}
