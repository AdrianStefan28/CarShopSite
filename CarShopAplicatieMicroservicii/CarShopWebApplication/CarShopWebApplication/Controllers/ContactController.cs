using CarShopWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CarShopWebApplication.Controllers
{
    public class ContactController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ContactController> _logger;

        public ContactController(HttpClient httpClient, ILogger<ContactController> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        // Get all contacts
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://localhost:7137/api/contact");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var contacts = JsonConvert.DeserializeObject<List<Contact>>(json);
                    return View(contacts);
                }
                else
                {
                    _logger.LogError($"Failed to fetch contacts: {response.StatusCode}");
                    ViewBag.ErrorMessage = "Unable to fetch contacts. Please try again later.";
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching contacts");
                ViewBag.ErrorMessage = "An unexpected error occurred.";
                return View("Error");
            }
        }

        // Create a new contact
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://localhost:7137/api/contact", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    _logger.LogError($"Failed to create contact: {response.StatusCode}");
                    ViewBag.ErrorMessage = "Failed to create contact. Please try again.";
                    return View(contact);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating contact");
                ViewBag.ErrorMessage = "An unexpected error occurred.";
                return View(contact);
            }
        }

        // Edit an existing contact
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://localhost:7137/api/contact/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var contact = JsonConvert.DeserializeObject<Contact>(json);
                    return View(contact);
                }
                else
                {
                    _logger.LogError($"Failed to fetch contact details: {response.StatusCode}");
                    ViewBag.ErrorMessage = "Unable to fetch contact details.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching contact details");
                ViewBag.ErrorMessage = "An unexpected error occurred.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(contact), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"https://localhost:7137/api/contact/{contact.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    _logger.LogError($"Failed to update contact: {response.StatusCode}");
                    ViewBag.ErrorMessage = "Failed to update contact. Please try again.";
                    return View(contact);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating contact");
                ViewBag.ErrorMessage = "An unexpected error occurred.";
                return View(contact);
            }
        }

        // Delete a contact
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"https://localhost:7137/api/contact/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    _logger.LogError($"Failed to delete contact: {response.StatusCode}");
                    ViewBag.ErrorMessage = "Failed to delete contact.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting contact");
                ViewBag.ErrorMessage = "An unexpected error occurred.";
                return RedirectToAction("Index");
            }
        }
    }

}
