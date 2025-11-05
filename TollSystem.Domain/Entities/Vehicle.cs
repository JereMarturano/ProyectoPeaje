namespace TollSystem.Domain.Entities
{
    public class Vehicle : Entity
    {
        public string LicensePlate { get; private set; }
        public string Color { get; private set; }
        public int Axles { get; private set; }

        public Vehicle(string licensePlate, string color, int axles)
        {
            LicensePlate = licensePlate;
            Color = color;
            Axles = axles;
        }
    }
}
