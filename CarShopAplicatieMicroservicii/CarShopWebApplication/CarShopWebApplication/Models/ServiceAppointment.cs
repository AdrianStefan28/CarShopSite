namespace CarShopWebApplication.Models
{
    public class ServiceAppointment
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public DateTime AppointmentDate { get; set; }
        public string ServiceDescription { get; set; }
    }
}
