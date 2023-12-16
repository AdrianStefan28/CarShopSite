using Microsoft.EntityFrameworkCore;
using ServiceAuto.Models;
using ServiceAuto.Repositories.Interfaces;

namespace ServiceAuto.Repositories
{
    public class CarPartRepository : BaseRepository<CarPart>, ICarPartRepository
    {
        public CarPartRepository(DbContext dbContext) : base(dbContext) { }

    }
}
