using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAuto.Models;
using ServiceAuto.Services;
using System.Diagnostics;

namespace ServiceAuto.Controllers
{
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly ContactService contactService;

        public ContactController(ILogger<ContactController> logger, ContactService conService)
        {
            contactService = conService;
            _logger = logger;
        }
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="Admin")]
        public ActionResult List()
        {
            var contacts = contactService.GetContacts();   
            return View(contacts);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = contactService.GetContact(id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,ContactFirstName,ContactLastName,ContactEmail,ContactSubject,ContactContext")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                contactService.AddContact(contact);
                return RedirectToAction(nameof(Confirm));
            }
            return View(contact);
        }

        public ActionResult Confirm()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contact = contactService.GetContact(id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (contactService.GetContacts() == null)
            {
                return Problem("Entity set 'ServiceingContext.Cars'  is null.");
            }
            var contact = contactService.GetContact(id);
            if (contact != null)
            {
                contactService.DeleteContact(id);
            }

            return RedirectToAction(nameof(List));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
