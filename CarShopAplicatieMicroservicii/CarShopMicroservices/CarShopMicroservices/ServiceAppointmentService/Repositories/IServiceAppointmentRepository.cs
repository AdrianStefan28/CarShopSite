using CarShopMicroservices.ServiceAppointmentService.Models;

namespace CarShopMicroservices.ServiceAppointmentService.Repositories
{
    public interface IServiceAppointmentRepository
    {
        IEnumerable<ServiceAppointment> GetAll();
        ServiceAppointment Get(int id);
        ServiceAppointment Add(ServiceAppointment appointment);
        ServiceAppointment Update(ServiceAppointment appointment);
        void Remove(int id);
    }

}
