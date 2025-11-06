using TollSystem.Domain.ValueObjects;

namespace TollSystem.Domain.Entities
{
    public class Vehicle : Entity
    {
        public LicensePlate LicensePlate { get; private set; }
        public string Color { get; private set; }
        public int Axles { get; private set; }

        public Vehicle(LicensePlate licensePlate, string color, int axles)
        {
            LicensePlate = licensePlate;
            Color = color;
            Axles = axles;
        }
    }
}
