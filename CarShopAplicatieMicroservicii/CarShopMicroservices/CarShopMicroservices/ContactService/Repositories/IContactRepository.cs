using CarShopMicroservices.ContactService.Models;

namespace CarShopMicroservices.ContactService.Repositories
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetAll();
        Contact Get(int id);
        Contact Add(Contact contact);
        void Remove(int id);
    }

}
