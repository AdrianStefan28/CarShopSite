namespace ServiceAuto.Models
{
    public class Service : ModelEntity
    {
      
        public string? ServiceName { get; set; }
        public string? ServiceDescription { get; set;}
        public string? ServiceAddress1 { get; set;}
        public string? ServiceCity { get; set;}
        public string? ServiceState { get; set;}

        public ICollection<Employee>? Employees { get; set;}
        public ExpenseReport? ExpenseReport { get; set;}
    }
}
