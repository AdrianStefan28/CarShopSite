using CarShopMicroservices.ContactService.Models;
using CarShopMicroservices.ContactService.Repositories;

namespace CarShopMicroservices.ContactService.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public IEnumerable<Contact> GetContacts() => _contactRepository.GetAll();
        public Contact GetContact(int id) => _contactRepository.Get(id);
        public Contact AddContact(Contact contact) => _contactRepository.Add(contact);
        public void DeleteContact(int id) => _contactRepository.Remove(id);
    }
}
