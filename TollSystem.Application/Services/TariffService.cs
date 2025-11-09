using System;
using System.Threading.Tasks;
using TollSystem.Application.Interfaces;
using TollSystem.Domain.Entities;
using TollSystem.Domain.Enums;
using TollSystem.Domain.Repositories;

namespace TollSystem.Application.Services
{
    public class TariffService : ITariffService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TariffService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<decimal> GetFeeForVehicle(Vehicle vehicle)
        {
            var category = DetermineVehicleCategory(vehicle);
            vehicle.SetVehicleCategory(category);
            var tariff = await _unitOfWork.Tariffs.GetByCategoryAsync(category);

            if (tariff == null)
            {
                throw new Exception($"No tariff found for vehicle category {category}");
            }

            return tariff.Price;
        }

        private VehicleCategory DetermineVehicleCategory(Vehicle vehicle)
        {
            if (vehicle.Axles == 1) return VehicleCategory.Moto;
            if (vehicle.Axles == 2 && vehicle.Height < 2.1m) return VehicleCategory.Auto;
            if (vehicle.Axles == 2 && (vehicle.Height >= 2.1m || vehicle.HasDualWheels)) return VehicleCategory.CamionPesado2Ejes;
            if (vehicle.Axles >= 3 && vehicle.Axles <= 4) return VehicleCategory.Camion3a4Ejes;
            if (vehicle.Axles >= 5 && vehicle.Axles <= 6) return VehicleCategory.Camion5a6Ejes;
            if (vehicle.Axles > 6) return VehicleCategory.CamionMas6Ejes;

            // Default category if no match
            return VehicleCategory.Auto;
        }
    }
}
