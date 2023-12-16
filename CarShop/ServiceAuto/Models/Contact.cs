namespace ServiceAuto.Models
{
    public class Contact : ModelEntity
    {
      
        public string? ContactFirstName { get; set; }
        public string? ContactLastName { get; set;}
        public string? ContactEmail { get; set; }
        public string? ContactSubject { get; set;}
        public string? ContactContext { get; set; }
    }
}
