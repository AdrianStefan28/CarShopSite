using CarShopMicroservices.CarService.Models;
using CarShopMicroservices.CarService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarShopMicroservices.CarService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Car>> Get() => Ok(_carService.GetCars());

        [HttpGet("{id}")]
        public ActionResult<Car> Get(int id) => Ok(_carService.GetCar(id));

        [HttpPost]
        public ActionResult<Car> Post([FromBody] Car car)
        {
            var createdCar = _carService.AddCar(car);
            return CreatedAtAction(nameof(Get), new { id = createdCar.Id }, createdCar);
        }

        [HttpPut("{id}")]
        public ActionResult<Car> Put(int id, [FromBody] Car car)
        {
            car.Id = id;
            var updatedCar = _carService.UpdateCar(car);
            return Ok(updatedCar);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _carService.DeleteCar(id);
            return NoContent();
        }
    }

}
