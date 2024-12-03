namespace ServiceAuto.Models
{
    public partial class EmployeeAddress : ModelEntity
    {
      
        public string? EmployeeAddress1 { get; set; }
        public string? EmployeeCity { get; set; }
        public string? EmployeeState { get; set; }
        public int EmployeeId { get; set; }

        public Employee? Employee { get; set; }
    }
}
