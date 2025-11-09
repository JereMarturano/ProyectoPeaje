namespace TollSystem.Presentation.DTOs
{
    public class CreateTollPassageDto
    {
        public string? LicensePlate { get; set; }
        public string? Color { get; set; }
        public int Axles { get; set; }
        public decimal Height { get; set; }
        public bool HasDualWheels { get; set; }
    }
}
