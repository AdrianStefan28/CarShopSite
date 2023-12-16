using Microsoft.EntityFrameworkCore;
using ServiceAuto.Models;
using ServiceAuto.Repositories.Interfaces;

namespace ServiceAuto.Repositories
{
    public class ContactRepository : BaseRepository<Contact>, IContactRepository
    {
        public ContactRepository(DbContext dbContext) : base(dbContext) { }
    }
}
