namespace ServiceAuto.Models
{
    public class ProgramareService : ModelEntity
    {
        public string? Oras { get; set; }
        public string? Marca { get; set; }
        public string? Model { get; set; }
        public string? Defectiune { get; set; }

        public DateTime? DataProgramare { get; set; }
    }
}
