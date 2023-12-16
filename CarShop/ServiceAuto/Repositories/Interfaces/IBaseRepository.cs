using ServiceAuto.Models;

namespace ServiceAuto.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : ModelEntity
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        T Add(T entity);
        T Update(T entity);
        void Remove(int entityId);

    }
}
