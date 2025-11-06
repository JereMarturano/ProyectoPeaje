using System.Threading.Tasks;
using TollSystem.Application.Interfaces;
using TollSystem.Domain.Entities;
using TollSystem.Domain.Repositories;
using TollSystem.Domain.ValueObjects;

namespace TollSystem.Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Vehicle> GetOrCreateVehicleAsync(string licensePlate, string color, int axles)
        {
            // The validation is now handled by the LicensePlate value object
            var validatedPlate = new LicensePlate(licensePlate);

            // This is a simplified implementation. In a real-world scenario, you would
            // probably want to query the vehicle by license plate first.
            var vehicle = new Vehicle(validatedPlate, color, axles);
            return await _vehicleRepository.AddAsync(vehicle);
        }
    }
}
