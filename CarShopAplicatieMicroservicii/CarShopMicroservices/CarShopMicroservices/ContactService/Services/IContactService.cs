using CarShopMicroservices.ContactService.Models;

namespace CarShopMicroservices.ContactService.Services
{
    public interface IContactService
    {
        IEnumerable<Contact> GetContacts();
        Contact GetContact(int id);
        Contact AddContact(Contact contact);
        void DeleteContact(int id);
    }

}
