using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TollSystem.Domain.Entities;

namespace TollSystem.Application.Interfaces
{
    public interface ITollQueryService
    {
        Task<IEnumerable<TollPassage>> GetTollPassagesByVehicleAsync(string licensePlate);
        Task<IEnumerable<TollPassage>> GetTollPassagesByDateRangeAsync(DateTime start, DateTime end);
    }
}
