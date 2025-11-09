using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using TollSystem.Application.Interfaces;
using TollSystem.Domain.Entities;
using TollSystem.Domain.Repositories;

namespace TollSystem.Application.Services
{
    public class TollPassageService : ITollPassageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITariffService _tariffService;
        private readonly ILogger<TollPassageService> _logger;

        public TollPassageService(IUnitOfWork unitOfWork, ITariffService tariffService, ILogger<TollPassageService> logger)
        {
            _unitOfWork = unitOfWork;
            _tariffService = tariffService;
            _logger = logger;
        }

        public async Task<TollPassage> CreateTollPassageAsync(Vehicle vehicle)
        {
            try
            {
                _logger.LogInformation("Creating toll passage for vehicle {LicensePlate}", vehicle.LicensePlate);
                var amount = await _tariffService.GetFeeForVehicle(vehicle);
                var tollPassage = new TollPassage(vehicle, amount);
                await _unitOfWork.TollPassages.AddAsync(tollPassage);
                await _unitOfWork.CompleteAsync();
                _logger.LogInformation("Toll passage created successfully for vehicle {LicensePlate}", vehicle.LicensePlate);
                return tollPassage;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating toll passage for vehicle {LicensePlate}", vehicle.LicensePlate);
                throw;
            }
        }
    }
}
