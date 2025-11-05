using System;

namespace TollSystem.Domain.Entities
{
    public class TollPassage : Entity
    {
        public int VehicleId { get; private set; }
        public Vehicle Vehicle { get; private set; }
        public DateTime Timestamp { get; private set; }

        private TollPassage() { } // For EF Core

        public TollPassage(Vehicle vehicle)
        {
            Vehicle = vehicle;
            Timestamp = DateTime.UtcNow;
        }
    }
}
