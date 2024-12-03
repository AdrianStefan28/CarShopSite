using CarShopMicroservices.ServiceAppointmentService.Models;

namespace CarShopMicroservices.ServiceAppointmentService.Services
{
    public interface IServiceAppointmentService
    {
        IEnumerable<ServiceAppointment> GetAppointments();
        ServiceAppointment GetAppointment(int id);
        ServiceAppointment AddAppointment(ServiceAppointment appointment);
        ServiceAppointment UpdateAppointment(ServiceAppointment appointment);
        void DeleteAppointment(int id);
    }

}
