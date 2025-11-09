using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TollSystem.Domain.Entities;
using TollSystem.Domain.Repositories;
using TollSystem.Domain.ValueObjects;
using TollSystem.Infrastructure.Data;

namespace TollSystem.Infrastructure.Repositories
{
    public class TollPassageRepository : Repository<TollPassage>, ITollPassageRepository
    {
        public TollPassageRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TollPassage>> GetByLicensePlateAsync(LicensePlate licensePlate)
        {
            return await _context.TollPassages
                .Include(p => p.Vehicle)
                .Where(p => p.Vehicle.LicensePlate == licensePlate)
                .ToListAsync();
        }

        public async Task<IEnumerable<TollPassage>> GetByDateRangeAsync(DateTime start, DateTime end)
        {
            return await _context.TollPassages
                .Include(p => p.Vehicle)
                .Where(p => p.Timestamp >= start && p.Timestamp <= end)
                .ToListAsync();
        }
    }
}
