using ServiceAuto.Models;

namespace ServiceAuto.Services.Interfaces
{
    public interface IEmployeeAddressService
    {
        IEnumerable<EmployeeAddress> GetEmployeeAddresses();
        EmployeeAddress GetEmployeeAddress(int id);
        EmployeeAddress AddEmployeeAddress(EmployeeAddress employeeAddress);
        EmployeeAddress UpdateEmployeeAddress(EmployeeAddress employeeAddress);
        void DeleteEmployeeAddress(int id);
    }
}
