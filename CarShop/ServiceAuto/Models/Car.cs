namespace ServiceAuto.Models
{
    public class Car : ModelEntity
    {
        public string? CarBrand { get; set;}
        public string? CarModel { get; set;}
        public int? CarProductYear { get; set;}
        public float? CarPrice { get; set;}
        public byte[]? CarPhoto { get; set;}

    }
}
