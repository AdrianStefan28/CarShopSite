using Microsoft.EntityFrameworkCore;
using ServiceAuto.Models;
using ServiceAuto.Repositories.Interfaces;

namespace ServiceAuto.Repositories
{
    public class ProgramareServiceRepository : BaseRepository<ProgramareService>, IProgramareServiceRepository
    {
        public ProgramareServiceRepository(DbContext dbContext) : base(dbContext) { }
    }
      
}
