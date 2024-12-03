using CarShopMicroservices.ServiceAppointmentService.Data;
using CarShopMicroservices.ServiceAppointmentService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CarShopMicroservices.ServiceAppointmentService.Repositories
{
    public class ServiceAppointmentRepository : IServiceAppointmentRepository
    {
        private readonly ServiceAppointmentDbContext _context;

        // Constructorul primește DbContext pentru a accesa baza de date
        public ServiceAppointmentRepository(ServiceAppointmentDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ServiceAppointment> GetAll() => _context.ServiceAppointments.ToList(); // Returnează toate programările din DB

        public ServiceAppointment Get(int id) => _context.ServiceAppointments.FirstOrDefault(a => a.Id == id); // Căutăm programarea după ID

        public ServiceAppointment Add(ServiceAppointment appointment)
        {
            _context.ServiceAppointments.Add(appointment); // Adăugăm programarea în DB
            _context.SaveChanges(); // Salvează modificările în baza de date
            return appointment;
        }

        public ServiceAppointment Update(ServiceAppointment appointment)
        {
            var existingAppointment = _context.ServiceAppointments.FirstOrDefault(a => a.Id == appointment.Id);
            if (existingAppointment != null)
            {
                existingAppointment.CarId = appointment.CarId;
                existingAppointment.AppointmentDate = appointment.AppointmentDate;
                existingAppointment.ServiceDescription = appointment.ServiceDescription;
                _context.SaveChanges(); // Salvează modificările
            }
            return existingAppointment;
        }

        public void Remove(int id)
        {
            var appointment = _context.ServiceAppointments.FirstOrDefault(a => a.Id == id);
            if (appointment != null)
            {
                _context.ServiceAppointments.Remove(appointment); // Ștergem programarea din DB
                _context.SaveChanges(); // Salvează modificările
            }
        }
    }
}
