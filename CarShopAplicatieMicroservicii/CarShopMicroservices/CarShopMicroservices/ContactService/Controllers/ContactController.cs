using CarShopMicroservices.ContactService.Models;
using CarShopMicroservices.ContactService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarShopMicroservices.ContactService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Contact>> Get() => Ok(_contactService.GetContacts());

        [HttpGet("{id}")]
        public ActionResult<Contact> Get(int id) => Ok(_contactService.GetContact(id));

        [HttpPost]
        public ActionResult<Contact> Post([FromBody] Contact contact)
        {
            var createdContact = _contactService.AddContact(contact);
            return CreatedAtAction(nameof(Get), new { id = createdContact.Id }, createdContact);
        }

        [HttpPut("{id}")]
        public ActionResult<Contact> Put(int id, [FromBody] Contact contact)
        {
            contact.Id = id;
            var updatedContact = _contactService.AddContact(contact);
            return Ok(updatedContact);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _contactService.DeleteContact(id);
            return NoContent();
        }
    }

}
