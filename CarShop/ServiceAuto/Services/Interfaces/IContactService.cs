using ServiceAuto.Models;

namespace ServiceAuto.Services.Interfaces
{
    public interface IContactService
    {
        IEnumerable<Contact> GetContacts();
        Contact GetContact(int id);
        Contact AddContact(Contact contact);
        Contact UpdateContact(Contact contact);
        void DeleteContact(int id);
    }
}
