using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TollSystem.Application.Interfaces;
using TollSystem.Domain.Entities;
using TollSystem.Domain.Repositories;
using TollSystem.Domain.ValueObjects;

namespace TollSystem.Application.Services
{
    public class TollQueryService : ITollQueryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TollQueryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TollPassage>> GetTollPassagesByVehicleAsync(string licensePlate)
        {
            var validatedPlate = new LicensePlate(licensePlate);
            return await _unitOfWork.TollPassages.GetByLicensePlateAsync(validatedPlate);
        }

        public async Task<IEnumerable<TollPassage>> GetTollPassagesByDateRangeAsync(DateTime start, DateTime end)
        {
            return await _unitOfWork.TollPassages.GetByDateRangeAsync(start, end);
        }
    }
}
