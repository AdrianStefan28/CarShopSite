namespace ServiceAuto.Models
{
    public class Employee : ModelEntity
    {
      
        public string? EmployeeName { get; set; }
        public int? EmployeeAge { get; set; }
        public float? EmployeeSalary { get; set; }

        public string? EmployeeServiceName { get; set; }

        public EmployeeAddress? EmployeeAddress { get; set; }
        public Service? Service { get; set; }
    }

}
