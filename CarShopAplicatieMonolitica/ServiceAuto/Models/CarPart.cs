namespace ServiceAuto.Models
{
    public class CarPart : ModelEntity
    {
     
        public string? CarPartName { get; set;}
        public string? CarPartDescription { get; set; }
        public float? CarPartPrice { get; set; }
        public byte[]? CarPartPhoto { get; set; }

    }
}
