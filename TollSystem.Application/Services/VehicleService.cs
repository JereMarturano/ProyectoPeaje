using System.Threading.Tasks;
using TollSystem.Application.Interfaces;
using TollSystem.Domain.Entities;
using TollSystem.Domain.Repositories;
using TollSystem.Domain.ValueObjects;

namespace TollSystem.Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VehicleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Vehicle> GetOrCreateVehicleAsync(string licensePlate, string color, int axles, decimal height, bool hasDualWheels)
        {
            var validatedPlate = new LicensePlate(licensePlate);

            var vehicle = await _unitOfWork.Vehicles.GetByLicensePlateAsync(validatedPlate);

            if (vehicle == null)
            {
                vehicle = new Vehicle(validatedPlate, color, axles, height, hasDualWheels);
                await _unitOfWork.Vehicles.AddAsync(vehicle);
            }

            return vehicle;
        }
    }
}
