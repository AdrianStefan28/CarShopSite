namespace CarShopWebApplication.Controllers
{
    using CarShopWebApplication.Models;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public class ServiceAppointmentController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ServiceAppointmentController> _logger;

        public ServiceAppointmentController(HttpClient httpClient, ILogger<ServiceAppointmentController> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        // Get all service appointments
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://localhost:7137/api/serviceappointment");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var serviceAppointments = JsonConvert.DeserializeObject<List<ServiceAppointment>>(json);
                    return View(serviceAppointments);
                }
                else
                {
                    _logger.LogError($"Failed to fetch service appointments: {response.StatusCode}");
                    ViewBag.ErrorMessage = "Unable to fetch service appointments. Please try again later.";
                    return View("Error");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching service appointments");
                ViewBag.ErrorMessage = "An unexpected error occurred.";
                return View("Error");
            }
        }

        // Create a new service appointment
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ServiceAppointment serviceAppointment)
        {
            if (!ModelState.IsValid)
            {
                return View(serviceAppointment);
            }

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(serviceAppointment), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("https://localhost:7137/api/serviceappointment", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    _logger.LogError($"Failed to create service appointment: {response.StatusCode}");
                    ViewBag.ErrorMessage = "Failed to create service appointment. Please try again.";
                    return View(serviceAppointment);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating service appointment");
                ViewBag.ErrorMessage = "An unexpected error occurred.";
                return View(serviceAppointment);
            }
        }

        // Edit an existing service appointment
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://localhost:7137/api/serviceappointment/{id}");
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    var serviceAppointment = JsonConvert.DeserializeObject<ServiceAppointment>(json);
                    return View(serviceAppointment);
                }
                else
                {
                    _logger.LogError($"Failed to fetch service appointment details: {response.StatusCode}");
                    ViewBag.ErrorMessage = "Unable to fetch service appointment details.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching service appointment details");
                ViewBag.ErrorMessage = "An unexpected error occurred.";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ServiceAppointment serviceAppointment)
        {
            if (!ModelState.IsValid)
            {
                return View(serviceAppointment);
            }

            try
            {
                var content = new StringContent(JsonConvert.SerializeObject(serviceAppointment), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"https://localhost:7137/api/serviceappointment/{serviceAppointment.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    _logger.LogError($"Failed to update service appointment: {response.StatusCode}");
                    ViewBag.ErrorMessage = "Failed to update service appointment. Please try again.";
                    return View(serviceAppointment);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating service appointment");
                ViewBag.ErrorMessage = "An unexpected error occurred.";
                return View(serviceAppointment);
            }
        }

        // Delete a service appointment
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"https://localhost:7137/api/serviceappointment/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    _logger.LogError($"Failed to delete service appointment: {response.StatusCode}");
                    ViewBag.ErrorMessage = "Failed to delete service appointment.";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting service appointment");
                ViewBag.ErrorMessage = "An unexpected error occurred.";
                return RedirectToAction("Index");
            }
        }
    }

}
