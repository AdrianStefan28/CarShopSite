using CarShopMicroservices.CarPartService.Models;
using CarShopMicroservices.CarPartService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarShopMicroservices.CarPartService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarPartController : ControllerBase
    {
        private readonly ICarPartService _carPartService;

        public CarPartController(ICarPartService carPartService)
        {
            _carPartService = carPartService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CarPart>> Get() => Ok(_carPartService.GetCarParts());

        [HttpGet("{id}")]
        public ActionResult<CarPart> Get(int id) => Ok(_carPartService.GetCarPart(id));

        [HttpPost]
        public ActionResult<CarPart> Post([FromBody] CarPart carPart)
        {
            var createdCarPart = _carPartService.AddCarPart(carPart);
            return CreatedAtAction(nameof(Get), new { id = createdCarPart.Id }, createdCarPart);
        }

        [HttpPut("{id}")]
        public ActionResult<CarPart> Put(int id, [FromBody] CarPart carPart)
        {
            carPart.Id = id;
            var updatedCarPart = _carPartService.UpdateCarPart(carPart);
            return Ok(updatedCarPart);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _carPartService.DeleteCarPart(id);
            return NoContent();
        }
    }

}
