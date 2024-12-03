using CarShopMicroservices.ContactService.Data;
using CarShopMicroservices.ContactService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CarShopMicroservices.ContactService.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactDbContext _context;

        // Constructorul primește DbContext pentru a accesa baza de date
        public ContactRepository(ContactDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Contact> GetAll() => _context.Contacts.ToList(); // Returnează toate contactele din DB

        public Contact Get(int id) => _context.Contacts.FirstOrDefault(c => c.Id == id); // Căutăm contactul după ID

        public Contact Add(Contact contact)
        {
            _context.Contacts.Add(contact); // Adăugăm contactul în DB
            _context.SaveChanges(); // Salvează modificările în baza de date
            return contact;
        }

        public void Remove(int id)
        {
            var contact = _context.Contacts.FirstOrDefault(c => c.Id == id);
            if (contact != null)
            {
                _context.Contacts.Remove(contact); // Ștergem contactul din DB
                _context.SaveChanges(); // Salvează modificările
            }
        }
    }
}
