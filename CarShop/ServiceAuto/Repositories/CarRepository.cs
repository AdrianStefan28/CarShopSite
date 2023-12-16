using Microsoft.EntityFrameworkCore;
using ServiceAuto.Models;
using ServiceAuto.Repositories.Interfaces;

namespace ServiceAuto.Repositories
{
    public class CarRepository : BaseRepository<Car>, ICarRepository
    {
        public CarRepository(DbContext dbContext) : base(dbContext) { }

    }
}
