using CarShopMicroservices.ServiceAppointmentService.Models;
using CarShopMicroservices.ServiceAppointmentService.Repositories;
using CarShopMicroservices.ServiceAppointmentService.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarShopMicroservices.ServiceAppointmentService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceAppointmentController : ControllerBase
    {
        private readonly IServiceAppointmentService _appointmentService;

        public ServiceAppointmentController(IServiceAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ServiceAppointment>> Get() => Ok(_appointmentService.GetAppointments());

        [HttpGet("{id}")]
        public ActionResult<ServiceAppointment> Get(int id) => Ok(_appointmentService.GetAppointment(id));

        [HttpPost]
        public ActionResult<ServiceAppointment> Post([FromBody] ServiceAppointment appointment)
        {
            var createdAppointment = _appointmentService.AddAppointment(appointment);
            return CreatedAtAction(nameof(Get), new { id = createdAppointment.Id }, createdAppointment);
        }

        [HttpPut("{id}")]
        public ActionResult<ServiceAppointment> Put(int id, [FromBody] ServiceAppointment appointment)
        {
            appointment.Id = id;
            var updatedAppointment = _appointmentService.UpdateAppointment(appointment);
            return Ok(updatedAppointment);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _appointmentService.DeleteAppointment(id);
            return NoContent();
        }
    }


}
