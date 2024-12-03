using ServiceAuto.Models;
using ServiceAuto.Repositories.Interfaces;
using ServiceAuto.Services.Interfaces;

namespace ServiceAuto.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return employeeRepository.GetAll();
        }
        public Employee GetEmployee(int id)
        {
            return employeeRepository.Get(id);
        }

        public Employee AddEmployee(Employee employee)
        {
            return employeeRepository.Add(employee);
        }
        public Employee UpdateEmployee(Employee employee)
        {
            return employeeRepository.Update(employee);
        }

        public void DeleteEmployee(int id)
        {
            employeeRepository.Remove(id);
        }
    }
}
