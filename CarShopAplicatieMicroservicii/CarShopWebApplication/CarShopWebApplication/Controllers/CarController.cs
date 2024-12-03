using CarShopWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

public class CarController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<CarController> _logger;

    public CarController(HttpClient httpClient, ILogger<CarController> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    // Get all cars
    public async Task<IActionResult> Index()
    {
        try
        {
            var response = await _httpClient.GetAsync("https://localhost:7137/api/car");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var cars = JsonConvert.DeserializeObject<List<Car>>(json);
                return View(cars);
            }
            else
            {
                _logger.LogError($"Failed to fetch cars: {response.StatusCode}");
                ViewBag.ErrorMessage = "Unable to fetch cars. Please try again later.";
                return View("Error");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching cars");
            ViewBag.ErrorMessage = "An unexpected error occurred.";
            return View("Error");
        }
    }

    // Create a new car
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Car car)
    {
        if (!ModelState.IsValid)
        {
            return View(car);
        }

        try
        {
            var content = new StringContent(JsonConvert.SerializeObject(car), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://localhost:7137/api/car", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                _logger.LogError($"Failed to create car: {response.StatusCode}");
                ViewBag.ErrorMessage = "Failed to create car. Please try again.";
                return View(car);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating car");
            ViewBag.ErrorMessage = "An unexpected error occurred.";
            return View(car);
        }
    }

    // Edit an existing car
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"https://localhost:7137/api/car/{id}");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var car = JsonConvert.DeserializeObject<Car>(json);
                return View(car);
            }
            else
            {
                _logger.LogError($"Failed to fetch car details: {response.StatusCode}");
                ViewBag.ErrorMessage = "Unable to fetch car details.";
                return RedirectToAction("Index");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching car details");
            ViewBag.ErrorMessage = "An unexpected error occurred.";
            return RedirectToAction("Index");
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Car car)
    {
        if (!ModelState.IsValid)
        {
            return View(car);
        }

        try
        {
            var content = new StringContent(JsonConvert.SerializeObject(car), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"https://localhost:7137/api/car/{car.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                _logger.LogError($"Failed to update car: {response.StatusCode}");
                ViewBag.ErrorMessage = "Failed to update car. Please try again.";
                return View(car);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating car");
            ViewBag.ErrorMessage = "An unexpected error occurred.";
            return View(car);
        }
    }

    // Delete a car
    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7137/api/car/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                _logger.LogError($"Failed to delete car: {response.StatusCode}");
                ViewBag.ErrorMessage = "Failed to delete car.";
                return RedirectToAction("Index");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting car");
            ViewBag.ErrorMessage = "An unexpected error occurred.";
            return RedirectToAction("Index");
        }
    }
}
