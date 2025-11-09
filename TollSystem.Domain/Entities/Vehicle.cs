using TollSystem.Domain.Enums;
using TollSystem.Domain.ValueObjects;

namespace TollSystem.Domain.Entities
{
    public class Vehicle : Entity
    {
        public LicensePlate LicensePlate { get; private set; }
        public string Color { get; private set; }
        public int Axles { get; private set; }
        public int VehicleCategoryId { get; private set; }
        public VehicleCategory VehicleCategory { get; private set; }
        public decimal Height { get; private set; }
        public bool HasDualWheels { get; private set; }


        public Vehicle(LicensePlate licensePlate, string color, int axles, decimal height, bool hasDualWheels)
        {
            LicensePlate = licensePlate;
            Color = color;
            Axles = axles;
            Height = height;
            HasDualWheels = hasDualWheels;
        }

        public void SetVehicleCategory(VehicleCategory category)
        {
            VehicleCategory = category;
            VehicleCategoryId = (int)category;
        }
    }
}
