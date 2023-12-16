using ServiceAuto.Models;
using ServiceAuto.Repositories.Interfaces;
using ServiceAuto.Services.Interfaces;

namespace ServiceAuto.Services
{
    public class EmployeeAddressService : IEmployeeAddressService
    {
        private readonly IEmployeeAddressRepository employeeAddressRepository;
        public EmployeeAddressService(IEmployeeAddressRepository employeeAddressRepository)
        {
            this.employeeAddressRepository = employeeAddressRepository;
        }

        public IEnumerable<EmployeeAddress> GetEmployeeAddresses()
        {
            return employeeAddressRepository.GetAll();
        }
        public EmployeeAddress GetEmployeeAddress(int id)
        {
            return employeeAddressRepository.Get(id);
        }

        public EmployeeAddress AddEmployeeAddress(EmployeeAddress employeeAddress)
        {
            return employeeAddressRepository.Add(employeeAddress);
        }
        public EmployeeAddress UpdateEmployeeAddress(EmployeeAddress employeeAddress)
        {
            return employeeAddressRepository.Update(employeeAddress);
        }

        public void DeleteEmployeeAddress(int id)
        {
            employeeAddressRepository.Remove(id);
        }
    }
}
