using CarShopMicroservices.ServiceAppointmentService.Models;
using CarShopMicroservices.ServiceAppointmentService.Repositories;

namespace CarShopMicroservices.ServiceAppointmentService.Services
{
    public class ServiceAppointmentService : IServiceAppointmentService
    {
        private readonly IServiceAppointmentRepository _appointmentRepository;

        public ServiceAppointmentService(IServiceAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public IEnumerable<ServiceAppointment> GetAppointments() => _appointmentRepository.GetAll();
        public ServiceAppointment GetAppointment(int id) => _appointmentRepository.Get(id);
        public ServiceAppointment AddAppointment(ServiceAppointment appointment) => _appointmentRepository.Add(appointment);
        public ServiceAppointment UpdateAppointment(ServiceAppointment appointment) => _appointmentRepository.Update(appointment);
        public void DeleteAppointment(int id) => _appointmentRepository.Remove(id);
    }

}
