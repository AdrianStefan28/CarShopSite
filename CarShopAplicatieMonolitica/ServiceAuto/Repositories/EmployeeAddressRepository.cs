using Microsoft.EntityFrameworkCore;
using ServiceAuto.Models;
using ServiceAuto.Repositories.Interfaces;

namespace ServiceAuto.Repositories
{
    public class EmployeeAddressRepository : BaseRepository<EmployeeAddress>, IEmployeeAddressRepository
    {
        public EmployeeAddressRepository(DbContext dbContext) : base(dbContext) { }

    }
}
