using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TollSystem.Domain.Entities;
using TollSystem.Domain.ValueObjects;

namespace TollSystem.Domain.Repositories
{
    public interface ITollPassageRepository : IRepository<TollPassage>
    {
        Task<IEnumerable<TollPassage>> GetByLicensePlateAsync(LicensePlate licensePlate);
        Task<IEnumerable<TollPassage>> GetByDateRangeAsync(DateTime start, DateTime end);
    }
}
