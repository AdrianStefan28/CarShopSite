using CarShopWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

public class CarPartController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CarPartController> _logger;

    public CarPartController(HttpClient httpClient, ILogger<CarPartController> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    // Get all car parts
    public async Task<IActionResult> Index()
    {
        try
        {
            var response = await _httpClient.GetAsync("https://localhost:7137/api/carpart");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var carParts = JsonConvert.DeserializeObject<List<CarPart>>(json);
                return View(carParts);
            }
            else
            {
                _logger.LogError($"Failed to fetch car parts: {response.StatusCode}");
                ViewBag.ErrorMessage = "Unable to fetch car parts. Please try again later.";
                return View("Error");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching car parts");
            ViewBag.ErrorMessage = "An unexpected error occurred.";
            return View("Error");
        }
    }

    // Create a new car part
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CarPart carPart)
    {
        if (!ModelState.IsValid)
        {
            return View(carPart);
        }

        try
        {
            var content = new StringContent(JsonConvert.SerializeObject(carPart), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7137/api/carpart", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                _logger.LogError($"Failed to create car part: {response.StatusCode}");
                ViewBag.ErrorMessage = "Failed to create car part. Please try again.";
                return View(carPart);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating car part");
            ViewBag.ErrorMessage = "An unexpected error occurred.";
            return View(carPart);
        }
    }

    // Edit an existing car part
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"https://localhost:7137/api/carpart/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var carPart = JsonConvert.DeserializeObject<CarPart>(json);
                return View(carPart);
            }
            else
            {
                _logger.LogError($"Failed to fetch car part details: {response.StatusCode}");
                ViewBag.ErrorMessage = "Unable to fetch car part details.";
                return RedirectToAction("Index");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching car part details");
            ViewBag.ErrorMessage = "An unexpected error occurred.";
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(CarPart carPart)
    {
        if (!ModelState.IsValid)
        {
            return View(carPart);
        }

        try
        {
            var content = new StringContent(JsonConvert.SerializeObject(carPart), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"https://localhost:7137/api/carpart/{carPart.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                _logger.LogError($"Failed to update car part: {response.StatusCode}");
                ViewBag.ErrorMessage = "Failed to update car part. Please try again.";
                return View(carPart);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating car part");
            ViewBag.ErrorMessage = "An unexpected error occurred.";
            return View(carPart);
        }
    }

    // Delete a car part
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7137/api/carpart/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                _logger.LogError($"Failed to delete car part: {response.StatusCode}");
                ViewBag.ErrorMessage = "Failed to delete car part.";
                return RedirectToAction("Index");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting car part");
            ViewBag.ErrorMessage = "An unexpected error occurred.";
            return RedirectToAction("Index");
        }
    }
}
