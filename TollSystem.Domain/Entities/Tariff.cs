using TollSystem.Domain.Enums;

namespace TollSystem.Domain.Entities
{
    public class Tariff : Entity
    {
        public VehicleCategory VehicleCategory { get; private set; }
        public decimal Price { get; private set; }

        public Tariff(VehicleCategory vehicleCategory, decimal price)
        {
            VehicleCategory = vehicleCategory;
            Price = price;
        }
    }
}
